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
            if (Departments[index].Staff.Count > 0) {
                Console.WriteLine("this department contains some staff, can't delete");
            }
            else
                Departments.RemoveAt(index);
        }

        public void PrintDepartments() {
            Console.WriteLine();
            for (int i = 0; i < Departments.Count; ++i)
                Console.WriteLine("{0} - {1}", i, Departments[i]);
        }
    }
}
