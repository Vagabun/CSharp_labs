using System;

namespace lab_2 {
    internal static class Utils {
        public static double EuclidMetric(Point p1, Point p2) {
            return Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
        }
    }
}
