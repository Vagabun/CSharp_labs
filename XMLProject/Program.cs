using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace XMLProject {
    class Program {
        //private static void PrintHelp() {
        //    Console.WriteLine();
        //    Console.WriteLine("A - add employee");
        //    Console.WriteLine("R - remove employee");
        //    Console.WriteLine("D - add department");
        //    Console.WriteLine("L - remove department");
        //    Console.WriteLine("E - edit department");
        //    Console.WriteLine("M - move employee");
        //    Console.WriteLine("Q - quit program");
        //}

        private static void MainMenuHelp() {
            Console.WriteLine();
            Console.WriteLine("A - add department");
            Console.WriteLine("R - remove department");
            Console.WriteLine("P - print all departments");
            Console.WriteLine("S - select department");
            Console.WriteLine("Q - quit program");
        }

        private static void MainMenu(ref Company company) {
            while (true) {
                MainMenuHelp();
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.A: {
                            Console.WriteLine("\nspecify name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("\nspecify rate:");
                            string rate = Console.ReadLine();
                            company.AddDepartment(name, Convert.ToDouble(rate));
                            break;
                        }
                    case ConsoleKey.R: {
                            Console.WriteLine("\nspecify index:");
                            string index = Console.ReadLine();
                            company.RemoveDepartment(Convert.ToInt32(index));
                            break;
                        }
                    case ConsoleKey.P: {
                            company.PrintDepartments();
                            break;
                        }
                    case ConsoleKey.S: {
                            Console.WriteLine("\nspecify department by index:");
                            string index = Console.ReadLine();
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
