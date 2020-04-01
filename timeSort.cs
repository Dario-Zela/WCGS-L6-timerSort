/*
 * Lower 6 C# Sorting template
 * 1) Read and understand the existing code and structure
 * 2) Comment the existing code
 * 3) Write and test the bubble() and merge() sort algorithms. 
 *    You may have to write/modify this in an exam
 * 4) Write and test the quick sort alogorithm - this is a quite a bit harder. 
 *    You may have to describe this algo in 
 * 5) Write and test the heap sort alogorithm - not part of the currciulum.
 *    This may be needed for your NEA.
 * Ensure that all code is commented.
 * When you have finished, commit the code back to github with a link to your repl.it code
 * so that it can be reviewed and executed.
 * 
 * My code lives here: <Insert repl.it link here>
 */


using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

class MainClass
{
    //Abstract class that works as an interface
    //Allows to iterate through the different sorting algorithms
    abstract class sorter
    {
        protected String name { get; }
        public String GetName() { return name; }

        public sorter(String name) { this.name = name; }
        public abstract void sort(int[] tosort);
    }

    //Simple bubble sort implementation
    class bubble : sorter
    {
        public bubble() : base("bubble") { }
        public override void sort(int[] tosort)
        {
            int lenght = tosort.Length;
            Action<int> swap = new Action<int>((index) =>
            {
                int temp = tosort[index + 1];
                tosort[index + 1] = tosort[index];
                tosort[index] = temp;
            });
            //Iterate through the array, swapping values until nothing is swapped
            bool swapped;
            do 
            {
                swapped = false;
                for (int i = 0; i < lenght - 1; i++)
                {
                    if(tosort[i] > tosort[i + 1])
                    {
                        swap(i);
                        swapped = true;
                    }
                }
            } 
            while (swapped);
            /*
            //Prints the array to the Console
            string output = "\n\n[";
            foreach (var item in tosort)
            {
                output += "\"" + item + "\"";
                if (item != tosort.Last())
                {
                    output += " , ";
                }
            }
            Console.WriteLine(output + "];");
            */
            return;
        }
    }

    //Simple merge sort implementation
    class merge : sorter
    {
        public merge() : base("merge") { }
        public override void sort(int[] tosort)
        {
            mergeSort(tosort);
            /*
            string output = "\n\n[";
            foreach (var item in tosort)
            {
                output += "\"" + item + "\"";
                if (item != tosort.Last())
                {
                    output += " , ";
                }
            }
            Console.WriteLine(output + "];");
            */
            return;
        }

        private int[] mergeSort(int[] tosort)
        {
            //Recursively split the array until the arrays are of only lenght one
            if (tosort.Length == 1) return tosort;
            int[] Right = mergeSort(tosort.Take(tosort.Length / 2).ToArray());
            int[] Left = mergeSort(tosort.Skip(tosort.Length / 2).ToArray());
            int i = 0;
            int j = 0;
            int currentIndex = 0;
            //Merging Algorithm
            do
            {
                if (i < Right.Length && j < Left.Length)
                {
                    if (Right[i] >= Left[j])
                    {
                        tosort[currentIndex] = Left[j];
                        j++;
                    }
                    else
                    {
                        tosort[currentIndex] = Right[i];
                        i++;
                    }
                    currentIndex++;
                }
                else
                {
                    if (i == Right.Length)
                    {
                        while (j < Left.Length)
                        {
                            tosort[currentIndex] =Left[j];
                            j++;
                            currentIndex++;
                        } 
                    }
                    else
                    {
                        while (i < Right.Length)
                        {
                            tosort[currentIndex] = Right[i];
                            i++;
                            currentIndex++;
                        }
                    }
                }
            }
            while (currentIndex != tosort.Length);
            return tosort;
        }
    }

    //Simple quick sort implementation
    class quick : sorter
    {
        public quick() : base("quick") { }

        public override void sort(int[] tosort)
        {
            quickSort(tosort, 0, tosort.Length - 1);
            /*
            string output = "\n\n[";
            foreach (var item in tosort)
            {
                output += "\"" + item + "\"";
                if (item != tosort.Last())
                {
                    output += " , ";
                }
            }
            Console.WriteLine(output + "];");
            */
            return;
        }

        private void quickSort(int[] tosort, int low, int high)
        {
            if (low < high)
            {
                //Create the pivot and move all items in respect to the pivot
                int partitionIndex = partition(tosort, low, high);
                //Reapply the algorithm to the values left and right of the pivot
                quickSort(tosort, low, partitionIndex - 1);
                quickSort(tosort, partitionIndex + 1, high);
            }
        }

        private int partition(int[] tosort, int low, int high)
        {
            Action<int,int> swap = new Action<int, int>((index, index2) =>
            {
                int temp = tosort[index2];
                tosort[index2] = tosort[index];
                tosort[index] = temp;
            });
            //The pivot is always the left most item to reduce complexity
            int pivot = tosort[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (tosort[j] < pivot)
                {
                    i++;
                    swap(i, j);
                }
            }
            swap(high, i + 1);
            return i + 1;
        }
    }

    //Simple heap sort implementation
    class heap : sorter
    {
        public heap() : base("heap") { }

        public override void sort(int[] tosort)
        {
            int n = tosort.Length;
            heapSort(tosort, n);
            /*
            string output = "\n\n[";
            foreach (var item in tosort)
            {
                output += "\"" + item + "\"";
                if (item != tosort.Last())
                {
                    output += " , ";
                }
            }
            Console.WriteLine(output + "];");
            */
            return;
        }

        private void heapSort(int[] tosort, int n)
        {
            Action<int, int> swap = new Action<int, int>((index, index2) =>
            {
                int temp = tosort[index2];
                tosort[index2] = tosort[index];
                tosort[index] = temp;
            });
            //Construct max heap
            for (int i = n / 2 - 1; i >= 0; i--) heapify(tosort, n, i);
            //keep on swapping the two extreme values and reheapify the binary tree
            for (int i = n - 1; i >= 0; i--)
            {
                swap(0, i);
                heapify(tosort, i, 0);
            }
        }

        private void heapify(int[] tosort, int n, int i)
        {
            Action<int, int> swap = new Action<int, int>((index, index2) =>
            {
                int temp = tosort[index2];
                tosort[index2] = tosort[index];
                tosort[index] = temp;
            });
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * (i + 1);

            if (left < n && tosort[left] > tosort[largest]) largest = left;
            if (right < n && tosort[right] > tosort[largest]) largest = right;

            if(largest != i)
            {
                swap(i, largest);
                heapify(tosort, n, largest);
            }
        }
    }

    //Simple insertion sort
    class insert : sorter
    {
        public insert() : base("insert") { }

        public override void sort(int[] tosort)
        {
            insertsort(tosort);
            /*
            string output = "\n\n[";
            foreach (var item in tosort)
            {
                output += "\"" + item + "\"";
                if (item != tosort.Last())
                {
                    output += " , ";
                }
            }
            Console.WriteLine(output + "];");
            */
        }

        public void insertsort(int[] tosort)
        {
            Action<int, int> swap = new Action<int, int>((index, index2) =>
            {
                int temp = tosort[index2];
                tosort[index2] = tosort[index];
                tosort[index] = temp;
            });
            int length = tosort.Length;
            for (int i = 0; i < length; i++)
            {
                int pos = i;
                while (pos > 0 && tosort[pos] < tosort[pos - 1])
                {
                    swap(pos, pos - 1);
                    pos--;
                }
            }
        }
    }

    public static void Main()
    {
        Stopwatch sw = new Stopwatch();

        List<sorter> sorters = new List<sorter>();

        //
        // Three sets of data to test with your sorter algorithms courtesy of random.com
        //

        int[][] data =
          {
    /* 10 items = short */
    new int[] { 14, 1, 15, 17, 20, 13, 2, 8, 5, 3},
    /* 50 items = a bit longer */
    new int[] { 83, 8, 133, 156, 199, 92, 194, 52, 152, 197, 66, 154, 170, 138, 47, 130, 163, 106, 172, 128, 113, 181, 135, 15, 69, 182, 160, 140, 159, 200, 112, 169, 91, 65, 55, 131, 33, 63, 40, 150, 161, 9, 39, 62, 78, 145, 20, 32, 178, 94},
    /* 100 = should sort out the algos */
    new int[] { 488, 243, 78, 486, 463, 418, 175, 306, 59, 90, 331, 13, 298, 50, 257, 448, 218, 464, 467, 356, 1, 120, 434, 98, 371, 154, 493, 270, 164, 96, 302, 237, 457, 299, 361, 38, 292, 60, 262, 128, 312, 136, 122, 310, 153, 80, 167, 93, 52, 296, 408, 11, 482, 39, 106, 475, 174, 181, 289, 31, 73, 274, 411, 178, 244, 316, 368, 201, 63, 221, 57, 236, 14, 235, 461, 47, 79, 10, 112, 421, 349, 211, 182, 319, 226, 375, 176, 111, 314, 108, 209, 238, 103, 304, 190, 255, 452, 422, 7, 500 },
    new int[10000]
        };

        Random rng = new Random();
        for(int i = 0; i < 10000; i++)
        {
            data[3][i] = rng.Next(10000);
        }
        
        //
        // Single Core - Implement the bubble and merge sort algorithms
        //

        sorters.Add(new bubble());
        sorters.Add(new merge());

        //
        // Dual Core - Implement Quick sort
        sorters.Add (new quick());

        // 
        // Quad Core - Implement heap sort
        //
        sorters.Add (new heap());

        //
        //Octa Core - Implement insert sort
        //
        sorters.Add(new insert());

        // Iterate through all the sort routines on the three sets of data to compare the alogorithm speed
        foreach (sorter s in sorters)
        {
            for (int x = 0; x < data.Length; x++)
            {
                sw.Reset();
                sw.Start();
                s.sort(data[x]);
                sw.Stop();
                Console.WriteLine("{1} sort, list length {2}, Elapsed={0}", sw.Elapsed, s.GetName(), data[x].Length);
            }
        }
        Console.ReadKey();
    }
}
