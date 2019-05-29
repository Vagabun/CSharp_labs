using System;
using System.Text;

namespace lab_2 {
    internal sealed class Circle : Shape {
        private Point _center { get; set; }
        private double _radius { get; set; }

        public Circle(Point center, double radius) {
            _center = center;
            _radius = radius;

            perimeter = calculatePerimeter();
            square = calculateSquare();
            centerOfMass = calculateCenterOfMass();

        }

        public override Point calculateCenterOfMass() {
            return _center;
        }

        public override double calculatePerimeter() {
            return _radius * 2 * Math.PI;
        }

        public override double calculateSquare() {
            return Math.PI * Math.Pow(_radius, 2);
        }

        public override string ToString() {
            return string.Format("Circle: Square = {0}, Perimeter = {1}, " +
                "center of mass in ({2},{3})", square, perimeter, centerOfMass.x, centerOfMass.y);
        }
    }
}
