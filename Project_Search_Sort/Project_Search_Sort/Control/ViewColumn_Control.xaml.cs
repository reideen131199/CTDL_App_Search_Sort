using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ViewColumn_Control.xaml
    /// </summary>
    public partial class ViewColumn_Control : UserControl
    {
        private int[] arr;
        private Column_Control[] columns;

        public ViewColumn_Control(int[] a)
        {
            InitializeComponent();
            arr = a;
            CreateCol();
        }

        private void CreateCol()
        {
            int n = (arr.Length < 1) ? 0 : arr.Length - 1;
            columns = new Column_Control[n + 1];

            LayoutAnimation.Width = n * 40;
            for (int i=1; i<=n; i++)
            {
                columns[i] = new Column_Control();
                columns[i].col.Val = arr[i];
                Canvas.SetBottom(columns[i], 0);
                Canvas.SetLeft(columns[i], (i-1) * 40);
                LayoutAnimation.Children.Add(columns[i]);
            }
        }


        //private void ExchangeCol(Column_Control control_1, Column_Control control_2, double )

    }
}
