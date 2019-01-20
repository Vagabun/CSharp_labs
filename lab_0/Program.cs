using System;

namespace lab_0
{
    class Program
    {   
        static void Main(string[] args)
        {
            Sort sort = new Sort(0, 100, 10);
            sort.FillArray();
            Console.Write("unsorted array: ");
            sort.WriteArray();
            sort.MergeSort(0, 9);
//            sort.SelectionSort();
            Console.Write("sorted array: ");
            sort.WriteArray();
        }
    }

    class Sort
    {
        private readonly int min_rand_value;
        private readonly int max_rand_value;
        private readonly int arr_size;
        private int[] arr;
        
        //utils
        public void FillArray()
        {
            Random r = new Random();
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i] = r.Next(min_rand_value, max_rand_value);
            }
        }

        private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public void WriteArray()
        {
            foreach (var i in arr)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
        }
        
        public Sort(int min_rand, int max_rand, int size)
        {
            min_rand_value = min_rand;
            max_rand_value = max_rand;
            arr_size = size;
            arr = new int[arr_size];
        }
        
        public void SelectionSort()
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int m = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[m])
                        m = j;
                }
                if (m != i)
                    Swap(ref arr[m], ref arr[i]);
            }
        }

        public void MergeSort(int a, int b)
        {
            if (a >= b)
                return;
            int m = (a + b) / 2;
            MergeSort(a, m);
            MergeSort(m + 1, b);
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