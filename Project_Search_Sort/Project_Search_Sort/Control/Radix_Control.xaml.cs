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

namespace Project_Search_Sort.Control
{
    /// <summary>
    /// Interaction logic for Radix_Control.xaml
    /// </summary>
    public partial class Radix_Control : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private string val = (25 << 5).ToString();
        public string Val
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Val)));
            }
        }



        public Radix_Control()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
