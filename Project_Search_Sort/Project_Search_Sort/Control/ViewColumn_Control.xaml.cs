using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;

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
        public void run(string st)
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
        }

        #region Algorithm Sort
        
        #region Bubble Sort
        /// <summary>
        /// Bubble Sort
        /// </summary>
        /// <param name="k">If k=1, Increasing. if k=0, Decreasing </param>
        public async void BubbleSort(int k)
        {
            for (int i = 1; i < size; i++)
            {
                for (int j = i + 1; j <= size; j++)
                {
                    columns[i].col.Bg = (Brush)(new BrushConverter()).ConvertFromString("#ff0000");
                    columns[j].col.Bg = (Brush)(new BrushConverter()).ConvertFromString("#ff0000");
                    await Task.Delay(time + 100);

                    if (CompareValue(columns[i].col.Val, columns[j].col.Val, k)) //Swap(ref arr[i], ref arr[j]);
                    {
                        AnimationColumn.ExchangeCol(columns[i], columns[j], time);
                        Column_Control temp = columns[i];
                        columns[i] = columns[j];
                        columns[j] = temp;
                    }

                    await Task.Delay(time + 100);
                    columns[j].col.BgDefault();
                    columns[j].col.BgDefault();
                }
                columns[i].col.Bg = (Brush)(new BrushConverter()).ConvertFromString("#00ff50");
            }
            columns[size].col.Bg = (Brush)(new BrushConverter()).ConvertFromString("#00ff50");
        }
        #endregion

        #region Selection
        
        public async void SelectionSort(int k)
        {
            int temp;
            for (int i = 1; i < size; i++)
            {
                temp = i;
                columns[temp].col.Bg = (Brush)(new BrushConverter()).ConvertFromString("#bb0000");
                for (int j = i + 1; j <= size; j++)
                {
                    //if (arr[j] < arr[min]) min = j;
                    columns[j].col.Bg = (Brush)(new BrushConverter()).ConvertFromString("#ff0000");
                    await Task.Delay(time + 100);

                    if (!CompareValue(columns[j].col.Val, columns[temp].col.Val, k))
                    {
                        temp = j;

                    }
                        

                    columns[i].col.BgDefault();
                    columns[j].col.BgDefault();
                    await Task.Delay(time + 100);
                }
                if (temp != i)
                {
                    //Swap(ref arr[min], ref arr[i]);
                }
            }
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
