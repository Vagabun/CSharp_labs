using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_2 {
    internal sealed class Polygon : Shape {
        private int _pointsCount { get; set; }
        private List<Point> _pointsList;

        public Polygon(int pointsCount, List<Point> pointsList) {
            _pointsCount = pointsCount;
            _pointsList = new List<Point>(pointsList);
            _pointsList.Add(_pointsList.First());

            perimeter = calculatePerimeter();
            square = calculateSquare();
            centerOfMass = calculateCenterOfMass();

        }

        public override Point calculateCenterOfMass() {
            double newX = 0, newY = 0;
            Point[] arr = _pointsList.ToArray();
            for (int i = 0; i < _pointsList.Count - 1; ++i) {
                newX += EuclidMetric(arr[i], arr[i + 1]) * arr[i].x;
                newY += EuclidMetric(arr[i], arr[i + 1]) * arr[i].y;
            }
            return new Point(newX / calculatePerimeter(), newY / calculatePerimeter());
        }

        public override double calculatePerimeter() {
            double result = 0;
            Point[] arr = _pointsList.ToArray();
            for (int i = 0; i < _pointsList.Count - 1; ++i)
                result += Math.Sqrt(Math.Pow(Math.Abs(arr[i + 1].x - arr[i].x), 2) +
                    Math.Pow(Math.Abs(arr[i + 1].y - arr[i].y), 2));
            return result;

        }

        public override double calculateSquare() {
            double result = 0;
            Point[] arr = _pointsList.ToArray();
            for (int i = 0; i < _pointsList.Count - 1; ++i)
                result += (arr[i + 1].x - arr[i].x) * (arr[i + 1].y + arr[i].y);
            return Math.Abs(result / 2);
        }

        private double EuclidMetric(Point p1, Point p2) {
            return Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
        }
    }
}
