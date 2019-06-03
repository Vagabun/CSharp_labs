using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MatrixImageFilter {
    class Program {
        static void Main(string[] args) {
            MatrixFilter m = new MatrixFilter(5);
            Bitmap img = new Bitmap("image.png");
            Bitmap result = m.ApplyGaussianBlur(img, false);
            result.Save("normal.png", ImageFormat.Png);
        }
    }
}
