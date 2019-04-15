using System;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for Sort_View.xaml
    /// </summary>
    public partial class Sort_View : UserControl
    {
        #region Private Variable

        private TextBlock chosseAlgorithm;
        private List<TextBlock> AlgorithmSorts;
        private ViewColumnSort_Control ViewAnimationColumn;
        private ViewRadixSort_Control ViewAnimationRadix;
        private ViewTreeSort_Control ViewAnimationTree;
        #endregion

        #region Constructor

        public Sort_View()
        {
            InitializeComponent();
            CreateLayoutListSort();
            chosseAlgorithm = AlgorithmSorts[0];
            chosseAlgorithm.FontWeight = FontWeights.Bold;
            chosseAlgorithm.Background = new SolidColorBrush(Colors.AntiqueWhite);

            CreateNewView(new int[0]);
        }

        #endregion

        #region Row List Sort

        /// <summary>
        /// Create Layout List Sort
        /// </summary>
        private void CreateLayoutListSort()
        {
            string[] st = {
                "Bubble_Sort",
                "Selection_Sort",
                "Insert_Sort",
                "Shell_Sort",
                "Quick_Sort",
                "Merge_Sort",
                "Radix_Sort",
                "Heap_Sort",
                "Counting_Sort"
            };

            AlgorithmSorts = new List<TextBlock>();
            foreach (string s in st)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Name = s;
                textBlock.Text = s.Replace("_", " ");
                textBlock.FontSize = 16;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.Margin = new Thickness(0, 3, 0, 3);
                textBlock.Padding = new Thickness(5, 3, 5, 3);
                textBlock.MouseDown += TextBlock_SelecAlgSort;
                AlgorithmSorts.Add(textBlock);
                LayoutListAlgorithmSort.Children.Add(textBlock);
            }
        }

        /// <summary>
        /// Event Select Algorithm Sort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_SelecAlgSort(object sender, RoutedEventArgs e)
        {
            chosseAlgorithm.Background = new SolidColorBrush(Colors.Transparent);
            chosseAlgorithm.FontWeight = FontWeights.Normal;
            Sorted();

            TextBlock newChosseAlgorithm = (TextBlock)sender;

            // New ChosseAlgorithm is Coungting Sort
            if (newChosseAlgorithm.Name == "Counting_Sort" && chosseAlgorithm.Name != "Counting_Sort")
            {
                chosseAlgorithm = newChosseAlgorithm;

                int[] arr = CreateRandomArr();
                ViewArray.Text = string.Join(", ", arr);

                CreateNewView(arr);
            }
            // New ChosseAlgorithm is Radix Sort
            else if (newChosseAlgorithm.Name == "Radix_Sort" && chosseAlgorithm.Name != "Radix_Sort")
            {
                chosseAlgorithm = newChosseAlgorithm;

                int[] arr = CreateRandomArr();
                ViewArray.Text = string.Join(", ", arr);

                CreateNewView(arr);
            }
            else
            {
                if (chosseAlgorithm.Name != "Radix_Sort" || chosseAlgorithm.Name != "Counting_Sort")
                {
                    chosseAlgorithm = newChosseAlgorithm;
                    int[] arr = CreateRandomArr();
                    ViewArray.Text = string.Join(", ", arr);
                }

                CreateNewView(ConvertStringToArr(ViewArray.Text));
            }
            
            chosseAlgorithm.FontWeight = FontWeights.Bold;
            chosseAlgorithm.Background = new SolidColorBrush(Colors.AntiqueWhite);
        }

        #endregion

        #region Row Control

        #region Status

        private void Sorting()
        {
            Btn_Pause.IsEnabled = true;
            Btn_Pause.Content = "Pause";
            Btn_End.IsEnabled = true;
            Btn_StartSort.IsEnabled = false;
            Slider_Time.IsEnabled = false;
        }

        private void NotSorting()
        {
            Btn_Pause.Content = "Resume";
            Btn_End.IsEnabled = false;
        }

        private void Sorted()
        {
            Btn_RandomArr.IsEnabled = true;
            Btn_Sorted.IsEnabled = true;
            ViewArray.IsEnabled = true;
            Btn_StartSort.IsEnabled = true;
            Btn_Pause.IsEnabled = false;
            Btn_End.IsEnabled = false;
            Slider_Time.IsEnabled = true;
        }

        #endregion

        #region Event on Row Control

        private void Button_RandomArr(object sender, RoutedEventArgs e)
        {
            int[] arr = CreateRandomArr();
            ViewArray.Text = string.Join(", ", arr);

            // Create View
            CreateNewView(arr);           
        }

        private void Button_Sorted(object sender, RoutedEventArgs e)
        {
            Btn_Decreasing.Visibility = Visibility.Visible;
            Btn_Increasing.Visibility = Visibility.Visible;
        }

        private void Button_Sort_Increasing(object sender, RoutedEventArgs e)
        {
            // If Array do not have then Random
            if (ViewArray.Text == "")
                ViewArray.Text = string.Join(", ", CreateRandomArr());

            // Read Array
            int[] arr = ConvertStringToArr(ViewArray.Text);

            // Check arr

            // Sort Array
            Array.Sort(arr);
            ViewArray.Text = string.Join(", ", arr);

            CreateNewView(arr);

            Btn_Decreasing.Visibility = Visibility.Collapsed;
            Btn_Increasing.Visibility = Visibility.Collapsed;
        }

        private void Button_Sort_Decreasing(object sender, RoutedEventArgs e)
        {
            // If Array do not have then Random
            if (ViewArray.Text == "")
                ViewArray.Text = string.Join(", ", CreateRandomArr());

            // Read Array
            int[] arr = ConvertStringToArr(ViewArray.Text);

            // Check arr

            // Sort Array
            Array.Sort<int>(arr,
                    new Comparison<int>(
                            (i1, i2) => i2.CompareTo(i1)
                    ));

            ViewArray.Text = string.Join(", ", arr);

            CreateNewView(arr);

            Btn_Decreasing.Visibility = Visibility.Collapsed;
            Btn_Increasing.Visibility = Visibility.Collapsed;
        }

        private void ViewArray_LostFocus(object sender, RoutedEventArgs e)
        {
            // Send ViewArray.Text to Layout Animation Algorithm
            int[] arr = ConvertStringToArr(ViewArray.Text);

            if (arr.Length !=0 && !CheckArr(arr)) return;

            CreateNewView(arr);
        }

        private void Button_StartSort(object sender, RoutedEventArgs e)
        {
            // Check Arr
            if (!CheckArr(ConvertStringToArr(ViewArray.Text))) return;

            Btn_RandomArr.IsEnabled = false;
            Btn_Sorted.IsEnabled = false;
            ViewArray.IsEnabled = false;
            Sorting();

            // Run Animation Layout Sort
            run(chosseAlgorithm.Name);
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            string st = chosseAlgorithm.Name;
            if (Btn_Pause.Content.ToString() == "Pause")
            {
                NotSorting();

                // Pause Sort
                if (st == "Radix_Sort" || st == "Counting_Sort")
                    ViewAnimationRadix.Pause = true;
                else if (st == "Heap_Sort")
                    ViewAnimationTree.Pause = true;
                else
                    ViewAnimationColumn.Pause = true;
            }
            else
            {
                Sorting();

                // Resume Sort
                if (st == "Radix_Sort" || st == "Counting_Sort")
                    ViewAnimationRadix.Pause = false;
                else if (st == "Heap_Sort")
                    ViewAnimationTree.Pause = false;
                else
                    ViewAnimationColumn.Pause = false;
            }
        }

        private void Button_End(object sender, RoutedEventArgs e)
        {
            // Read Array
            int[] arr = ConvertStringToArr(ViewArray.Text);
            Array.Sort(arr);

            CreateNewView(arr, true);

            Sorted();
        }

        #endregion

        #endregion

        #region Helper

        /// <summary>
        /// Check Array
        /// </summary>
        /// <param name="arr">Array need check</param>
        /// <returns></returns>
        private bool CheckArr(int[] arr)
        {
            int length = arr.Length;
            if (length == 0)
            {
                ShowError("Giá trị của mảng không đúng!!!");
                return false;
            }
            else if (length == 1 && arr[0] == -1)
            {
                ShowError("Giá trị của mỗi phần tử chỉ từ 1 đến 50");
                return false;
            }
            else if (length > 18 || length < 3)
            {
                ShowError("Độ dài mảng lớn hơn 2 và không quá 18 phần tử");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Array begin from 0
        /// </summary>
        /// <returns></returns>
        private int[] CreateRandomArr()
        {
            Random rand = new Random();
            int n = rand.Next(15) + 3;

            int Max = 50;
            if (chosseAlgorithm.Name == "Counting_Sort")
                Max = 9;

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = rand.Next(Max) + 1;
            }
            return arr;
        }

        /// <summary>
        /// Call function Sort chossed
        /// </summary>
        public async void run(string st)
        {
            if (st == "Radix_Sort")
            {
                // Send arr and Create ViewAnimation new
                CreateNewView(ConvertStringToArr(ViewArray.Text));
                ViewAnimationRadix.Time = (int)Slider_Time.Value;
                
                // Run Algorithm
                await ViewAnimationRadix.RadixSort();
            }
            else if (st == "Heap_Sort")
            {
                // Send arr and Create ViewAnimaion new
                CreateNewView(ConvertStringToArr(ViewArray.Text));
                ViewAnimationTree.Time = (int)Slider_Time.Value;

                // Run Algorithm
                await ViewAnimationTree.PerformHeapSort();
            }
            else if (st == "Counting_Sort")
            {
                CreateNewView(ConvertStringToArr(ViewArray.Text));
                ViewAnimationRadix.Time = (int)Slider_Time.Value;

                // Run Algorithm
                await ViewAnimationRadix.CountingSort();
            }
            else // ViewColumn
            {
                CreateNewView(ConvertStringToArr(ViewArray.Text));
                ViewAnimationColumn.Time = (int)Slider_Time.Value;

                // Run Algorithm
                switch (st)
                {
                    case "Selection_Sort":
                        await ViewAnimationColumn.SelectionSort(1);
                        break;

                    case "Insert_Sort":
                        await ViewAnimationColumn.InsertionSort(1);
                        break;

                    case "Quick_Sort":
                        await ViewAnimationColumn.QuickSort();
                        ViewAnimationColumn.LockAll();
                        break;

                    case "Shell_Sort":
                        await ViewAnimationColumn.ShellSort();
                        break;

                    case "Merge_Sort":
                        await ViewAnimationColumn.MergeSort_Recursive(1, -1);
                        break;

                    default:
                        await ViewAnimationColumn.BubbleSort(1);
                        break;
                }
            }

            Sorted();
        }

        /// <summary>
        /// Convert String To Arr
        /// </summary>
        /// <param name="st"></param>
        /// <returns>Return arr[0] if False</returns>
        private int[] ConvertStringToArr(string st)
        {
            string[] Sts = st.Split(',');
            int[] a = new int[Sts.Length];

            for (int i=0; i<Sts.Length; i++)
            {
                if (!Int32.TryParse(Sts[i], out a[i]))
                    return new int[0];
                if (a[i] > 50 || a[i] < 1)
                {
                    int[] ar = new int[1] { -1 };
                    return ar;
                }
            }
            return a;
        }

        /// <summary>
        /// Show Error
        /// </summary>
        /// <param name="err">Error</param>
        private void ShowError(string err)
        {
            MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Create Control for Array
        /// </summary>
        /// <param name="arr">Array need create</param>
        private void CreateNewView(int[] arr, bool Lock = false)
        {
            // Remove ViewAnimation old
            LayoutAnimation.Children.Clear();

            // Send arr and Create View
            string st = chosseAlgorithm.Name;
            if (st == "Radix_Sort")
            {
                ViewAnimationRadix = new ViewRadixSort_Control(arr);
                ViewAnimationRadix.ChangedFormRadix();
                LayoutAnimation.Children.Add(ViewAnimationRadix);
                if (Lock) ViewAnimationRadix.LockAll();
            }
            else if (st == "Heap_Sort")
            {
                ViewAnimationTree = new ViewTreeSort_Control(arr);
                LayoutAnimation.Children.Add(ViewAnimationTree);
                if (Lock) ViewAnimationTree.LockAll();
            }
            else if (st == "Counting_Sort")
            {
                ViewAnimationRadix = new ViewRadixSort_Control(arr);
                ViewAnimationRadix.ChangedFormCounting();
                LayoutAnimation.Children.Add(ViewAnimationRadix);
                if (Lock) ViewAnimationRadix.LockAll();
            }
            else
            {
                ViewAnimationColumn = new ViewColumnSort_Control(arr);
                ViewAnimationColumn.Time = (int)Slider_Time.Value;
                LayoutAnimation.Children.Add(ViewAnimationColumn);
                if (Lock) ViewAnimationColumn.LockAll();
            }

        }

        #endregion
    }
}
