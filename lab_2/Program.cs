using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace lab_2 {
    class Program {
        static void Run() {
            List<Shape> shapes = new List<Shape>();

            while (true) {
                printHelp();
                var key = Console.ReadKey();

                switch (key.Key) {
                    case ConsoleKey.C: {
                            createMenu(shapes);
                            break;
                        }
                    case ConsoleKey.L: {
                            printShapes(shapes);
                            break;
                        }
                    case ConsoleKey.H: {
                            printHelp();
                            break;
                        }
                    case ConsoleKey.Q: {
                            return;
                        }
                    default: {
                            Console.WriteLine("Wrong Input");
                            break;
                        }
                }
            }
        }

        private static void printHelp() {
            Console.WriteLine();
            Console.WriteLine("c - create a shape");
            Console.WriteLine("l - print a list of existing shapes");
            Console.WriteLine("h - help");
            Console.WriteLine("q - exit of program");
        }

        private static void printShapes(List<Shape> shapes) {
            Console.WriteLine();
            foreach (Shape item in shapes)
                Console.WriteLine(item);
        }

        private static void createMenu(List<Shape> shapes) {
            Console.WriteLine();
            Console.WriteLine("Choose a shape to create: E - Ellipse, С - Circle, P - polygon, press Q to cancel");

            while (true) {
                var key = Console.ReadKey();

                switch (key.Key) {
                    case ConsoleKey.E: {
                            createEllipse(shapes);
                            return;
                        }
                    case ConsoleKey.C: {
                            createCircle(shapes);
                            return;
                        }
                    case ConsoleKey.P: {
                            createPolygon(shapes);
                            return;
                        }
                    case ConsoleKey.Q: {
                            return;
                        }
                    default: {
                            Console.WriteLine("Wrong input");
                            break;
                        }
                }
            }

        }

        private static void createEllipse(List<Shape> shapes) {
            Point focus1, focus2;
            Console.WriteLine();
            Console.WriteLine("Input coordinates of focuses in a format x1 y1 x2 y2");
            string line = Console.ReadLine();
            var regForPoint = new Regex(@"(\d*\.?\d+)\s+(\d*\.?\d+)");
            var matches = regForPoint.Matches(line);
            if (matches.Count == 2) {
                GroupCollection group1 = matches[0].Groups;
                GroupCollection group2 = matches[1].Groups;
                focus1 = new Point(Convert.ToDouble(group1[1].Value), Convert.ToDouble(group1[2].Value));
                focus2 = new Point(Convert.ToDouble(group2[1].Value), Convert.ToDouble(group2[2].Value));
                Console.WriteLine("Input a length of a semi-major axis");
                double axis = Convert.ToDouble(Console.ReadLine());
                shapes.Add(new Ellipse(focus1, focus2, axis));
            }
            else {
                Console.WriteLine("Wrong input");
            }
        }

        private static void createCircle(List<Shape> shapes) {
            Point center;
            Console.WriteLine();
            Console.WriteLine("Input coordinates of circle's center in a format x y");
            string line = Console.ReadLine();
            var regForPoint = new Regex(@"(\d*\.?\d+)\s+(\d*\.?\d+)");
            var matches = regForPoint.Matches(line);
            if (matches.Count == 1) {
                GroupCollection group1 = matches[0].Groups;
                center = new Point(Convert.ToDouble(group1[1].ToString()), Convert.ToDouble(group1[2].ToString()));
                Console.WriteLine("Input a radius");
                double radius = Convert.ToDouble(Console.ReadLine());
                shapes.Add(new Circle(center, radius));
            }
            else {
                Console.WriteLine("Wrong input");
            }
        }

        private static void createPolygon(List<Shape> shapes) {
            int n;
            List<Point> points = new List<Point>();
            Console.WriteLine();
            Console.WriteLine("Input a number of points in a polygon");
            n = Convert.ToInt32(Console.ReadLine());
            if (n <= 2) {
                Console.WriteLine("Input at least 3 points to create a polygon");
                return;
            }
            Console.WriteLine("Input {0} points in a format x1 y1 x2 y2 .... xn yn", n);
            string line = Console.ReadLine();
            var regForPoint = new Regex(@"(\d*\.?\d+)\s+(\d*\.?\d+)");
            var matches = regForPoint.Matches(line);
            if (matches.Count == n) {
                foreach (Match match in matches) {
                    GroupCollection group = match.Groups;
                    points.Add(new Point(Convert.ToDouble(group[1].Value), Convert.ToDouble(group[2].Value)));
                }
                shapes.Add(new Polygon(n, points));
            }
            else {
                Console.WriteLine("Wrong input");
            }
        }

        static void Main() {
            Run();
        }
    }
}
