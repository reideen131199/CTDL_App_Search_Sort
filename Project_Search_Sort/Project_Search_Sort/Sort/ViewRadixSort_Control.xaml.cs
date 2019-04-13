using System;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for ViewRadixSort_Control.xaml
    /// </summary>
    public partial class ViewRadixSort_Control : UserControl
    {
        private int[] arr;
        private int size;
        private int time = 100;
        private Radix_Control[] radixs;
        private bool pause = false;

        // Get Set
        public int Time
        {
            get { return time; }
            set { time = 1100 - value; }
        }

        public bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }

        #region Constructor

        public ViewRadixSort_Control(int[] a)
        {
            InitializeComponent();

            int[] dest = new int[a.Length + 1];
            Array.Copy(a, 0, dest, 1, a.Length);

            // CreateCol
            size = (dest.Length < 1) ? 0 : dest.Length - 1;
            radixs = new Radix_Control[size + 1];

            for (int i = 1; i <= size; i++)
            {
                radixs[i] = new Radix_Control();
                radixs[i].radix.Val = dest[i];
                Canvas.SetBottom(radixs[i], 200);
                Canvas.SetLeft(radixs[i], (i - 1) * 72);
                LayoutAnimation.Children.Add(radixs[i]);
            }

            LayoutAnimation.Width = size * 72;

            arr = dest;
        }

        #endregion

        #region Algorithm Radix Sort

        private double Bot = -280;

        /// <summary>
        /// Radix
        /// </summary>
        public async void RadixSort()
        {
            int i, j;
            int[] tmp = new int[arr.Length];
            Radix_Control[] tmpRadix = new Radix_Control[arr.Length];

            for (int shift = 31; shift > -1; --shift)
            {
                j = 0;
                for (i = 1; i <= size; ++i)
                {
                    bool move = (arr[i] << shift) >= 0;

                    if (shift == 0 ? !move : move)
                    {
                        if (i != 1)
                        {
                            AnimationControl.MoveColX(radixs[i], (i - j - 1) * 72, time);
                            if (j!=0) await Task.Delay(time + 100);
                        }
                        radixs[i - j] = radixs[i];
                        arr[i - j] = arr[i];
                    }
                    else
                    {
                        AnimationControl.MoveColY(radixs[i], Bot, time);
                        await Task.Delay(time+100);

                        tmpRadix[j] = radixs[i];
                        tmp[j++] = arr[i];
                    }
                        

                }
                //Array.Copy(tmp, 0, arr, arr.Length - j, j);

                int n = size + 1;
                for (int k = n - j; k < n; k++)
                {
                    radixs[k] = tmpRadix[k - n + j];
                    arr[k] = tmp[k - n + j];

                    AnimationControl.MoveColX(radixs[k], (k - 1) * 72, time);
                    AnimationControl.MoveColY(radixs[k], 200, time);
                    await Task.Delay(time + 100);
                }

            }

            BlockCompare.Text = string.Join(" ", arr);
        }

        #endregion

        #region Helper

        /// <summary>
        /// Remove All Columns
        /// </summary>
        private void removeAllCol()
        {
            for (int i = 1; i <= size; i++)
                ;// LayoutAnimation.Children.Remove(columns[i]);
        }

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
                ;       
        }

        /// <summary>
        /// Function Pause
        /// </summary>
        /// <returns></returns>
        private async Task PauseAnimation()
        {
            while (pause) { await Task.Delay(500); }
        }

        #endregion

    }
}
