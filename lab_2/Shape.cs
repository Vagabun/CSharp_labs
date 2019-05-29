namespace lab_2 {

    internal abstract class Shape {
        abstract public double calculateSquare();
        abstract public double calculatePerimeter();
        abstract public Point calculateCenterOfMass();

        protected double square { get; set; }
        protected double perimeter { get; set; }
        protected Point centerOfMass { get; set; }
    }
}
