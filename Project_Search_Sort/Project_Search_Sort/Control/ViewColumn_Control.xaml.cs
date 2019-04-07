﻿using System;
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

        /// <summary>
        /// Call function Sort chossed
        /// </summary>
        public bool run(string st)
        {
            switch(st)
            {
                case "Bubble": BubbleSort(1); break;

                case "Selection": SelectionSort(1); break;

                case "Insert": InsertionSort(1); break;

                case "Counting": CountingSort(); break;

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
        public async void InsertionSort(int k)
        {
            columns[1].col.BgLock();
            for (int i = 2; i <= size; i++)
            {
                Column_Control key = columns[i];
                key.col.BgCompare();
                AnimationColumn.MoveColY(key, -280, time);
                await Task.Delay(time + 100);

                int count = i;
                while (count-1 > 0 && key.col.Val < columns[count - 1].col.Val)
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

        #region Counting

        /// <summary>
        /// Counting
        /// </summary>
        public void CountingSort()
        {
            int[] count = new int[11];
            Column_Control[] copy = columns;
            int maxx = columns[1].col.Val;

            for (int i = 2; i <= size; i++)
            {
                count[columns[i].col.Val]++;
                if (copy[i].col.Val > maxx) maxx = copy[i].col.Val;
            }

            for (int i = 1; i <= maxx; i++)
            {
                count[i] += count[i - 1];
            }

            columns = new Column_Control[size + 2];

            for (int i = 1; i <= size; i++)
            {
                columns[count[copy[i].col.Val]] = copy[i];
                count[copy[i].col.Val] -= 1;
            }

            for (int i = 1; i <= size; i++)
                BlockCompare.Text += columns[i].col.Val.ToString() + ", ";

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
            LayoutAnimation.Children.Add(t);
            return t;
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
