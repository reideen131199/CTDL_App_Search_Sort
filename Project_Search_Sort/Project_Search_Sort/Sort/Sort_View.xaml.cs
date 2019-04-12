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
        private ViewColumnSort_Control ViewAnimation;

        #endregion

        #region Constructor

        public Sort_View()
        {
            InitializeComponent();
            CreateLayoutListSort();
            chosseAlgorithm = AlgorithmSorts[0];
            chosseAlgorithm.FontWeight = FontWeights.Bold;
            chosseAlgorithm.Background = new SolidColorBrush(Colors.AntiqueWhite);

            ViewAnimation = new ViewColumnSort_Control(new int[0]);
            LayoutAnimation.Children.Add(ViewAnimation);
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
            chosseAlgorithm = (TextBlock)sender;
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
            LayoutAnimation.Children.Remove(ViewAnimation);

            int[] arr = CreateRandomArr();
            ViewArray.Text = string.Join(", ", arr);

            // Send arr to Layout Animation Algorithm
            ViewAnimation = new ViewColumnSort_Control(arr);
            LayoutAnimation.Children.Add(ViewAnimation);           
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

            // Remove ViewAnimation old
            LayoutAnimation.Children.Remove(ViewAnimation);

            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumnSort_Control(arr);
            LayoutAnimation.Children.Add(ViewAnimation);

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

            // Remove ViewAnimation old
            LayoutAnimation.Children.Remove(ViewAnimation);

            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumnSort_Control(arr);
            LayoutAnimation.Children.Add(ViewAnimation);

            Btn_Decreasing.Visibility = Visibility.Collapsed;
            Btn_Increasing.Visibility = Visibility.Collapsed;
        }

        private void ViewArray_LostFocus(object sender, RoutedEventArgs e)
        {
            // Send ViewArray.Text to Layout Animation Algorithm
            int[] arr = ConvertStringToArr(ViewArray.Text);

            if (arr.Length !=0 && !CheckArr(arr)) return;

            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumnSort_Control(arr);
            LayoutAnimation.Children.Add(ViewAnimation);
        }

        private void Button_StartSort(object sender, RoutedEventArgs e)
        {
            // Check Arr
            if (!CheckArr(ConvertStringToArr(ViewArray.Text))) return;

            Btn_RandomArr.IsEnabled = false;
            Btn_Sorted.IsEnabled = false;
            ViewArray.IsEnabled = false;
            Sorting();

            // Remove ViewAnimation old
            LayoutAnimation.Children.Remove(ViewAnimation);
            
            // Run Animation Layout Sort
            run(chosseAlgorithm.Name);
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            if (Btn_Pause.Content.ToString() == "Pause")
            {
                NotSorting();

                // Pause Sort
                ViewAnimation.Pause = true;
            }
            else
            {
                Sorting();

                // Resume Sort
                ViewAnimation.Pause = false;
            }
        }

        private void Button_End(object sender, RoutedEventArgs e)
        {
            // Read Array
            int[] arr = ConvertStringToArr(ViewArray.Text);
            Array.Sort(arr);

            // Remove ViewAnimation old
            LayoutAnimation.Children.Remove(ViewAnimation);

            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumnSort_Control(arr);
            ViewAnimation.LockAll();
            LayoutAnimation.Children.Add(ViewAnimation);

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
            else if (length > 20 || length < 3)
            {
                ShowError("Độ dài mảng lớn hơn 2 và không quá 20 phần tử");
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
            int n = rand.Next(18) + 3;

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = rand.Next(50) + 1;
            }
            return arr;
        }

        /// <summary>
        /// Call function Sort chossed
        /// </summary>
        public async void run(string st)
        {
            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumnSort_Control(ConvertStringToArr(ViewArray.Text));
            ViewAnimation.Time = (int)Slider_Time.Value;
            LayoutAnimation.Children.Add(ViewAnimation);

            switch (st)
            {
                case "Selection_Sort":
                    await ViewAnimation.SelectionSort(1);
                    break;
                      
                case "Insert_Sort":
                    await ViewAnimation.InsertionSort(1);
                    break;

                case "Quick_Sort":
                    await ViewAnimation.QuickSort();
                    ViewAnimation.LockAll();
                    break;

                case "Shell_Sort":
                    await ViewAnimation.ShellSort();
                    break;

                case "Merge_Sort":
                    await ViewAnimation.MergeSort_Recursive(1, -1);
                    break;

                case "Radix_Sort":
                    //await FastResult(ConvertStringToArr(ViewArray.Text));
                    await ViewAnimation.QuickSort();
                    ViewAnimation.LockAll();
                    break;
                case "Heap_Sort":
                    //await FastResult(ConvertStringToArr(ViewArray.Text));
                    await ViewAnimation.ShellSort();
                    break;

                case "Counting_Sort":
                    //await ViewAnimation.CountingSort();
                    await FastResult(ConvertStringToArr(ViewArray.Text));
                    break;

                default:
                    await ViewAnimation.BubbleSort(1);
                    break;
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
        
        private async Task FastResult(int[] arr)
        {
            // Sort Array
            Array.Sort(arr);
            ViewArray.Text = string.Join(", ", arr);

            // Remove ViewAnimation old
            LayoutAnimation.Children.Remove(ViewAnimation);

            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumnSort_Control(arr);
            LayoutAnimation.Children.Add(ViewAnimation);

            ViewAnimation.LockAll();
        }


        #endregion
    }
}
