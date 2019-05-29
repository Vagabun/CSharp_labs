namespace lab_2 {

    internal abstract class Shape {
        abstract public double Square();
        abstract public double Perimeter();
        abstract public double CenterOfMass();

        protected double _square { get; set; }
        protected double _perimeter { get; set; }
        protected Point _center { get; set; }
    }
}
