using System;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for ViewTreeSort_Control.xaml
    /// </summary>
    public partial class ViewTreeSort_Control : UserControl
    {
        #region Private Value

        private int[] arr;
        private int size;
        private int time = 500;
        private Node_Control[] nodesTree;
        private Node_Control[] nodes;

        private Line[] lines;

        private bool pause = false;

        private double PosTop = 350;

        // Get Set
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

        public ViewTreeSort_Control(int[] a)
        {
            InitializeComponent();

            int[] dest = new int[a.Length + 1];
            Array.Copy(a, 0, dest, 1, a.Length);

            arr = dest;
            size = dest.Length - 1;

            CreateLayoutTree();
            CreateLayoutArray();
        }

        #endregion

        #region Algorithm: Heap Sort

        private int heapSize;

        private async Task Heapify(int index)
        {
            // Pause
            if (pause) await PauseAnimation();

            int left = 2 * index;
            int right = 2 * index + 1;
            int largest = index;

            nodes[index].node.BgCompare();
            nodesTree[index].node.BgCompare();

            if (left <= heapSize)
            {
                nodes[left].node.BgCompare();
                nodesTree[left].node.BgCompare();

                if (arr[left] > arr[index])
                {
                    largest = left;
                }
            }

            if (right <= heapSize)
            {
                nodes[right].node.BgCompare();
                nodesTree[right].node.BgCompare();
                if (arr[right] > arr[largest])
                {
                    largest = right;
                }
            }

            await Task.Delay(time);

            if (left <= heapSize)
            {
                nodes[left].node.BgDefault();
                nodesTree[left].node.BgKey();
            }

            if (right <= heapSize)
            {
                nodes[right].node.BgDefault();
                nodesTree[right].node.BgKey();
            }

            nodes[largest].node.BgCompare();
            nodesTree[largest].node.BgCompare();

            // Pause
            if (pause) await PauseAnimation();

            if (largest != index)
            {
                nodes[largest].node.BgDefault();
                nodesTree[largest].node.BgKey();

                await Swap(index, largest);

                await Heapify(largest);
            }

            nodes[largest].node.BgDefault();
            nodesTree[largest].node.BgKey();
        }

        public async Task PerformHeapSort()
        {
            heapSize = size;
            for (int i = heapSize / 2; i > 0; i--)
                await Heapify(i);

            for (int i = heapSize; i > 1; i--)
            {

                // Pause
                if (pause) await PauseAnimation();

                await Swap(1, i);

                nodes[heapSize].node.BgLock();
                nodesTree[heapSize--].node.BgLock();

                await Heapify(1);

                // Pause
                if (pause) await PauseAnimation();

            }

            nodes[1].node.BgLock();
            nodesTree[1].node.BgLock();
        }

        #endregion

        #region Helper

        /// <summary>
        /// Swap 2 Value
        /// </summary>
        /// <param name="i">value 1</param>
        /// <param name="j">value 2</param>
        /// <returns>End Task</returns>
        private async Task Swap(int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;

            AnimationControl.ExchangeColX(nodes[i], nodes[j], time);
            AnimationControl.ExchangeColY(nodes[i], nodes[j], time);

            AnimationControl.ExchangeColX(nodesTree[i], nodesTree[j], time);
            AnimationControl.ExchangeColY(nodesTree[i], nodesTree[j], time);

            await Task.Delay(time + 100);

            Node_Control tempNode = nodes[i];
            nodes[i] = nodes[j];
            nodes[j] = tempNode;

            tempNode = nodesTree[i];
            nodesTree[i] = nodesTree[j];
            nodesTree[j] = tempNode;
        }

        /// <summary>
        /// Create Layout for Array
        /// </summary>
        private void CreateLayoutArray()
        {
            nodes = new Node_Control[size + 1];
            for (int i=1; i<= size; i++)
            {
                nodes[i] = new Node_Control();
                nodes[i].node.Val = arr[i];

                nodes[i].node.BgDefault();

                Canvas.SetBottom(nodes[i], 30);
                Canvas.SetLeft(nodes[i], (i - 1) * 50);

                LayoutArray.Children.Add(nodes[i]);
            }
            LayoutArray.Width = size * 50;
        }

        /// <summary>
        /// Create Layout for Tree
        /// </summary>
        private void CreateLayoutTree()
        {
            int maxFloor = calFloorTree(size);
            LayoutTree.Width = 70 * Math.Pow(2, (maxFloor - 1));

            #region Draw Node for Tree

            nodesTree = new Node_Control[size + 1];
            for (int i = 1; i <= size; i++)
            {
                nodesTree[i] = new Node_Control();
                nodesTree[i].node.Val = arr[i];
                Canvas.SetBottom(nodesTree[i], PosTop - (calFloorTree(i) - 1) * 75);
                Canvas.SetLeft(nodesTree[i], calCanvasLeft(i, maxFloor));
                LayoutTree.Children.Add(nodesTree[i]);
            }

            #endregion

            #region Draw Line

            lines = new Line[size + 1];
            for (int i=2; i<=size; i++)
            {
                int parent = i / 2;

                lines[i] = new Line();
                lines[i].Stroke = Brushes.YellowGreen;
                lines[i].StrokeThickness = 3;

                double LayoutTreeHeight = LayoutTree.ActualHeight + 380;

                lines[i].X1 = Canvas.GetLeft(nodesTree[parent]) + 20;
                lines[i].Y1 = LayoutTreeHeight - Canvas.GetBottom(nodesTree[parent]) + 40;

                lines[i].X2 = Canvas.GetLeft(nodesTree[i]) + 20;
                lines[i].Y2 = LayoutTreeHeight - Canvas.GetBottom(nodesTree[i]) - 2;

                LayoutTree.Children.Add(lines[i]);
            }

            #endregion
        }

        /// <summary>
        /// Calculator floor of element i in array
        /// </summary>
        /// <param name="i">Element i</param>
        /// <returns>Floor of Tree</returns>
        private int calFloorTree(int i)
        {
            return (int)(Math.Log(i) / Math.Log(2)) + 1;
        }

        /// <summary>
        /// Calculator position Ox of Node i on Canvas
        /// </summary>
        /// <param name="i">Node i need calculator</param>
        /// <param name="maxFloor">Height Floor</param>
        /// <returns>Position Ox of Node i</returns>
        private double calCanvasLeft(int i, int maxFloor)
        {
            int floor = calFloorTree(i);
            double step = Math.Pow(2, maxFloor - floor) * 70;
            int NodeThOfFloor = i - (int)Math.Pow(2, floor - 1);

            double space = 0;
            if (maxFloor != floor) space = 35 * (Math.Pow(2, maxFloor - floor) - 1);

            return space + step * NodeThOfFloor;
        }

        /// <summary>
        /// Compare between 2 Value
        /// </summary>
        /// <param name="val1">Value 1</param>
        /// <param name="val2">Value 2</param>
        /// <param name="type">If type=1, val1 > val2. If type=0, val1 ... val2 (ahihi) </param>
        /// <returns></returns>
        private bool CompareValue(int val1, int val2, int type)
        {
            if (type == 1)
                return val1 > val2;
            return val1 < val2;
        }

        /// <summary>
        /// Function Pause
        /// </summary>
        /// <returns></returns>
        private async Task PauseAnimation()
        {
            while (pause) { await Task.Delay(500); }
        }

        /// <summary>
        /// BgLock for all columns
        /// </summary>
        public void LockAll()
        {
            for (int i = 1; i <= size; i++)
            {
                nodes[i].node.BgLock();
                nodesTree[i].node.BgLock();
            }
        }
        #endregion
    }
}
