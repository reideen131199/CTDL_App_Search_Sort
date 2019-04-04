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
        private int size;
        private int[] arr;

        #region Constructor

        /// <summary>
        ///  Normal contruct 
        /// </summary>
        public Sort()
        {
            size = 0;
            arr = new int[1001];
        }

        /// <summary>
        /// extra contruct
        /// </summary>
        /// <param name="m">is size of array</param>
        /// <param name="b">is array, which sort</param>
        public Sort(int m, int[] b)
        {
            size = m;
            arr = new int[size + 1];
            for (int i = 1; i <= size; i++) arr[i] = b[i];
        }
        #endregion

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

        #region Bubble Sort
        /// <summary>
        /// Bubble Sort
        /// </summary>
        /// <param name="k"></param>
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
        #endregion

        #region Selection Sort
        /// <summary>
        /// Selection Sort
        /// </summary>
        /// <param name="k"></param>
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
        #endregion

        #region Insert
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="k"></param>
        public void InsertionSort(int k)
        {
            for (int i = 2; i <= size; i++)
            {
                int h = arr[i];
                for (int j = 1; j <= i-1; j++)
                {
                    if (k == 1)
                    {
                        if (h >= arr[j])
                        {
                            for (int l = i; l > j; l--) arr[l] = arr[l-1];
                            arr[j] = h;
                            break;
                        }
                    }
                    else
                    {
                        if (h <= arr[j])
                        {
                            for (int l = i; l > j; l--) arr[l] = arr[l - 1];
                            arr[j] = h;
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        #region Counting
        /// <summary>
        /// Counting
        /// </summary>
        public void CountingSort()
        {
            int[] count = new int[1001];
            int[] copy = arr;
            int maxx = -100000000;

            for (int i=1; i<=size; i++)
            {
                count[arr[i]]++;
                if (copy[i] > maxx) maxx = copy[i];
            }

            for (int i = 1; i <= maxx; i++)
            {
                count[i] += count[i - 1];
            }

            arr = new int[101];

            for (int i = 1; i <= size; i++)
            {
                arr[count[copy[i]]] = copy[i];
                count[copy[i]] -= 1;
            }

            //arr = count;
        }
        #endregion

        #region Quick Sort
        /// <summary>
        /// Quick Sort
        /// </summary>
        public void QuickSort()
        {
            Queue left = new Queue(), right = new Queue();
            left.Enqueue(1);
            right.Enqueue(size);
            int l, r, m, i, j;
            
            while (left.Count != 0)
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
        #endregion

        #region Shell Sort
        /// <summary>
        /// Shell Sort
        /// </summary>
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
        #endregion

        #region Radix Sort
        /// <summary>
        /// Radix
        /// </summary>
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
                        arr[i - j] = arr[i];
                    else
                        tmp[j++] = arr[i];
                }
                Array.Copy(tmp, 0, arr, arr.Length - j, j);
            }
        }
        #endregion

        #region Merge
        /// <summary>
        /// Merge
        /// </summary>
        public void MergeSort()
        {
            MergeSort ms = new MergeSort();
            ms.Input(arr);
            ms.MergeSort_Recursive(1, size);
            arr = ms.Output();
        }
        #endregion

        // Heap
        public void HeapSort()
        {
            HeapSort hs = new HeapSort();
            hs.Input(arr);
            hs.PerformHeapSort(size);
            arr = hs.Output();
        }
    }




    class HeapSort
    {
        private int heapSize;
        private int[] arr;

        public void Input(int[] a)
        {
            arr = a;
        }

        public int[] Output()
        {
            return arr;
        }

        private void BuildHeap(int size)
        {
            heapSize = size;
            for (int i = heapSize / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        private void Swap(int x, int y)//function to swap elements
        {
            int temp = arr[x];
            arr[x] = arr[y];
            arr[y] = temp;
        }

        private void Heapify(int index)
        {
            int left = 2 * index;
            int right = 2 * index + 1;
            int largest = index;

            if (left <= heapSize && arr[left] > arr[index])
            {
                largest = left;
            }

            if (right <= heapSize && arr[right] > arr[largest])
            {
                largest = right;
            }

            if (largest != index)
            {
                Swap(index, largest);
                Heapify(largest);
            }
        }

        public void PerformHeapSort(int size)
        {
            BuildHeap(size);
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                Swap(0, i);
                heapSize--;
                Heapify(0);
            }
            //DisplayArray();
        }

        private void DisplayArray()
        {
            for (int i = 0; i < arr.Length; i++)
            { Console.Write("[{0}]", arr[i]); }
        }
    }






    class MergeSort
    {
        private int[] numbers;

        public void Input(int[] arr)
        {
            numbers = arr;
        }

        public int[] Output()
        {
            return numbers;
        }

        public void DoMerge(int left, int mid, int right)
        {

            int[] temp = new int[25];

            int i, left_end, num_elements, tmp_pos;

            left_end = (mid - 1);

            tmp_pos = left;

            num_elements = (right - left + 1);

            while ((left <= left_end) && (mid <= right))
            {

                if (numbers[left] <= numbers[mid])
                {
                    temp[tmp_pos++] = numbers[left++];
                }
                else
                {
                    temp[tmp_pos++] = numbers[mid++];
                }
            }

            while (left <= left_end)
            {
                temp[tmp_pos++] = numbers[left++];
            }

            while (mid <= right)
            {
                temp[tmp_pos++] = numbers[mid++];
            }

            for (i = 0; i < num_elements; i++)
            {

                numbers[right] = temp[right];
                right--;

            }

        }

        public void MergeSort_Recursive(int left, int right)
        {

            int mid;

            if (right > left)

            {

                mid = (right + left) / 2; //Divide step

                MergeSort_Recursive(left, mid);//Conquer step

                MergeSort_Recursive((mid + 1), right);//Conquer step

                DoMerge(left, (mid + 1), right);//Conquer step

            }

        }
    }



}
