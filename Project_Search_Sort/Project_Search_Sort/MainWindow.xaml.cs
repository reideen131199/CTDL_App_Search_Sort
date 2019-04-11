using System;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Media.Imaging;
using System.Windows.Media;

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

            Uri resourceUri = new Uri("Images/Button_Sort.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            Btn_Sort.Background = new ImageBrush(temp);

            resourceUri = new Uri("Images/Button_Search.png", UriKind.Relative);
            streamInfo = Application.GetResourceStream(resourceUri);
            temp = BitmapFrame.Create(streamInfo.Stream);
            Btn_Search.Background = new ImageBrush(temp);

            resourceUri = new Uri("Images/Button_Exit.png", UriKind.Relative);
            streamInfo = Application.GetResourceStream(resourceUri);
            temp = BitmapFrame.Create(streamInfo.Stream);
            Btn_Exit.Background = new ImageBrush(temp);
        }

        #region Event Button on Intro

        private void Border_MouseDown_Sort(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Child_Layout = new Sort_View();
            Layout.Children.Add(Child_Layout);
            goLayout();
        }

        private void Border_MouseDown_Search(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Child_Layout = new Search_View();
            Layout.Children.Add(Child_Layout);
            goLayout();
        }

        private void Border_MouseDown_Exit(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Btn_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.BorderThickness = new Thickness(3);
        }

        private void Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.BorderThickness = new Thickness(1);
        }

        #endregion

        #region Event Button on Layout

        private void Button_BackIntro(object sender, RoutedEventArgs e)
        {
            goIntro();
            Layout.Children.Remove(Child_Layout);
        }

        #endregion

        #region Helper

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
                Intro.Visibility = Visibility.Collapsed;
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

        #endregion

    }
}
