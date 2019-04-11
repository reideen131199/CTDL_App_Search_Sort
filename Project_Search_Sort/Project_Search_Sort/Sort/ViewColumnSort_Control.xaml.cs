using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;
using System.Collections;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for ViewColumn_Control.xaml
    /// </summary>
    public partial class ViewColumnSort_Control : UserControl
    {
        //private int[] arr;
        private int size;
        private int time = 100;
        private Column_Control[] columns;

        // Get Set
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }

        #region Constructor

        /// <summary>
        /// Create View Column Control
        /// </summary>
        /// <param name="a">Array need show</param>
        public ViewColumnSort_Control(int[] a)
        {
            InitializeComponent();

            int[] dest = new int[a.Length + 1];
            Array.Copy(a, 0, dest, 1, a.Length);

            // CreateCol
            size = (dest.Length < 1) ? 0 : dest.Length - 1;
            columns = new Column_Control[size + 1];

            for (int i = 1; i <= size; i++)
            {
                columns[i] = new Column_Control();
                columns[i].col.Val = dest[i];
                Canvas.SetBottom(columns[i], 0);
                Canvas.SetLeft(columns[i], (i - 1) * 40);
                LayoutAnimation.Children.Add(columns[i]);
            }

            LayoutAnimation.Width = size * 40;
        }

        #endregion

        #region Algorithm Sort      Canvas.Bottom: -280(Insert Sort, Merge)

        private double Bot = -280;

        #region Bubble Sort
        /// <summary>
        /// Bubble Sort
        /// </summary>
        /// <param name="k">If k=1, Increasing. If k=0, Decreasing</param>
        public async Task BubbleSort(int k)
        {
            for (int i = 1; i < size; i++)
            {
                for (int j = i + 1; j <= size; j++)
                {
                    columns[i].col.BgCompare();
                    columns[j].col.BgCompare();
                    await Task.Delay(time + 100);

                    if (CompareValue(columns[i].col.Val, columns[j].col.Val, k)) //Swap(ref arr[i], ref arr[j]);
                    {
                        // Swap
                        AnimationColumn.ExchangeColX(columns[i], columns[j], time);
                        Column_Control temp = columns[i];
                        columns[i] = columns[j];
                        columns[j] = temp;
                        await Task.Delay(time + 100);
                    }

                    columns[j].col.BgDefault();
                    columns[j].col.BgDefault();
                }
                columns[i].col.BgLock();
            }
            columns[size].col.BgLock();
        }
        #endregion

        #region Selection
        /// <summary>
        /// Selection Sort
        /// </summary>
        /// <param name="k">If k=1, Increasing. If k=0, Decreasing</param>
        public async Task SelectionSort(int k)
        {
            int ValueTemp;
            for (int i = 1; i < size; i++)
            {
                ValueTemp = i;
                columns[ValueTemp].col.BgTemp();

                for (int j = i + 1; j <= size; j++)
                {
                    columns[j].col.BgCompare();
                    await Task.Delay(time + 100);

                    if (CompareValue(columns[ValueTemp].col.Val, columns[j].col.Val, k))
                    {
                        columns[ValueTemp].col.BgDefault();
                        ValueTemp = j;
                        columns[ValueTemp].col.BgTemp();
                    }
                    else
                        columns[j].col.BgDefault();
                }

                if (ValueTemp != i)
                {
                    // Swap
                    AnimationColumn.ExchangeColX(columns[i], columns[ValueTemp], time);
                    Column_Control ControlTemp = columns[i];
                    columns[i] = columns[ValueTemp];
                    columns[ValueTemp] = ControlTemp;
                    await Task.Delay(time + 100);
                }
                columns[i].col.BgLock();
            }
            columns[size].col.BgLock();
        }

        #endregion

        #region Insert
        /// <summary>
        /// Insert Sort
        /// </summary>
        /// <param name="k">If k=1, Increasing. If k=0, Decreasing</param>
        public async Task InsertionSort(int k)
        {
            columns[1].col.BgLock();
            for (int i = 2; i <= size; i++)
            {
                Column_Control key = columns[i];
                key.col.BgCompare();
                AnimationColumn.MoveColY(key, Bot, time);
                await Task.Delay(time + 100);

                int count = i;
                while (count - 1 > 0 && CompareValue(columns[count - 1].col.Val, key.col.Val, k))
                {
                    count--;
                    AnimationColumn.ExchangeColX(key, columns[count], time);
                    await Task.Delay(time + 100);
                    columns[count + 1] = columns[count];
                }

                columns[count] = key;
                AnimationColumn.MoveColY(key, 0, time);
                key.col.BgLock();
                await Task.Delay(time + 100);
            }
        }
        #endregion

        #region Quick Sort          Chua khoa
        /// <summary>
        /// Quick Sort
        /// </summary>
        public async Task QuickSort()
        {
            Queue left = new Queue(), right = new Queue();
            left.Enqueue(1);
            right.Enqueue(size);
            int l, r, i, j;
            Column_Control m = new Column_Control();

            while (left.Count != 0)
            {
                l = (int)left.Dequeue();
                r = (int)right.Dequeue();
                m = columns[(l + r) / 2];
                i = l;
                j = r;

                m.col.BgKey();

                BlockCompare.Text = m.col.Val.ToString();
                while (i <= j)
                {
                    columns[i].col.BgCompare();

                    while (columns[i].col.Val < m.col.Val)
                    {
                        await Task.Delay(time);
                        columns[i].col.BgDefault();
                        i++;
                        columns[i].col.BgCompare();
                    }
                    await Task.Delay(time);

                    columns[j].col.BgCompare();
                    while (columns[j].col.Val > m.col.Val)
                    {
                        await Task.Delay(time);
                        columns[j].col.BgDefault();
                        j--;
                        columns[j].col.BgCompare();
                    }
                    await Task.Delay(time);

                    if (i <= j)
                    {
                        if (i < j)
                        {
                            AnimationColumn.ExchangeColX(columns[i], columns[j], time);
                            Column_Control ControlTemp = columns[i];
                            columns[i] = columns[j];
                            columns[j] = ControlTemp;
                            await Task.Delay(time + 100);
                        }
                        columns[i].col.BgDefault();
                        columns[j].col.BgDefault();
                        i++;
                        j--;
                    }

                    for (int z = 1; z <= size; z++)
                        if (columns[z].col.CheckBgCompare())
                            columns[z].col.BgDefault();
                }

                //m.col.BgLock();
                m.col.BgDefault();

                if (l == j) columns[l].col.BgLock();

                if (l > j)
                {
                    columns[l].col.BgLock();
                    columns[l - 1].col.BgLock();    // Error
                    columns[j].col.BgLock();
                }

                if (l < j)
                {
                    left.Enqueue(l);
                    right.Enqueue(j);
                }


                if (i == r) columns[i].col.BgLock();

                if (r < i)
                {
                    columns[r].col.BgLock();
                    columns[i - 1].col.BgLock();
                    columns[i].col.BgLock();
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
        public async Task ShellSort()
        {
            int inc = 4;
            while (inc > 0)
            {
                for (int i = 1; i <= size; i++)
                {
                    Column_Control temp = columns[i];
                    bool Lock = false;
                    temp.col.BgCompare();

                    // If code dont run while
                    if (i - inc > 0 && columns[i - inc].col.Val <= temp.col.Val)
                    {
                        if (columns[i - inc].col.CheckBgLock()) Lock = true;

                        columns[i - inc].col.BgCompare();
                        await Task.Delay(time);

                        if (Lock)
                            columns[i - inc].col.BgLock();
                        else
                            columns[i - inc].col.BgDefault();
                    }

                    // While for insert temp
                    int j = i;
                    while ((j - inc > 0) && (columns[j - inc].col.Val > temp.col.Val))
                    {
                        Lock = false;
                        if (columns[j - inc].col.CheckBgLock()) Lock = true;

                        columns[j - inc].col.BgCompare();
                        await Task.Delay(time);

                        AnimationColumn.ExchangeColX(columns[j - inc], temp, time);
                        await Task.Delay(time + 100);

                        columns[j] = columns[j - inc];
                        if (Lock)
                            columns[j].col.BgLock();
                        else
                            columns[j].col.BgDefault();
                        j = j - inc;
                    }

                    // If compare value in While fail
                    if ((j - inc > 0) && (columns[j - inc].col.Val <= temp.col.Val))
                    {
                        Lock = false;
                        if (columns[j - inc].col.CheckBgLock()) Lock = true;

                        columns[j - inc].col.BgCompare();
                        await Task.Delay(time);

                        if (Lock)
                            columns[j - inc].col.BgLock();
                        else
                            columns[j - inc].col.BgDefault();
                    }


                    columns[j] = temp;
                    if (inc == 1)
                        temp.col.BgLock();
                    else
                        temp.col.BgDefault();
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

        #region Merge Sort

        public async Task DoMerge(int left, int mid, int right)
        {
            Column_Control[] temp = new Column_Control[size + 1];
            int left_end = (mid - 1);
            int tmp_pos = left;
            int num_elements = (right - left + 1);

            for (int i = left; i <= right; i++)
            {
                temp[i] = columns[i];
                columns[i].col.BgCompare();
                AnimationColumn.MoveColY(columns[i], Bot, time);
            }
            await Task.Delay(time);

            while ((left <= left_end) && (mid <= right))
            {
                if (temp[left].col.Val <= temp[mid].col.Val)
                {
                    AnimationColumn.MoveColX(temp[left], (tmp_pos-1) * 40, time);
                    AnimationColumn.MoveColY(temp[left], 0, time);
                    await Task.Delay(time + 100);
                    columns[tmp_pos++] = temp[left++];
                }
                else
                {
                    AnimationColumn.MoveColX(temp[mid], (tmp_pos - 1) * 40, time);
                    AnimationColumn.MoveColY(temp[mid], 0, time);
                    await Task.Delay(time + 100);
                    columns[tmp_pos++] = temp[mid++];
                }
            }

            while (left <= left_end)
            {
                AnimationColumn.MoveColX(temp[left], (tmp_pos - 1) * 40, time);
                AnimationColumn.MoveColY(temp[left], 0, time);
                await Task.Delay(time + 100);
                columns[tmp_pos++] = temp[left++];
            }
            
            while (mid <= right)
            {
                AnimationColumn.MoveColX(temp[mid], (tmp_pos - 1) * 40, time);
                AnimationColumn.MoveColY(temp[mid], 0, time);
                await Task.Delay(time + 100);
                columns[tmp_pos++] = temp[mid++];
            }
                
        } 

        public async Task MergeSort_Recursive(int left, int right)
        {
            if (right == -1) right = size;
            if (right > left)
            {
                int mid = (right + left) / 2; //Divide step
                await MergeSort_Recursive(left, mid);//Conquer step
                await MergeSort_Recursive((mid + 1), right);//Conquer step
                await DoMerge(left, (mid + 1), right);//Conquer step

                for (int i = left; i <= right; i++)
                    columns[i].col.BgKey();
                await Task.Delay(time);
            }
            if (left == 1 && right == size)
            {
                for (int i = left; i <= right; i++)
                    columns[i].col.BgLock();
            }
        }

        #endregion

        #region Counting       Add new Control
        /*
        /// <summary>
        /// Counting
        /// </summary>
        public async void CountingSort()
        {
            int[] count = new int[10];
            Column_Control[] copy = columns;
            int maxx = columns[1].col.Val;

            // Create Col Count. 1 to 9
            Canvas layoutCount = new Canvas();
            layoutCount.Width = 9 * 40;
            Grid.SetRow(layoutCount, 2);

            TextBlock[] textBlock = new TextBlock[10];
            for (int i = 1; i <= 9; i++)
            {
                textBlock[i] = new TextBlock();
                textBlock[i] = createTextValueCol(i.ToString(), (i - 1) * 40);
                layoutCount.Children.Add(textBlock[i]);
            }
            Wrap.Children.Add(layoutCount);
            BlockCompare.Text = layoutCount.ActualHeight.ToString();

            for (int i = 1; i <= size; i++)
            {
                count[columns[i].col.Val]++;
                if (copy[i].col.Val > maxx) maxx = copy[i].col.Val;
            }

            for (int i = 1; i <= maxx; i++)
            {
                count[i] += count[i - 1];
            }

            columns = new Column_Control[size + 2];

            for (int i = 1; i <= size; i++) //8,2,3,2,6
            {
                columns[count[copy[i].col.Val]] = copy[i];
                count[copy[i].col.Val] -= 1;
            }

            //arr = count;
        }

        private TextBlock createTextValueCol(string text, double posLeft)
        {
            TextBlock t = new TextBlock();
            t.Text = text;
            t.FontSize = 24;
            t.Width = 30;
            t.TextAlignment = System.Windows.TextAlignment.Center;
            Canvas.SetBottom(t, 0);
            Canvas.SetLeft(t, posLeft);
            return t;
        }
        */
        #endregion

        #endregion

        #region Helper
        
        /// <summary>
        /// Compare between 2 Value
        /// </summary>
        /// <param name="val1">Value 1</param>
        /// <param name="val2">Value 2</param>
        /// <param name="type">If type=1, val1 > val2. If type=0, val1 ... val2 (ahihi) </param>
        /// <returns></returns>
        private bool CompareValue(int val1, int val2, int type)
        {
            if (type == 1)
                return val1 > val2;
            return val1 < val2;
        }

        /// <summary>
        /// BgLock for all columns
        /// </summary>
        public void LockAll()
        {
            for (int i = 1; i <= size; i++)
                columns[i].col.BgLock();
        }
        #endregion
    }
}
