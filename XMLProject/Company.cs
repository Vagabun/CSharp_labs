using System;
using System.Collections.Generic;
namespace XMLProject {

    [Serializable]
    public class Company {
        public List<Department> Departments;

        public Company() {
            Departments = new List<Department>();
        }

        public void AddDepartment(string name, double rate) {
            Departments.Add(new Department(name, rate));
        }

        public void RemoveDepartment(int index) {
            //var item = Departments.Find((obj) => obj.Name == name);
            //if (item.Staff.Count > 0)
            //    Departments.Remove(item);
            //else {
            //    Console.WriteLine("this department contains some staff, can't delete");
            //}

            if (Departments[index].Staff.Count > 0) {
                Console.WriteLine("this department contains some staff, can't delete");
            }
            else
                Departments.RemoveAt(index);
        }

        public void MoveEmployee(string employeeName, string oldDepartmentName, string newDepartmentName) {
            var oldIndex = Departments.FindIndex((obj) => obj.Name == oldDepartmentName);
            var newIndex = Departments.FindIndex((obj) => obj.Name == newDepartmentName);
            var empl = Departments[oldIndex].MoveEmployee(employeeName);
            Departments[newIndex].AddEmployee(empl.Name, empl.Salary, empl.Rate, empl.Position);
        }

        public void PrintDepartments() {
            Console.WriteLine();
            for (int i = 0; i < Departments.Count; ++i)
                Console.WriteLine("{0} - {1}", i, Departments[i]);
            //Console.WriteLine("{0} - department name: {1} rate: {2}", i, Departments[i].Name, Departments[i].Rate);
        }
    }
}
