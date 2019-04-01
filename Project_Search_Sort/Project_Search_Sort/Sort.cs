using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllSort
{
    class Sort
    {
        static void Main(string[] args) { }

        private int size;
        private int[] arr;


        // normal contruct 
        public Sort()
        {
            size = 0;
            arr = new int[1001];
        }
        // extra contruct
        public Sort(int m, int[] b)
        {
            size = m;
            arr = new int[size + 1];
            for (int i = 1; i <= size; i++) arr[i] = b[i];
        }


        // Nhap du lieu
        public void Input(int m, int[] b)
        {
            size = m;
            for (int i = 1; i <= size; i++) arr[i] = b[i];
        }


        // Get
        public int getSize()
        {
            return size;
        }
        public int[] getArray()
        {
            return arr;
        }


        // Doi cho
        public void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }


        // Luu y: khi sort, muon sort tu nho den lon thi nhap k = 1


        // Bubble
        public void BubbleSort(int k)
        {
            if (k == 1)
            {
                for (int i = 1; i < size; i++)
                {
                    for (int j = i + 1; j <= size; j++)
                    {
                        if (arr[i] > arr[j]) Swap(ref arr[i], ref arr[j]);
                    }
                }
            }
            else
            {
                for (int i = 1; i < size; i++)
                {
                    for (int j = i + 1; j <= size; j++)
                    {
                        if (arr[i] < arr[j]) Swap(ref arr[i], ref arr[j]);
                    }
                }
            }
        }


        // Select
        public void SelectionSort(int k)
        {
            int max, min;
            if (k == 1)
            {
                for (int i = 1; i < size; i++)
                {
                    min = i;
                    for (int j = i + 1; j <= size; j++)
                    {
                        if (arr[j] < arr[min]) min = j;
                    }
                    if (min != i) Swap(ref arr[min], ref arr[i]);
                }
            }
            else
            {
                for (int i = 1; i < size; i++)
                {
                    max = i;
                    for (int j = i + 1; j <= size; j++)
                    {
                        if (arr[j] > arr[max]) max = j;
                    }
                    if (max != i) Swap(ref arr[max], ref arr[i]);
                }
            }
        }


        // Insert
        public void InsertionSort(int k)
        {
            for (int i = 1; i <= size; i++)
            {
                int h = arr[i];
                for (int j = i - 1; j >= 1; j--)
                {
                    if (k == 1)
                    {
                        if (h >= arr[j])
                        {
                            arr[j + 1] = h;
                            break;
                        }
                        else arr[j + 1] = arr[j];
                    }
                    else
                    {
                        if (h <= arr[j])
                        {
                            arr[j + 1] = h;
                            break;
                        }
                        else arr[j + 1] = arr[j];
                    }
                }
            }
        }


        // Counting
        public void CountingSort()
        {
            int[] count = new int[1000001];
            int[] copy = arr;

            for (int i=1; i<=size; i++)
            {
                count[arr[i]]++;
            }

            for (int i = 1; i <= size; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = 1; i <= size; i++)
            {
                arr[count[copy[i]]] = copy[i];
                count[i]--;
            }
        }


        // Quick
        public void QuickSort()
        {
            Queue left = new Queue(), right = new Queue();
            left.Enqueue(1);
            right.Enqueue(size);
            int l, r, m, i, j;
            while (left.Peek() != null)
            {
                l = (int)left.Dequeue();
                r = (int)right.Dequeue();
                m = arr[(l + r) / 2];
                i = l;
                j = r;

                while (i <= j)
                {
                    while (arr[i] < m) i++;
                    while (arr[j] > m) j--;
                    if (i <= j)
                    {
                        if (i < j) Swap(ref arr[i], ref arr[j]);
                        i++;
                        j--;
                    }
                }

                if (l < j)
                {
                    left.Enqueue(l);
                    right.Enqueue(j);
                }
                if (i < r)
                {
                    left.Enqueue(i);
                    right.Enqueue(r);
                }
            }
        }


        // Shell
        public void ShellSort()
        {
            int i, j, inc, temp;
            inc = 3;
            while (inc > 0)
            {
                for (i = 0; i < size; i++)
                {
                    j = i;
                    temp = arr[i];
                    while ((j >= inc) && (arr[j - inc] > temp))
                    {
                        arr[j] = arr[j - inc];
                        j = j - inc;
                    }
                    arr[j] = temp;
                }
                if (inc / 2 != 0)
                    inc = inc / 2;
                else if (inc == 1)
                    inc = 0;
                else
                    inc = 1;
            }
        }


        // Merge
        public void MergeSort()
        {

        }


        // Heap
        public void HeapSort()
        {

        }


        // Radix
        public void RadixSort()
        {
            int i, j;
            int[] tmp = new int[arr.Length];
            for (int shift = 31; shift > -1; --shift)
            {
                j = 0;
                for (i = 0; i < arr.Length; ++i)
                {
                    bool move = (arr[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)   
                        arr[i-j] = arr[i];
                    else                             
                        tmp[j++] = arr[i];
                }
                Array.Copy(tmp, 0, arr, arr.Length-j, j);
            }
        }
    }

}
