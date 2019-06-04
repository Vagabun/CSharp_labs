using System;
namespace XMLProject {

    [Serializable]
    public class Employee {
        public string Name { get; set; }
        public string Salary { get; set; }
        public double Rate { get; set; }
        public string Position { get; set; }

        public Employee() {
        }

        public Employee(string name, string salary, double rate, string position) {
            Name = name;
            Salary = salary;
            Rate = rate;
            Position = position;
        }

        public override string ToString() {
            return
                string.Format("name: {0}, salary: {1}, rate: {2}, position: {3}",
                Name, Salary, Rate, Position);
        }
    }
}
