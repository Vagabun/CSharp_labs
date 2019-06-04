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
    }
}
