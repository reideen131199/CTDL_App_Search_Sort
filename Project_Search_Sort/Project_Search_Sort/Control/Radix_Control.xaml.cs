using System;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Controls;


namespace Project_Search_Sort
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
