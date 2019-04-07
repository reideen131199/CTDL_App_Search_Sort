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

        #region Color local

        #region Define string rgb Color

        private const string ColorDefault = "#ccc";
        private const string ColorTemp = "#bb0000";
        private const string ColorLock = "#00ff50";
        private const string ColorCompare = "#ff0000";
        private const string ColorKey = "#ff8a27";
        //private const string  = "";

        #endregion

        #region Function set Background from string rgb local

        public void BgDefault()
        {
            Bg = (Brush)(new BrushConverter()).ConvertFromString(ColorDefault);
        }

        public void BgLock()
        {
            Bg = (Brush)(new BrushConverter()).ConvertFromString(ColorLock);
        }

        public void BgCompare()
        {
            Bg = (Brush)(new BrushConverter()).ConvertFromString(ColorCompare);
        }

        public void BgTemp()
        {
            Bg = (Brush)(new BrushConverter()).ConvertFromString(ColorTemp);
        }

        public void BgKey()
        {
            Bg = (Brush)(new BrushConverter()).ConvertFromString(ColorKey);
        }

        #endregion

        #endregion
    }

    #endregion

    #region Class Animation for Column Control

    class AnimationColumn
    {
        public AnimationColumn() { }

        #region Animation Ox
        
        /// <summary>
        /// Exchange position 2 column control on time
        /// </summary>
        /// <param name="col1">Column 1</param>
        /// <param name="col2">Column 2</param>
        /// <param name="time">Time Exchange(Miliseconds)</param>
        public static void ExchangeColX(Column_Control col1, Column_Control col2, double time)
        {
            double savePositionControl_1 = Canvas.GetLeft(col1);
            MoveColX(col1, Canvas.GetLeft(col2), time);
            MoveColX(col2, savePositionControl_1, time);
        }

        /// <summary>
        /// Move control to position pos in comparison with Left panel parent on time
        /// </summary>
        /// <param name="control">Column need Move</param>
        /// <param name="pos">Position in comparison width Left panel parent</param>
        /// <param name="time">Time move(Miliseconds)</param>
        public static void MoveColX(Column_Control control, double pos, double time)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation(pos, new Duration(TimeSpan.FromMilliseconds(time)));
            control.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }

        #endregion

        #region Animation Oy
        /// <summary>
        /// Exchange position 2 column control on time 
        /// </summary>
        /// <param name="col1">Column 1</param>
        /// <param name="col2">Column 2</param>
        /// <param name="time">Time Exchange(Miliseconds)</param>
        public static void ExchangeColY(Column_Control col1, Column_Control col2, double time)
        {
            double savePositionControl_1 = Canvas.GetBottom(col1);
            MoveColY(col1, Canvas.GetBottom(col2), time);
            MoveColY(col2, savePositionControl_1, time);
        }

        /// <summary>
        /// Move control to position pos in comparison with Bottom panel parent on time
        /// </summary>
        /// <param name="control">Column need Move</param>
        /// <param name="pos">Position in comparison width Bottom panel parent</param>
        /// <param name="time">Time move(Miliseconds)</param>
        public static void MoveColY(Column_Control control, double pos, double time)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation(pos, new Duration(TimeSpan.FromMilliseconds(time)));
            control.BeginAnimation(Canvas.BottomProperty, doubleAnimation);
        }

        #endregion 
    }

    #endregion
}
