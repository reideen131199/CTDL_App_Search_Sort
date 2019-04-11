using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for Search_View.xaml
    /// </summary>
    public partial class Search_View : UserControl
    {
        #region Private Variable

        private TextBlock chosseAlgorithm;
        private List<TextBlock> AlgorithmSorts;
        private ViewColumnSearch_Control ViewAnimation;

        #endregion

        #region Constructor

        public Search_View()
        {
            InitializeComponent();
            CreateLayoutListSort();
            chosseAlgorithm = AlgorithmSorts[0];
            chosseAlgorithm.FontWeight = FontWeights.Bold;
            chosseAlgorithm.Background = new SolidColorBrush(Colors.AntiqueWhite);

            ViewAnimation = new ViewColumnSearch_Control(new int[0]);
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
                "Linear_Search",
                //"Binary_Search_Tree",
                "Binary_Search"
                
            };

            AlgorithmSorts = new List<TextBlock>();
            foreach (string s in st)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Name = s;
                textBlock.Text = s.Replace("_", " ");
                textBlock.FontSize = 16;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.Margin = new Thickness(5, 3, 5, 3);
                textBlock.Padding = new Thickness(5, 3, 5, 3);
                textBlock.MouseDown += TextBlock_SelecAlgSort;
                AlgorithmSorts.Add(textBlock);
                LayoutListAlgorithmSearch.Children.Add(textBlock);
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

        private void Searching()
        {
            Btn_Pause.IsEnabled = true;
            Btn_Pause.Content = "Pause";
            Btn_End.IsEnabled = true;
            Btn_StartSearch.IsEnabled = false;
            Slider_Time.IsEnabled = false;
            ValueSearch.IsEnabled = false;
        }

        private void NotSearching()
        {
            Btn_Pause.Content = "Resume";
            Btn_End.IsEnabled = false;
        }

        private void Searched()
        {
            Btn_RandomArr.IsEnabled = true;
            ViewArray.IsEnabled = true;
            Btn_StartSearch.IsEnabled = true;
            Btn_Pause.IsEnabled = false;
            Btn_End.IsEnabled = false;
            Slider_Time.IsEnabled = true;
            ValueSearch.IsEnabled = true;
        }

        #endregion

        #region Event on Row Control

        private void Button_RandomArr(object sender, RoutedEventArgs e)
        {
            LayoutAnimation.Children.Remove(ViewAnimation);

            int[] arr = CreateRandomArr();
            ViewArray.Text = string.Join(", ", arr);

            // Send arr to Layout Animation Algorithm
            ViewAnimation = new ViewColumnSearch_Control(arr);
            LayoutAnimation.Children.Add(ViewAnimation);

            Btn_RandomArr.Content = Slider_Time.Value.ToString();
        }

        private void ViewArray_LostFocus(object sender, RoutedEventArgs e)
        {
            // Send ViewArray.Text to Layout Animation Algorithm
            int[] arr = ConvertStringToArr(ViewArray.Text);

            if (arr.Length != 0 && !CheckArr(arr)) return;

            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumnSearch_Control(arr);
            LayoutAnimation.Children.Add(ViewAnimation);

        }

        private void Button_StartSearch(object sender, RoutedEventArgs e)
        {
            // Check Arr
            CheckArr(ConvertStringToArr(ViewArray.Text));

            // Check Value Find
            int val = new int();
            if (ValueSearch.Text == "")
            {
                ShowError("Chưa có giá trị cần tìm!!");
                return;
            }
            else if (!Int32.TryParse(ValueSearch.Text, out val))
            {
                ShowError("Giá trị tìm kiếm không hợp lệ!!");
                return;
            }


            Btn_RandomArr.IsEnabled = false;
            ViewArray.IsEnabled = false;
            Searching();


            // Remove ViewAnimation old
            LayoutAnimation.Children.Remove(ViewAnimation);

            // Run Animation Layout Search
            run(chosseAlgorithm.Name, val);
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            if (Btn_Pause.Content.ToString() == "Pause")
            {
                NotSearching();
                // Pause Sort
            }
            else
            {
                Searching();
                // Resume
            }
        }

        private void Button_End(object sender, RoutedEventArgs e)
        {
            // Read Array
            int[] arr = ConvertStringToArr(ViewArray.Text);

            // Remove ViewAnimation old
            LayoutAnimation.Children.Remove(ViewAnimation);

            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumnSearch_Control(arr);
            ViewAnimation.SearchFast(Int16.Parse(ValueSearch.Text));
            LayoutAnimation.Children.Add(ViewAnimation);

            //End Sort
            Searched();
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
            else if (length > 20)
            {
                ShowError("Độ dài mảng không quá 20 phần tử");
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
        /// Convert String To Arr
        /// </summary>
        /// <param name="st"></param>
        /// <returns>Return arr[0] if False</returns>
        private int[] ConvertStringToArr(string st)
        {
            string[] Sts = st.Split(',');
            int[] a = new int[Sts.Length];

            for (int i = 0; i < Sts.Length; i++)
            {
                if (!Int32.TryParse(Sts[i], out a[i]))
                    return new int[0];
            }
            return a;
        }

        /// <summary>
        /// Call function Sort chossed
        /// </summary>
        public async void run(string st, int Value)
        {
            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumnSearch_Control(ConvertStringToArr(ViewArray.Text));
            ViewAnimation.Time = (int)Slider_Time.Value;
            LayoutAnimation.Children.Add(ViewAnimation);

            switch (st)
            {
                case "Binary_Search":
                    
                    // Read Array
                    int[] arr = ConvertStringToArr(ViewArray.Text);
                    Array.Sort(arr);

                    // Remove ViewAnimation old
                    LayoutAnimation.Children.Remove(ViewAnimation);

                    // Send arr and Create ViewAnimation new
                    ViewAnimation = new ViewColumnSearch_Control(arr);
                    LayoutAnimation.Children.Add(ViewAnimation);

                    await ViewAnimation.Binary(Value);
                    break;
                
                /*
                case "Binary_Search_Tree":
                    ViewAnimation.BinarySearchTree(Value);
                    break;
                */

                default:
                    await ViewAnimation.Linear(Value);
                    break;
            }
            Searched();
        }

        /// <summary>
        /// Show Error
        /// </summary>
        /// <param name="err">Error</param>
        private void ShowError(string err)
        {
            MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion
    }
}
