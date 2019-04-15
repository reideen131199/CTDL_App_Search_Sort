using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;

namespace Project_Search_Sort
{
    #region Node_Control

    /// <summary>
    /// Interaction logic for Node_Control.xaml
    /// </summary>
    public partial class Node_Control : UserControl
    {
        public Node node;

        public Node_Control()
        {
            InitializeComponent();
            node = new Node();
            DataContext = node;
        }

        public Node_Control(Node n)
        {
            InitializeComponent();
            node = n;
            DataContext = node;
        }
    }

    #endregion

    #region Node INotidyPropertyChanged

    public class Node : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e)=> { };

        private int val;
        private Brush bg;

        public int Val
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

        public Brush Bg
        {
            get
            {
                return bg;
            }
            set
            {
                bg = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Bg)));
            }
        }
        

        public Node()
        {
            Val = 0;
            BgKey();
        }


        #region Color local

        #region Define string rgb Color

        private const string ColorDefault = "#FFCCCCCC";
        private const string ColorTemp = "#FFBB0000";
        private const string ColorLock = "#FF00FF50";
        private const string ColorCompare = "#FFFF0000";
        private const string ColorKey = "#FFFF8A27";
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
        public bool CheckBgLock()
        {
            //if (bg == (Brush)(new BrushConverter()).ConvertFromString(ColorLock)) return true;
            if (bg.ToString() == "#FF00FF50") return true;
            return false;
        }

        public void BgCompare()
        {
            Bg = (Brush)(new BrushConverter()).ConvertFromString(ColorCompare);
        }
        public bool CheckBgCompare()
        {
            //if (bg == (Brush)(new BrushConverter()).ConvertFromString(ColorCompare)) return true;
            if (bg.ToString() == "#FFFF0000") return true;
            return false;
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

}
