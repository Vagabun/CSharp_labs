using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MatrixImageFilter {
    class Program {
        static void Main(string[] args) {

            Console.WriteLine("path to image:");
            var filepath = Console.ReadLine();
            Bitmap img = new Bitmap(filepath);


            MatrixFilter m = new MatrixFilter(5);
            var start = DateTime.Now;
            Bitmap result = m.ApplyGaussianBlur(img, false);
            var end = DateTime.Now - start;
            result.Save("normal.png", ImageFormat.Png);
            Console.WriteLine("normal processing: {0}", end.ToString());

            start = DateTime.Now;
            result = m.ApplyGaussianBlur(img, true);
            end = DateTime.Now - start;
            result.Save("unsafe.png", ImageFormat.Png);
            Console.WriteLine("unsafe processing: {0}", end.ToString());

        }
    }
}
