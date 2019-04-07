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
    public partial class ViewColumn_Control : UserControl
    {
        //private int[] arr;
        private int size;
        private int time = 100;
        private Column_Control[] columns;

        // Get Set
        public int Time { get; set; }

        #region Constructor

        /// <summary>
        /// Create View Column Control
        /// </summary>
        /// <param name="a">Array need show</param>
        public ViewColumn_Control(int[] a)
        {
            InitializeComponent();

            int[] dest = new int[a.Length + 1];
            Array.Copy(a, 0, dest, 1, a.Length);

            CreateCol(dest);
        }

        private void CreateCol(int[] arr)
        {
            size = (arr.Length < 1) ? 0 : arr.Length - 1;
            columns = new Column_Control[size + 1];

            LayoutAnimation.Width = size * 40;
            for (int i=1; i<=size; i++)
            {
                columns[i] = new Column_Control();
                columns[i].col.Val = arr[i];
                Canvas.SetBottom(columns[i], 0);
                Canvas.SetLeft(columns[i], (i-1) * 40);
                LayoutAnimation.Children.Add(columns[i]);
            }
        }

        #endregion

        /// <summary>
        /// Call function Sort chossed
        /// </summary>
        public bool run(string st)
        {
            switch(st)
            {
                case "Bubble": BubbleSort(1); break;

                case "Selection": SelectionSort(1); break;

                case "Insert":  break;

                case "Counting":  break;

                case "Quick":  break;

                case "Shell":  break;  
                
                default:  break;
            }
            return true;
        }

        #region Algorithm Sort
        
        #region Bubble Sort
        /// <summary>
        /// Bubble Sort
        /// </summary>
        /// <param name="k">If k=1, Increasing. If k=0, Decreasing</param>
        public async void BubbleSort(int k)
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
                        AnimationColumn.ExchangeCol(columns[i], columns[j], time);
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
        public async void SelectionSort(int k)
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

                    if (!CompareValue(columns[j].col.Val, columns[ValueTemp].col.Val, k))
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
                    AnimationColumn.ExchangeCol(columns[i], columns[ValueTemp], time);
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

        #endregion

        #region Helper

        /// <summary>
        /// Compare between 2 Value
        /// </summary>
        /// <param name="val1">Value 1</param>
        /// <param name="val2">Value 2</param>
        /// <param name="type">If type=1, >. if type=0, ></param>
        /// <returns></returns>
        private bool CompareValue(int val1, int val2, int type)
        {
            if (type == 1)
                return val1 > val2;
            return val1 < val2;
        }

        #endregion
    }
}
