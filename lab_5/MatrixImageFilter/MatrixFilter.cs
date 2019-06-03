using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MatrixImageFilter {
    public class MatrixFilter {
        private readonly int radius;
        private readonly int K;
        private readonly double[,] kernel; //gaussian matrix
        private readonly Func<int, int, int, double> gaussian =
            (x, y, sigma) => Math.Exp(-(x * x + y * y) / (2 * sigma * sigma)) /
            (2 * Math.PI * (sigma * sigma));

        public MatrixFilter(int radius) {
            this.radius = radius;
            K = 2 * radius + 1;
            kernel = new double[K, K];
            ComputeGaussianMatrix();
        }

        public Bitmap ApplyGaussianBlur(Bitmap src, bool runUnsafe) {
            return runUnsafe ? UnsafeProcessing(ref src) : NormalProcessing(ref src);
        }

        private void ComputeGaussianMatrix() {
            int sigma = 1;
            double sum = 0.0;
            for (int x = 0; x < K; ++x)
                for (int y = 0; y < K; ++y) {
                    // weighted average of elements neighborhood
                    kernel[x, y] = gaussian(x - radius, y - radius, sigma);
                    sum += kernel[x, y];
                }

            //normalize kernel
            for (int x = 0; x < K; ++x)
                for (int y = 0; y < K; ++y)
                    kernel[x, y] /= sum;
        }

        private Bitmap NormalProcessing(ref Bitmap image) {
            Bitmap processedImage = (Bitmap)image.Clone();

            for (int x = 0; x < processedImage.Width; ++x)
                for (int y = 0; y < processedImage.Height; ++y) {
                    Color newPixel = ApplyKernelToPixel(x, y, processedImage);
                    processedImage.SetPixel(x, y, newPixel);
                }
            return processedImage;
        }

        private Bitmap UnsafeProcessing(ref Bitmap image) {
            Bitmap processedImage = (Bitmap)image.Clone();

            unsafe {
                BitmapData data = processedImage.LockBits(
                    new Rectangle(0, 0, processedImage.Width, processedImage.Height),
                    ImageLockMode.ReadWrite, processedImage.PixelFormat
                );
                int bytesPerPixel = Image.GetPixelFormatSize(processedImage.PixelFormat) / 8;
                int heightInPixels = data.Height;
                int widthInBytes = data.Width * bytesPerPixel;
                byte* firstPixelPointer = (byte*)data.Scan0;

                for (int y = 0; y < heightInPixels; ++y) {
                    //stride - width of a single row of pixels
                    byte* currentLine = firstPixelPointer + y * data.Stride;
                    for (int x = 0; x < widthInBytes; ++x)
                        ApplyKernelToPixelUnsafe(x, y, bytesPerPixel, heightInPixels,
                            widthInBytes, firstPixelPointer, currentLine, data.Stride);
                }
                processedImage.UnlockBits(data);
            }
            return processedImage;
        }

        //using formula from lab text
        private Color ApplyKernelToPixel(int x, int y, Bitmap image) {
            byte R = 0, G = 0, B = 0;
            for (int i = y - radius; i <= y + radius; ++i) {
                int n = (i < 0) ? 0 : (i >= image.Height) ? image.Height - 1 : i;
                for (int j = x - radius; j <= x + radius; ++j) {
                    int m = (j < 0) ? 0 : (j >= image.Width) ? image.Width - 1 : j;
                    Color pixel = image.GetPixel(m, n);
                    R += (byte)(pixel.R * kernel[i - y + radius, j - x + radius]);
                    G += (byte)(pixel.G * kernel[i - y + radius, j - x + radius]);
                    B += (byte)(pixel.B * kernel[i - y + radius, j - x + radius]);
                }
            }

            return Color.FromArgb(R, G, B);
        }

        private unsafe void ApplyKernelToPixelUnsafe(int x, int y, int bytesPerPixel,
            int heightInPixels, int widthInBytes, byte* firstPixelPointer, byte* currentLine, int stride) {
            byte R = 0, G = 0, B = 0;
            for (int i = y - radius; i <= y + radius; ++i) {
                int n = (i < 0) ? 0 : (i >= heightInPixels) ? heightInPixels - 1 : i;
                for (int j = x - radius * bytesPerPixel; j <= x + radius * bytesPerPixel; j += bytesPerPixel) {
                    int m = (j < 0) ? 0 : (j >= widthInBytes) ? widthInBytes - bytesPerPixel : j;
                    int position = m + n * stride;
                    Color pixel = Color.FromArgb(firstPixelPointer[position + 2],
                        firstPixelPointer[position + 1], firstPixelPointer[position]);
                    R += (byte)(pixel.R * kernel[i - y + radius, (j - x) / bytesPerPixel + radius]);
                    G += (byte)(pixel.G * kernel[i - y + radius, (j - x) / bytesPerPixel + radius]);
                    B += (byte)(pixel.B * kernel[i - y + radius, (j - x) / bytesPerPixel + radius]);
                }
            }
            currentLine[x + 2] = R;
            currentLine[x + 1] = G;
            currentLine[x] = B;
        }

    }
}
