using System;
using System.IO;

namespace lab_1 {
    class Program {
        static void Main(string[] args) {

            //if (args.Length < 2) {
            //    Console.WriteLine("Specify filenames");
            //    return;
            //}

            //string path_1 = args[0];
            //string path_2 = args[1];

            string path_1 = "a.txt";
            string path_2 = "b.txt";

            using (FileStream fs_1 = File.OpenRead(path_1),
                   fs_2 = File.OpenRead(path_2)) {

                    int data_1, data_2;
                    while (true) {
                        data_1 = fs_1.ReadByte(); 
                        data_2 = fs_2.ReadByte();

                        if (data_1 == -1 && data_2 == -1)
                            break;
                        if (data_1 == -1 && data_2 != -1)
                            Console.WriteLine(String.Format("Diff: <EOF>(0x{0:X})", data_2));
                        else if (data_1 != -1 && data_2 == -1)
                            Console.WriteLine(String.Format("Diff: 0x{0:X}(<EOF>)", data_1));
                        else if (data_1 != data_2) {
                            Console.WriteLine(String.Format("Diff: 0x{0:X}(0x{1:X})", data_1, data_2));
                        }
                    }
                }
            }
    }
}
