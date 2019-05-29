using System;
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
    }
}
