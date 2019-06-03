using System;
using System.Drawing;

namespace MatrixImageFilter {
    public class MatrixFilter {
        private readonly int radius;
        private readonly int K;
        private double[,] kernel; //gaussian matrix
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
            return NormalProcessing(ref src);
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
            Bitmap i = (Bitmap)image.Clone();

            for (int x = 0; x < i.Width; ++x)
                for (int y = 0; y < i.Height; ++y) {
                    Color newPixel = ApplyKernelToPixel(x, y, i);
                    i.SetPixel(x, y, newPixel);
                }
            return i;
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

    }
}
