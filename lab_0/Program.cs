using System;

namespace lab_0
{
    class Program
    {
        private static int N;
        private static int[] arr;
        private const int min_rand_value = -100;
        private const int max_rand_value = 100;

        private static void Input()
        {
            Console.WriteLine("Input N: ");
            N = int.Parse(Console.ReadLine());
        }

        private static void FillSequence()
        {
            arr = new int[N];
            Random r = new Random();
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i] = r.Next(min_rand_value, max_rand_value);
            }
        }

        private static void WriteSequence()
        {
            foreach (var i in arr)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Input();
            FillSequence();
            Console.WriteLine("Source sequence: ");
            WriteSequence();
            Sort.MergeSort(ref arr, 0, N - 1);
            //Sort.SelectionSort(ref arr, N - 1);
            Console.WriteLine("Sorted sequence: ");
            WriteSequence();
        }
    }

    static class Sort
    {

        static private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        
        static public void SelectionSort(ref int[] arr, int length)
        {
            for (int i = 0; i < length - 1; i++)
            {
                int m = i;
                for (int j = i + 1; j < length; j++)
                {
                    if (arr[j] < arr[m])
                        m = j;
                }
                if (m != i)
                    Swap(ref arr[m], ref arr[i]);
            }
        }

        static public void MergeSort(ref int[] arr, int a, int b)
        {
            if (a >= b)
                return;
            int m = (a + b) / 2;
            MergeSort(ref arr, a, m);
            MergeSort(ref arr, m + 1, b);
            int i = a;
            int j = m + 1;
            int[] temp = new int[(a + 1) + b];
            for (int k = a; k <= b; k++)
            {
                if (j > b || i <= m && arr[i] < arr[j])
                {
                    temp[k] = arr[i];
                    i += 1;
                }
                else
                {
                    temp[k] = arr[j];
                    j += 1;
                }
            }
            for (int k = a; k <= b; k++)
                arr[k] = temp[k];
        }
    }
}