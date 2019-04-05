using System;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;

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
        
        #endregion

        #region Constructor
        public Sort_View()
        {
            InitializeComponent();
            CreateLayoutListSort();
            /*
            int[] a = { -1, 10, 64, 7, 52, 32, 18, 2, 48, 1, 99 };
            //int[] a = {-1, 1, 1, 5, 5, 8, 9, 2, 2, 3, 1 };
            Sort s = new Sort(10, a);
            s.CountingSort();
            a = s.getArray();
            for (int i = 1; i <= 10; i++)
            {
                //sx.Text += a[i];
                //sx.Text += " ";
            }
            */
        }
        #endregion

        #region Row List Sort
        /// <summary>
        /// Create Layout List Sort
        /// </summary>
        private void CreateLayoutListSort()
        {
            string[] st = { "Bubble", "Selection", "Insert", "Counting",
                            "Quick", "Shell", "Radix", "Merge", "Heap" };

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
            chosseAlgorithm = AlgorithmSorts[0];
            chosseAlgorithm.FontWeight = FontWeights.Bold;
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

        private void ViewArray_LostFocus(object sender, RoutedEventArgs e)
        {
            // Send ViewArray.Text to Layout Animation Algorithm
        }

        private void Button_RandomArr(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int n = rand.Next(20) + 1;

            int[] arr = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                arr[i] = rand.Next(50) + 1;
            }
            arr[0] = n;
            ViewArray.Text = string.Join(", ", arr);
            // Send ViewArray.Text to Layout Animation Algorithm
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            /*
            if (true)
            {
                Btn_Pause.Content = "Resume";
            }
            else
            {
                Btn_Pause.Content = "Pause";
            }
            */
        }

        private void Button_End(object sender, RoutedEventArgs e)
        {

        }

        private void Button_StartSort(object sender, RoutedEventArgs e)
        {
            Sorting();
            // Run Animation Layout Sort
        }

        private void Sorting()
        {
            Btn_Pause.Content = "Resume";
            Btn_End.IsEnabled = true;
            Btn_StartSort.IsEnabled = false;
        }

        #endregion

    }
}
