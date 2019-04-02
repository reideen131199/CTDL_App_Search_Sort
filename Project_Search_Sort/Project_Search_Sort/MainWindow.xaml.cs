using AllSort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            int[] a = { -1, 10, 64, 7, 52, 32, 18, 2, 48, 1, 99 };
            //int[] a = {-1, 1, 1, 5, 5, 8, 9, 2, 2, 3, 1 };
            Sort s = new Sort(10, a);
            s.CountingSort();
            a = s.getArray();
            for (int i = 1; i <= 10; i++)
            {
                sx.Text += a[i];
                sx.Text += " ";
            }
        }
    }
}
