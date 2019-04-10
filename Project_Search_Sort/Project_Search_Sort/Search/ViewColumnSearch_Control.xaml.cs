using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for ViewColumnSearch_Control.xaml
    /// </summary>
    public partial class ViewColumnSearch_Control : UserControl
    {
        #region Private Value

        private int size;
        private int time = 100;
        private Column_Control[] columns;
        private double Bot = -280;

        #endregion

        // Get Set Time
        public int Time { get; set; }

        #region Constructor

        /// <summary>
        /// Create View Columns Control
        /// </summary>
        /// <param name="a">Array need show</param>
        public ViewColumnSearch_Control(int[] a)
        {
            InitializeComponent();

            int[] dest = new int[a.Length + 1];
            Array.Copy(a, 0, dest, 1, a.Length);

            // CreateCol
            size = (dest.Length < 1) ? 0 : dest.Length - 1;
            columns = new Column_Control[size + 1];

            for (int i = 1; i <= size; i++)
            {
                columns[i] = new Column_Control();
                columns[i].col.Val = dest[i];
                Canvas.SetBottom(columns[i], 0);
                Canvas.SetLeft(columns[i], (i - 1) * 40);
                LayoutAnimation.Children.Add(columns[i]);
            }

            LayoutAnimation.Width = size * 40;
        }

        #endregion

        /// <summary>
        /// Call function Sort chossed
        /// </summary>
        public void run(string st, int Value)
        {
            switch (st)
            {
                case "Binary":
                    Binary(Value);
                    break;

                case "Binary_Search_Tree":
                    BinarySearchTree(Value);
                    break;

                default:
                    Linear(Value);
                    break;
            }
        }

        #region Algorithm Search

        #region Linear Search
        /// <summary>
        /// Algorithm Search Linear
        /// </summary>
        /// <param name="Value">Value need find</param>
        private async void Linear(int Value)
        {
            int i = 1;
            for (i=1; i<=size; i++)
            {
                columns[i].col.BgCompare();
                await Task.Delay(time);
                if (columns[i].col.Val == Value)
                {
                    AnimationColumn.MoveColY(columns[i], Bot, time);
                    AnimationColumn.MoveColX(columns[i], (size / 2 - 1) * 40, time);
                    columns[i].col.BgLock();
                    break;
                }
                columns[i].col.BgDefault();
            }
            if (i > size) BlockCompare.Text = "Not Found!!!";
        }

        #endregion

        #region Binary Search
        
        /// <summary>
        /// Algorithm Binary Search
        /// </summary>
        /// <param name="Value"></param>
        private async void Binary(int Value)
        {
            BlockCompare.Text = "Binary Search";
        }

        #endregion

        #region Binary Search Tree

        /// <summary>
        /// Algorithm Binary Search Tree
        /// </summary>
        /// <param name="Value">Value need find</param>
        private async void BinarySearchTree(int Value)
        {
            BlockCompare.Text = "Binary Search Tree";
        }

        #endregion

        #endregion

    }
}
