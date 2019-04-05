﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Project_Search_Sort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl Child_Layout;

        public MainWindow()
        {
            InitializeComponent();

            /*
            int[] a = { -1, 10, 64, 7, 52, 32, 18, 2, 48, 1, 99 };
            //int[] a = {-1, 1, 1, 5, 5, 8, 9, 2, 2, 3, 1 };
            Sort s = new Sort(10, a);
            s.CountingSort();
            a = s.getArray();
            for (int i = 1; i <= 10; i++)
            {
                //sx.Text += a[i];
                //sx.Text += " ";
            }
            */
        }

        private void goLayout()
        {
            DoubleAnimation show = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.75)));
            DoubleAnimation hide = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.75)));
            DoubleAnimation resize = new DoubleAnimation(30, new Duration(TimeSpan.FromSeconds(0.5)));

            BackIntro.Visibility = Visibility.Visible;
            BackIntro.Opacity = 0;

            BackIntro.BeginAnimation(OpacityProperty, show);
            Layout.BeginAnimation(OpacityProperty, show);
            Intro.BeginAnimation(OpacityProperty, hide);
            Eagle_Team.BeginAnimation(FontSizeProperty, resize);

            Task.Run(async () => {
                await Task.Delay(1000);
                Intro.Visibility = Visibility.Hidden;
            });
        }

        private void goIntro()
        {
            DoubleAnimation show = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.75)));
            DoubleAnimation hide = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.75)));
            DoubleAnimation resize = new DoubleAnimation(40, new Duration(TimeSpan.FromSeconds(0.5)));

            Intro.Visibility = Visibility.Visible;
            Intro.BeginAnimation(OpacityProperty, show);
            Layout.BeginAnimation(OpacityProperty, hide);
            BackIntro.BeginAnimation(OpacityProperty, hide);
            Eagle_Team.BeginAnimation(FontSizeProperty, resize);

            Layout.Children.Remove(Child_Layout);

            Task.Run(async () => {
                await Task.Delay(1000);
                BackIntro.Visibility = Visibility.Hidden;
            });
        }

        #region Event Button on Intro
        private void Button_Sort(object sender, RoutedEventArgs e)
        {
            Child_Layout = new Sort_View();
            Layout.Children.Add(Child_Layout);
            goLayout();

        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            //Child_Layout = new Search_View();
            //Layout.Children.Add(Child_Layout);
            goLayout();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Event Button on Layout

        private void Button_BackIntro(object sender, RoutedEventArgs e)
        {
            goIntro();
            Layout.Children.Remove(Child_Layout);
        }

        #endregion

    }
}
