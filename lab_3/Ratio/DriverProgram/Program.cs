using System;
using RatioLibrary;

namespace DriverProgram {
    class Program {
        static void Main() {

            Ratio r1 = new Ratio(3, 4);
            Ratio r2 = new Ratio(7, 8);
            Console.WriteLine("{0} is {1}, {2} is {3}", r1, r1.ToDouble(), r2, r2.ToDouble());
            Console.WriteLine("{0} + {1} = {2}", r1, r2, r1 + r2);
            Console.WriteLine("{0} - {1} = {2}", r1, r2, r1 - r2);
            Console.WriteLine("{0} * {1} = {2}", r1, r2, r1 * r2);
            Console.WriteLine("{0} / {1} = {2}", r1, r2, r1 / r2);
        }
    }
}
