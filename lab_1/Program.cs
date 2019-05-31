using System;
using System.IO;

namespace lab_1 {

    enum GlobalState
    {
        Identical,
        Differ,
    }
    enum FilesState
    {
        EOF1,
        EOF2,
        EOFs,
        None
    }

    enum OutputState
    {
        Limited,
        Unlimited
    }

    class FileAdapter
    {
        public FileAdapter(string filename)
        {
            try
            {
                _fs = File.OpenRead(filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.Exit(-1);
            }
            _bs = new BufferedStream(_fs); //will it be correctly disposed?
            Data = 0;
            Position = 0;
            EOF = false;
        }
        public void Read()
        {
            if (EOF) 
                Data = -1;
            else
            {
                Data = _bs.ReadByte();
                Position = _bs.Position;
                EOF |= Data == -1; //OR assignment
            }
        }

        public long Position { get; private set; }
        public int Data { get; private set;  }
        public bool EOF { get; private set; }
        private readonly FileStream _fs;
        private readonly BufferedStream _bs;
    }

    class Program {

        static int output_counter;

        static void SkipSameBytes(ref FileAdapter f1, ref FileAdapter f2)
        {
            while (f1.Data == f2.Data && !f1.EOF && !f2.EOF)
            {
                f1.Read();
                f2.Read();
            }
        }

        static void ReadTillEOF(ref FileAdapter f, string pattern, string EOFmsg, int N, OutputState ostate)
        {

            Console.Write(EOFmsg);
            switch (ostate)
            {
                case OutputState.Unlimited:
                    while (true)
                    {
                        Console.Write(pattern, f.Data);
                        f.Read();
                        if (f.EOF)
                            break;
                    }
                    break;
                case OutputState.Limited:
                    while (output_counter < N)
                    {
                        Console.Write(pattern, f.Data);
                        f.Read();
                        output_counter++;
                        if (f.EOF)
                            break;
                    }
                    if (output_counter == N)
                        Environment.Exit(0);
                    break;
            }
            Console.WriteLine();

        }

        static void ReadTillSame(ref FileAdapter f1, ref FileAdapter f2, 
            string pattern, int N, OutputState ostate)
        {
            Console.Write("\n0x{0:X}: ", f1.Position);
            switch (ostate)
            {
                case OutputState.Unlimited:
                    while (true)
                    {
                        Console.Write(pattern, f1.Data, f2.Data);
                        f1.Read(); f2.Read();
                        if (f1.Data == f2.Data || f1.EOF || f2.EOF)
                            break;

                    }
                    break;
                case OutputState.Limited:
                    while (output_counter < N)
                    {
                        Console.Write(pattern, f1.Data, f2.Data);
                        f1.Read(); f2.Read();
                        output_counter++;
                        if (f1.Data == f2.Data || f1.EOF || f2.EOF)
                            break;
                    }
                    if (output_counter == N)
                        Environment.Exit(0);
                    break;
            }
        }

        static void UpdateFilesState(ref FilesState state, bool status1, bool status2)
        {
            if (status1 && !status2)
                state = FilesState.EOF1;
            if (!status1 && status2)
                state = FilesState.EOF2;
            if (status1 && status2)
                state = FilesState.EOFs;
        }

        static void CheckArgs(string[] args, out string filepath_1,
            out string filepath_2, ref int N, ref OutputState ostate)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: specify the names of comparing files");
                throw new Exception();
            }
            if (args.Length > 3)
            {
                Console.WriteLine("Error: too many arguments");
                throw new Exception();
            }
            filepath_1 = args[0];
            filepath_2 = args[1];
            if (args.Length == 3)
            {
                if (int.TryParse(args[2], out int result))
                {
                    N = result;
                    ostate = OutputState.Limited;
                }
                else
                {
                    Console.WriteLine("Error: N should be number");
                    throw new Exception();
                }
            }
        }

        static void FileReader(string filepath_1, string filepath_2, int N, OutputState ostate)
        {
            FileAdapter f1 = new FileAdapter(filepath_1);
            FileAdapter f2 = new FileAdapter(filepath_2);

            GlobalState state = GlobalState.Identical;
            FilesState fstate = FilesState.None;

            while (true)
            {
                switch (state)
                {
                    case GlobalState.Identical:
                        SkipSameBytes(ref f1, ref f2);
                        if (f1.EOF && f2.EOF)
                        {
                            Console.Write("Files identical\n");
                            return;
                        }
                        //update state
                        state = GlobalState.Differ;
                        break;
                    case GlobalState.Differ:
                        Console.Write("Files differ:\n");
                        while (true)
                        {
                            //update state
                            UpdateFilesState(ref fstate, f1.EOF, f2.EOF);
                            switch (fstate)
                            {
                                case FilesState.EOF1:
                                    //output and read until EOF2

                                    /*show EOF position */
                                    //Console.Write("\n0x{0:X}: ", f2.Position);
                                    ReadTillEOF(ref f2, "(0x{0:X}) ", "<EOF>", N, ostate);
                                    break;
                                case FilesState.EOF2:
                                    //output and read until EOF1

                                    /* show EOF position */
                                    //Console.Write("\n0x{0:X}: ", f1.Position);
                                    ReadTillEOF(ref f1, "0x{0:X} ", "(<EOF>) ", N, ostate);
                                    break;
                                case FilesState.EOFs:
                                    return;
                                default:
                                    //output, read until same bytes
                                    ReadTillSame(ref f1, ref f2, "0x{0:X}(0x{1:X}) ", N, ostate);
                                    SkipSameBytes(ref f1, ref f2);
                                    break;
                            }
                        }
                }
            }
        }

        static void Main(string[] args)
        {
            int N = 0;
            OutputState ostate = OutputState.Unlimited;
            try
            {
                CheckArgs(args, out string path_1, out string path_2, ref N, ref ostate);
                FileReader(path_1, path_2, N, ostate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
