using System;
using System.Text;

namespace lab_2 {
    internal sealed class Ellipse : Shape {
        private Point _focus_1 { get; set; }
        private Point _focus_2 { get; set; }
        private double _semiMajorAxis { get; set; }
        private double _semiMinorAxis { get; set; }
        private double _focalDistance { get; set; }
        private double _e { get; set; }

        public Ellipse(Point focus_1, Point focus_2, double semiMajorAxis) {
            _focus_1 = focus_1;
            _focus_2 = focus_2;
            _semiMajorAxis = semiMajorAxis;

            _focalDistance = Math.Sqrt(Math.Pow(Math.Abs(_focus_1.x - _focus_2.x), 2) +
                Math.Pow(Math.Abs(_focus_1.y - _focus_2.y), 2)) / 2;
            _e = _focalDistance / _semiMajorAxis;
            _semiMinorAxis = _semiMinorAxis * Math.Sqrt(1 - Math.Pow(_e, 2));

            perimeter = calculatePerimeter();
            square = calculateSquare();
            centerOfMass = calculateCenterOfMass();
        }

        public override double calculateSquare() {
            return Math.PI * _semiMajorAxis * _semiMinorAxis;
        }

        public override double calculatePerimeter() {
            return (4 * Math.PI * _semiMajorAxis * _semiMinorAxis +
                Math.Pow(_semiMajorAxis - _semiMinorAxis, 2)) / (_semiMajorAxis + _semiMinorAxis);
        }

        public override Point calculateCenterOfMass() {
            return new Point(_focus_1.x + (_focus_2.x - _focus_1.x),
                _focus_1.y + (_focus_2.y - _focus_1.y));
        }

        public override string ToString() {

            return string.Format("Ellipse: Square = {0}, Perimeter = {1}, " +
                "center of mass in ({2},{3})", square, perimeter, centerOfMass.x, centerOfMass.y);
        }
    }
}
