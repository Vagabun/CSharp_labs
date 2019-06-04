using System;
using System.Collections.Generic;

namespace XMLProject {

    [Serializable]
    public class Department {
        public string Name { get; set; }
        public double Rate { get; set; }
        public List<Employee> Staff;

        public Department() {
        }

        public Department(string name, double rate) {
            Name = name;
            Rate = rate;
            Staff = new List<Employee>();
        }

        public void AddEmployee(string name, string salary, double rate, string position) {
            if (Rate - rate > 0) {
                Staff.Add(new Employee(name, salary, rate, position));
                Rate -= rate;
            }
            else {
                Console.WriteLine("can't add new employee with this rate, remaining rate = {0}", Rate);
            }
        }

        public void RemoveEmployee(string name) {
            var item = Staff.Find((obj) => obj.Name == name);
            Staff.Remove(item);
        }

        public Employee MoveEmployee(string name) {
            var item = Staff.Find((obj) => obj.Name == name);
            Staff.Remove(item);
            return item;
        }
    }
}
