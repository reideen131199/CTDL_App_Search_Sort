using System;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Threading.Tasks;

namespace Project_Search_Sort
{
    #region Column Control
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
    #endregion

    #region Class Col INotifyPropertyChanged

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
                this.H = value * 4;
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
            BgDefault();
        }

        public void BgDefault()
        {
            Bg = (Brush)(new BrushConverter()).ConvertFromString("#ccc");
        }

    }

    #endregion

    #region Class Animation for Column Control

    class AnimationColumn
    {
        public AnimationColumn() { }

        /// <summary>
        /// Exchange position 2 column control on time
        /// </summary>
        /// <param name="col1">Column 1</param>
        /// <param name="col2">Column 2</param>
        /// <param name="time">Time Exchange(Miliseconds)</param>
        public static void ExchangeCol(Column_Control col1, Column_Control col2, double time)
        {
            /*
            Column_Control temp = col1;
            col1 = col2;
            col2 = temp;
            */
            double savePositionControl_1 = Canvas.GetLeft(col1);
            MoveCol(col1, Canvas.GetLeft(col2), time);
            MoveCol(col2, savePositionControl_1, time);
        }

        /// <summary>
        /// Move control to position pos in comparison with Left panel parent on time
        /// </summary>
        /// <param name="control">Column need Move</param>
        /// <param name="pos">Position in comparison width Left panel parent</param>
        /// <param name="time">Time move(Miliseconds)</param>
        public static void MoveCol(Column_Control control, double pos, double time)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation(pos, new Duration(TimeSpan.FromMilliseconds(time)));
            control.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }
    }

    #endregion
}
