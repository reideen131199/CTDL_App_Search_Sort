using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for Sort_View.xaml
    /// </summary>
    public partial class Sort_View : UserControl
    {
        private Column_Control C;
        public Sort_View()
        {
            InitializeComponent();
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

        private int[] CreateArr()
        {
            string[] arrInput = ("0," + ViewArray.Text).Split(',');
            int[] res = new int[arrInput.Length];
            try
            {
                for (int i = 1; i < arrInput.Length; i++)
                {
                    res[i] = Int16.Parse(arrInput[i]);
                }
            }
            catch(Exception e)
            {
                ViewArray.Text = e.ToString();
                return res;
            }

            return res;
        }

        private void TextBlock_MouseUp_ChosserAlg(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void ViewArray_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewArray.Text = string.Join(", ", CreateArr());
        }

        private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Random rand = new Random();

        }
    }
}
