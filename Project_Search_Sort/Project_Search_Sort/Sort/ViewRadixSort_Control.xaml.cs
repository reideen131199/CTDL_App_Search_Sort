using System;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for ViewRadixSort_Control.xaml
    /// </summary>
    public partial class ViewRadixSort_Control : UserControl
    {
        #region Private Value

        private int[] arr;
        private int size;
        private int time = 100;
        private Radix_Control[] radixs;
        private bool pause = false;

        private double PosBot = -280;
        private double PosMid = -30;
        private double PosTop = 200;

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

        #endregion

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
                Canvas.SetBottom(radixs[i], PosTop);
                Canvas.SetLeft(radixs[i], (i - 1) * 72);
                LayoutAnimation.Children.Add(radixs[i]);
            }

            LayoutAnimation.Width = size * 72;

            arr = dest;
        }

        #endregion

        #region Algorithm

        #region Radix Sort

        /// <summary>
        /// Radix
        /// </summary>
        public async void RadixSort()
        {
            ChangedFormRadix();

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
                        AnimationControl.MoveColY(radixs[i], PosBot, time);
                        await Task.Delay(time+100);

                        tmpRadix[j] = radixs[i];
                        tmp[j++] = arr[i];
                    }
                }
                
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

        #region Counting Sort
        
        /// <summary>
        /// Counting Sort
        /// </summary>
        public async void CountingSort()
        {
            ChangedFormCounting();
            
            Radix_Control[] count = new Radix_Control[10];
            Radix_Control[] copy = new Radix_Control[size + 1];
            int maxx = radixs[1].radix.Val;

            #region Create Array Copy and Row Count

            // CreateRowCount
            count[0] = new Radix_Control();
            for (int i = 1; i < 10; i++)
            {
                count[i] = new Radix_Control();
                count[i].FormCounting();
                Canvas.SetLeft(count[i], (size - 9) * 72 /2 + (i - 1) * 72);
                Canvas.SetBottom(count[i], PosMid);

                LayoutAnimation.Children.Add(count[i]);
            }
            CreateIndexRowCount();

            // Copy Array radixs
            for (int i = 1; i <= size; i++)
            {
                copy[i] = CopyRadixControl(radixs[i]);
            }

            #endregion

            #region Algorithm

            for (int i = 1; i <= size; i++)
            {
                double posX = (size - 9) * 72 / 2 + (radixs[i].radix.Val - 1) * 72;
                LayoutAnimation.Children.Add(copy[i]);

                AnimationControl.MoveColX(radixs[i], posX, time);
                AnimationControl.MoveColY(radixs[i], PosMid, time);
                await Task.Delay(time + 100);

                LayoutAnimation.Children.Remove(radixs[i]);

                count[radixs[i].radix.Val].radix.Val++;
                if (copy[i].radix.Val > maxx) maxx = copy[i].radix.Val;
            }

            for (int i = 1; i <= maxx; i++)
            {
                count[i].radix.Val += count[i - 1].radix.Val;
                await Task.Delay(time);
            }

            radixs = new Radix_Control[size + 1];
            for (int i = 1; i <= size; i++) //8,2,3,2,6
            {
                radixs[i] = CopyRadixControl(copy[i]);
                LayoutAnimation.Children.Add(radixs[i]);

                // Move to Row Count
                double posX = (size - 9) * 72 / 2 + (radixs[i].radix.Val - 1) * 72;
                AnimationControl.MoveColX(radixs[i], posX, time);
                AnimationControl.MoveColY(radixs[i], PosMid, time);
                await Task.Delay(time + 100);

                // Move to Row Bottom
                posX = (count[copy[i].radix.Val].radix.Val - 1) * 72;
                AnimationControl.MoveColX(radixs[i], posX, time);
                AnimationControl.MoveColY(radixs[i], PosBot, time);
                await Task.Delay(time + 100);

                count[copy[i].radix.Val].radix.Val -= 1;
            }

            #endregion

            #region Print result

            LayoutCount.Children.Clear();
            for (int i = 1; i <= 9; i++)
                LayoutAnimation.Children.Remove(count[i]);
            for (int i = 1; i <= size; i++)
            {
                LayoutAnimation.Children.Remove(copy[i]);
                AnimationControl.MoveColY(radixs[i], PosMid, time);
            }

            #endregion
        }

        /// <summary>
        /// Create New Radix_Control from Old Radix_Control of Counting Sort
        /// </summary>
        /// <param name="radix_Control">Radix_Control need Copy</param>
        /// <returns></returns>
        private Radix_Control CopyRadixControl(Radix_Control radix_Control)
        {
            Radix_Control copy = new Radix_Control(radix_Control.radix);
            Canvas.SetBottom(copy, Canvas.GetBottom(radix_Control));
            Canvas.SetLeft(copy, Canvas.GetLeft(radix_Control));
            copy.FormCounting();
            return copy;
        }

        /// <summary>
        /// Create Layout Index for Array Count of Counting Sort
        /// </summary>
        private void CreateIndexRowCount()
        {
            // Create Index
            for (int i = 1; i <= 9; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = i.ToString();
                textBlock.FontSize = 24;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.Width = 70;
                Canvas.SetLeft(textBlock, (i - 1) * 72);
                Canvas.SetBottom(textBlock, 0);

                LayoutCount.Children.Add(textBlock);
            }
            LayoutCount.Width = 9 * 72;


        }
        
        #endregion

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

        /// <summary>
        /// Change Control of View to Form Radix
        /// </summary>
        public void ChangedFormRadix()
        {
            for (int i = 1; i <= size; i++)
                radixs[i].FormRadix();
        }

        /// <summary>
        /// Change Control of View to Form Counting
        /// </summary>
        public void ChangedFormCounting()
        {
            for (int i = 1; i <= size; i++)
                radixs[i].FormCounting();
        }

        #endregion

    }
}
