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
        private bool pause = false;
        private Column_Control[] columns;
        private double Bot = -280;

        private int[] arr;


        // Get Set Time
        public int Time
        {
            get { return time; }
            set { time = 1100 - value; }
        }

        public bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }

        #endregion

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

            size = (dest.Length < 1) ? 0 : dest.Length - 1;
            columns = new Column_Control[size + 1];

            CreateCol(dest);

            arr = dest;
        }

        #endregion

        #region Algorithm Search

        #region Linear Search

        /// <summary>
        /// Algorithm Search Linear
        /// </summary>
        /// <param name="Value">Value need find</param>
        public async Task Linear(int Value)
        {
            int i = 1;
            for (i=1; i<=size; i++)
            {
                columns[i].col.BgCompare();

                // Pause
                await PauseAnimation();

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
        public async Task Binary(int Value)
        {
            int left = 1, right = size, mid = left;

            time = time + 300;

            columns[left].col.BgKey();
            columns[right].col.BgKey();

            // Pause
            await PauseAnimation();

            await Task.Delay(time);

            while (left <= right && mid <= size && mid >= 1)
            {
                mid = (left + right) / 2;
                columns[mid].col.BgCompare();

                // Pause
                await PauseAnimation();

                await Task.Delay(time);

                if (columns[mid].col.Val == Value)
                {
                    columns[left].col.BgDefault();
                    columns[right].col.BgDefault();

                    columns[mid].col.BgLock();

                    AnimationColumn.MoveColY(columns[mid], Bot, time);
                    AnimationColumn.MoveColX(columns[mid], (size / 2 - 1) * 40, time);
                    return;
                }
                else if (columns[mid].col.Val > Value)
                {
                    columns[right].col.BgDefault();
                    columns[mid].col.BgDefault();
                    right = mid - 1;
                    if (right >= 1) columns[right].col.BgKey();
                }
                else
                {
                    columns[left].col.BgDefault();
                    columns[mid].col.BgDefault();
                    left = mid + 1;
                    if (left <= size) columns[left].col.BgKey();
                    
                }

                // Pause
                await PauseAnimation();

                await Task.Delay(time);
            }

            if (left <= size) columns[left].col.BgDefault();
            if (right >= 1) columns[right].col.BgDefault();
            if (columns[mid].col.Val != Value) BlockCompare.Text = "Not Found!!";
        }

        #endregion

        /* #region Binary Search Tree

        /// <summary>
        /// Algorithm Binary Search Tree
        /// </summary>
        /// <param name="Value">Value need find</param>
        public async void BinarySearchTree(int Value)
        {
            BlockCompare.Text = "Binary Search Tree";
        }

        #endregion
        */

        #endregion

        #region Helper
        
        /// <summary>
        /// Search not Animation
        /// </summary>
        /// <param name="Value">Value need find</param>
        public void SearchFast(int Value)
        {
            int i = 1;
            for (i = 1; i <= size; i++)
            {
                if (columns[i].col.Val == Value)
                {
                    AnimationColumn.MoveColY(columns[i], Bot, time);
                    AnimationColumn.MoveColX(columns[i], (size / 2 - 1) * 40, time);
                    columns[i].col.BgLock();
                    break;
                }
            }
            if (i > size) BlockCompare.Text = "Not Found!!!";
        }

        /// <summary>
        /// Create Layout Columns
        /// </summary>
        /// <param name="dest">Array Value for Columns</param>
        private void CreateCol(int[] dest)
        {
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

        /// <summary>
        /// Function Pause
        /// </summary>
        /// <returns></returns>
        private async Task PauseAnimation()
        {
            while (pause) { await Task.Delay(500); }
        }

        #endregion
    }
}
