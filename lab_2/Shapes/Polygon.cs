using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_2 {
    internal sealed class Polygon : Shape {
        private readonly List<Point> _pointsList;

        public Polygon(List<Point> pointsList) {
            _pointsList = new List<Point>(pointsList);
            _pointsList.Add(_pointsList.First());

            perimeter = calculatePerimeter();
            square = calculateSquare();
            centerOfMass = calculateCenterOfMass();

        }

        public override Point calculateCenterOfMass() {
            double newX = 0, newY = 0;
            for (int i = 0; i < _pointsList.Count - 1; ++i) {
                newX += Utils.EuclidMetric(_pointsList[i], _pointsList[i + 1]) *
                    (_pointsList[i].x + _pointsList[i + 1].x) / 2;
                newY += Utils.EuclidMetric(_pointsList[i], _pointsList[i + 1]) *
                    (_pointsList[i].y + _pointsList[i + 1].y) / 2;
            }
            return new Point(newX / perimeter, newY / perimeter);
        }

        public override double calculatePerimeter() {
            double result = 0;
            for (int i = 0; i < _pointsList.Count - 1; ++i)
                result += Utils.EuclidMetric(_pointsList[i + 1], _pointsList[i]);
            return result;

        }

        public override double calculateSquare() {
            double result = 0;
            for (int i = 0; i < _pointsList.Count - 1; ++i)
                result += (_pointsList[i + 1].x - _pointsList[i].x)
                    * (_pointsList[i + 1].y + _pointsList[i].y);
            return Math.Abs(result / 2);
        }

        public override string ToString() {
            return string.Format("Polygon: Square = {0}, Perimeter = {1}, " +
                "center of mass in ({2},{3})", square, perimeter, centerOfMass.x, centerOfMass.y);
        }
    }
}
