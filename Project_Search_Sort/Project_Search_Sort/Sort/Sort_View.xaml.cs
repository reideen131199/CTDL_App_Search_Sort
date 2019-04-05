using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            C = new Column_Control();
            LayoutAnimation.Children.Add(C);

            Task.Run(async () => { await Task.Delay(1000); C.col.Val = 30; });
        }

        private void TextBlock_MouseUp_ChosserAlg(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
