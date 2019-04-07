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
        private ViewColumn_Control ViewAnimation;

        #endregion

        #region Constructor

        public Sort_View()
        {
            InitializeComponent();
            CreateLayoutListSort();
            chosseAlgorithm = AlgorithmSorts[0];
            chosseAlgorithm.FontWeight = FontWeights.Bold;

            ViewAnimation = new ViewColumn_Control(new int[0]);
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
                "Bubble",
                "Selection",
                "Insert",
                "Counting",
                "Quick",
                "Shell",
                "Radix",
                "Merge",
                "Heap"
            };

            AlgorithmSorts = new List<TextBlock>();
            foreach (string s in st)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Name = s;
                textBlock.Text = s;
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
            chosseAlgorithm.FontWeight = FontWeights.Normal;
            chosseAlgorithm = (TextBlock)sender;
            chosseAlgorithm.FontWeight = FontWeights.Bold;
        }

        #endregion

        #region Row Control

        #region 
        private void Sorting()
        {
            Btn_Pause.IsEnabled = true;
            Btn_Pause.Content = "Pause";
            Btn_End.IsEnabled = true;
            Btn_StartSort.IsEnabled = false;
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
        }

        #endregion

        #region Event on Row Control

        private void Button_RandomArr(object sender, RoutedEventArgs e)
        {
            LayoutAnimation.Children.Remove(ViewAnimation);

            int[] arr = CreateRandomArr();
            ViewArray.Text = string.Join(", ", arr);

            // Send arr to Layout Animation Algorithm
            ViewAnimation = new ViewColumn_Control(arr);
            LayoutAnimation.Children.Add(ViewAnimation);           
        }

        private void Button_Sorted(object sender, RoutedEventArgs e)
        {
            // If Array do not have then Random
            if (ViewArray.Text == "")
            {
                ViewArray.Text = string.Join(", ", CreateRandomArr());
            }

            // Read Array
            int[] arr = ConvertStringToArr(ViewArray.Text);

            // Check arr
            if (arr.Length == 0)
            {
                ShowError("Mảng không hợp lệ!!");
                return;
            }

            // Sort Array
            Array.Sort(arr);
            ViewArray.Text = string.Join(", ", arr);

            // Remove ViewAnimation old
            LayoutAnimation.Children.Remove(ViewAnimation);

            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumn_Control(arr);
            LayoutAnimation.Children.Add(ViewAnimation);
        }

        private void ViewArray_LostFocus(object sender, RoutedEventArgs e)
        {
            // Send ViewArray.Text to Layout Animation Algorithm
            ConvertStringToArr(ViewArray.Text);
        }

        private void Button_StartSort(object sender, RoutedEventArgs e)
        {
            // Check Arr
            Btn_RandomArr.IsEnabled = false;
            Btn_Sorted.IsEnabled = false;
            ViewArray.IsEnabled = false;
            Sorting();

            // Remove ViewAnimation old
            LayoutAnimation.Children.Remove(ViewAnimation);

            // Send arr and Create ViewAnimation new
            ViewAnimation = new ViewColumn_Control(ConvertStringToArr(ViewArray.Text));
            LayoutAnimation.Children.Add(ViewAnimation);

            // Run Animation Layout Sort
            ViewAnimation.run(chosseAlgorithm.Text);
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            if (Btn_Pause.Content.ToString() == "Pause")
            {
                NotSorting();
                // Pause Sort
            }
            else
            {
                Sorting();
                // Resume
            }
        }

        private void Button_End(object sender, RoutedEventArgs e)
        {
            //End Sort
            Sorted();
        }

        #endregion

        #endregion

        #region Helper

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

            for (int i=0; i<Sts.Length; i++)
            {
                if (!Int32.TryParse(Sts[i], out a[i]))
                    return new int[0];
            }
            return a;
        }

        /// <summary>
        /// Show Error
        /// </summary>
        /// <param name="err">Error</param>
        private void ShowError(string err)
        {

        }
        
        #endregion
    }
}
