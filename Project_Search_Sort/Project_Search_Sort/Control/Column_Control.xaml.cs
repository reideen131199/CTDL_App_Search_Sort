using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Controls;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for Column_Control.xaml
    /// </summary>
    public partial class Column_Control : UserControl
    {
        public Col col;

        public Column_Control()
        {
            InitializeComponent();
            col = new Col();
            DataContext = col;
        }
        public Column_Control(Col c)
        {
            InitializeComponent();
            col = c;
            DataContext = col;
        }
    }

    public class Col : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private int h;
        private int val;
        private Brush bg;

        public int H
        {
            get
            {
                return this.h;
            }
            set
            {
                this.h = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(H)));
            }
        }

        public int Val
        {
            get
            {
                return val;
            }
            set
            {
                this.val = value;
                this.H = value * 10;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Val)));
            }
        }

        public Brush Bg
        {
            get
            {
                return this.bg;
            }
            set
            {
                this.bg = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Bg)));
            }
        }

        public Col()
        {
            Val = 0;
            Bg = (Brush)(new BrushConverter()).ConvertFromString("#fff");
        }

    }
}
