using System;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace Project_Search_Sort
{
    class AnimationControl
    {
        public AnimationControl() { }

        #region Animation Ox

        /// <summary>
        /// Exchange position 2 column control on time
        /// </summary>
        /// <param name="col1">Column 1</param>
        /// <param name="col2">Column 2</param>
        /// <param name="time">Time Exchange(Miliseconds)</param>
        public static void ExchangeColX(UserControl col1, UserControl col2, double time)
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
        public static void MoveColX(UserControl control, double pos, double time)
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
        public static void ExchangeColY(UserControl col1, UserControl col2, double time)
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
        public static void MoveColY(UserControl control, double pos, double time)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation(pos, new Duration(TimeSpan.FromMilliseconds(time)));
            control.BeginAnimation(Canvas.BottomProperty, doubleAnimation);
        }

        #endregion

    }
}
