using System;

namespace MatrixImageFilter {
    public class MatrixFilter {
        private readonly int radius;
        private readonly int K;
        private double[,] kernel; //gaussian matrix
        private readonly Func<int, int, int, double> gaussian =
            (x, y, sigma) => Math.Exp(-(Math.Pow(x, 2) + Math.Pow(y, 2)) / (2 * Math.Pow(sigma, 2)) / (2 * Math.PI * Math.Pow(sigma, 2);

        public MatrixFilter(int radius) {
            this.radius = radius;
            K = 2 * radius + 1;
            kernel = new double[K, K];
            ComputeGaussianMatrix();
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


    }
}
