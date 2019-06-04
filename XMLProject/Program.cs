using System;
using System.Xml.Serialization;
using System.IO;

namespace XMLProject {
    class Program {
        private static void MainMenuHelp() {
            Console.WriteLine();
            Console.WriteLine("A - add department");
            Console.WriteLine("R - remove department");
            Console.WriteLine("P - print all departments");
            Console.WriteLine("S - select department");
            Console.WriteLine("Q - quit program");
        }

        private static void DepartmentMenuHelp() {
            Console.WriteLine();
            Console.WriteLine("A - add employee");
            Console.WriteLine("R - remove employee");
            Console.WriteLine("E - edit department");
            Console.WriteLine("P - print all employees");
            Console.WriteLine("Q - quit");
        }

        private static void DepartmentMenu(ref Company company, int departmentIndex) {
            Console.WriteLine("selected department: {0}", company.Departments[departmentIndex]);
            while (true) {
                DepartmentMenuHelp();
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.A: {
                            //string name, string salary, double rate, string position
                            Console.WriteLine("\nspecify name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("specify salary:");
                            string salary = Console.ReadLine();
                            Console.WriteLine("specify rate:");
                            double rate = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("specify position:");
                            string position = Console.ReadLine();
                            company.Departments[departmentIndex].AddEmployee(name, salary, rate, position);
                            break;
                        }
                    case ConsoleKey.R: {
                            Console.WriteLine("\nspecify employee index:");
                            int index = Convert.ToInt32(Console.ReadLine());
                            company.Departments[departmentIndex].RemoveEmployee(index);
                            break;
                        }
                    case ConsoleKey.P: {
                            company.Departments[departmentIndex].PrintEmployees();
                            break;
                        }
                    case ConsoleKey.E: {
                            Console.WriteLine("\nspecify department new name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("specify department new rate:");
                            double rate = Convert.ToDouble(Console.ReadLine());
                            company.Departments[departmentIndex].EditDepartment(name, rate);
                            break;
                        }
                    case ConsoleKey.Q: {
                            return;
                        }
                    default: {
                            Console.WriteLine("\nwrong input");
                            break;
                        }
                }
            }
        }

        private static void MainMenu(ref Company company) {
            while (true) {
                MainMenuHelp();
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.A: {
                            Console.WriteLine("\nspecify name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("specify rate:");
                            double rate = Convert.ToDouble(Console.ReadLine());
                            company.AddDepartment(name, rate);
                            break;
                        }
                    case ConsoleKey.R: {
                            Console.WriteLine("\nspecify index:");
                            int index = Convert.ToInt32(Console.ReadLine());
                            company.RemoveDepartment(index);
                            break;
                        }
                    case ConsoleKey.P: {
                            company.PrintDepartments();
                            break;
                        }
                    case ConsoleKey.S: {
                            Console.WriteLine("\nspecify department by index:");
                            int index = Convert.ToInt32(Console.ReadLine());
                            DepartmentMenu(ref company, index);
                            break;
                        }
                    case ConsoleKey.Q: {
                            return;
                        }
                    default: {
                            Console.WriteLine("\nwrong input");
                            break;
                        }
                }
            }
        }


        static void Main(string[] args) {
            XmlSerializer formatter = new XmlSerializer(typeof(Company));
            Company company = new Company();

            string path = "t.xml";
            if (File.Exists(path)) {
                using (FileStream fs = File.OpenRead(path)) {
                    company = (Company)formatter.Deserialize(fs);
                }
                File.Delete(path);
            }

            MainMenu(ref company);

            using (FileStream fs = File.Create(path)) {
                formatter.Serialize(fs, company);
            }

        }
    }
}
