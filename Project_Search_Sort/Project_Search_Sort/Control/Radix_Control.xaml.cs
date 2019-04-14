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
    public partial class Radix_Control : UserControl
    {
        public Radix radix;

        private TextBlock[] textBlocks;

        #region Constructor

        public Radix_Control()
        {
            InitializeComponent();

            radix = new Radix();
            textBlocks = new TextBlock[radix.Binary.Length];

            DataContext = radix;
            CreateBinary();
        }

        public Radix_Control(Radix r)
        {
            InitializeComponent();

            radix = r;
            textBlocks = new TextBlock[radix.Binary.Length];

            DataContext = radix;
            CreateBinary();
        }

        #endregion

        /// <summary>
        /// Create View Binary
        /// </summary>
        private void CreateBinary()
        {
            for (int i=0; i<radix.Binary.Length; i++)
            {
                textBlocks[i] = new TextBlock();
                textBlocks[i].Text = radix.Binary[i].ToString();
                textBlocks[i].FontSize = 20;
            }
        }

        public void FormRadix()
        {
            Border_Value.BorderThickness = new Thickness(0);
            Border_Radix.Visibility = Visibility.Visible;
        }

        public void FormCounting()
        {
            Border_Value.BorderThickness = new Thickness(2);
            Border_Radix.Visibility = Visibility.Collapsed;
        }
    }


    public class Radix : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private int val;
        private string binary;

        public int Val
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
                Binary = Convert.ToString(Val, 2);
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Val)));
            }
        }
        public string Binary
        {
            get
            {
                return binary;
            }
            set
            {
                binary = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Binary)));
            }
        }

        public Radix()
        {
            Val = 0;
        }
    }
}
