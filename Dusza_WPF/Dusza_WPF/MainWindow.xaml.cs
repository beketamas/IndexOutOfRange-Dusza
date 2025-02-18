using DuszaProg_IndexOutOfRange;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Navigation;
using System.Xml.Linq;
using System.IO;

namespace Dusza_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Eleres = "";
        private bool IsDarkModeOn = true;

        // ✅ Added parameterless constructor
        public MainWindow() : this("") {}

        public MainWindow(string eleres)
        {
            InitializeComponent();

            Eleres = eleres;
            if (Eleres != "")
            {
                btnStartApplications.IsEnabled = true;
                btnAddComputer.IsEnabled = true;
                btnManageApplications.IsEnabled = true;
                btnManager.IsEnabled = true;
                btnDeleteComputer.IsEnabled = true;
                btnStartApplications.Opacity = 1;
                btnAddComputer.Opacity = 1;
                btnManageApplications.Opacity = 1;
                btnManager.Opacity = 1;
                btnStartApplications.Opacity = 1;
                btnDeleteComputer.Opacity = 1;
            }
            Container.Content = new EleresMegadas(this);

        }

        private void btnManager_Click(object sender, RoutedEventArgs e)
        {
            DropShadowEffect dropShadowEffect = new DropShadowEffect
            {
                Opacity = 1,
                BlurRadius = 10,
                ShadowDepth = 1,
                Color = Colors.DarkOrange
            };

            btnStartApplications.Effect = null;
            btnAddComputer.Effect = null;
            btnManager.Effect = dropShadowEffect;
            btnManageApplications.Effect = null;
            btnEleres.Effect = null;

            Container.Content = new ClusterTracer(Eleres);
        }



        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (IsDarkModeOn)
            {
                btnThemeToggle.Content = "🌙";
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("LightTheme.xaml", UriKind.Relative)
                });

                // ✅ Added null-checks to prevent crashes
                window?.SetValue(BackgroundProperty, Brushes.White);
                navbar?.SetValue(BackgroundProperty, Brushes.White);

                IsDarkModeOn = false;
            }
            else
            {
                btnThemeToggle.Content = "☀";
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("DarkTheme.xaml", UriKind.Relative)
                });

                window?.SetValue(BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF262627")));
                navbar?.SetValue(BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343538")));

                IsDarkModeOn = true;
            }
        }

        private void btnAddComputer_Click(object sender, RoutedEventArgs e)
        {
            DropShadowEffect dropShadowEffect = new DropShadowEffect
            {
                Opacity = 1,
                BlurRadius = 10,
                ShadowDepth = 1,
                Color = Colors.DarkOrange
            };

            btnStartApplications.Effect = null;
            btnAddComputer.Effect = dropShadowEffect;
            btnManager.Effect = null;
            btnManageApplications.Effect = null;
            btnEleres.Effect = null;

            Container.Content = new AddComputer(Eleres);
        }

        private void btnStartApplications_Click(object sender, RoutedEventArgs e)
        {
            DropShadowEffect dropShadowEffect = new DropShadowEffect
            {
                Opacity = 1,
                BlurRadius = 10,
                ShadowDepth = 1,
                Color = Colors.DarkOrange
            };

            btnStartApplications.Effect = dropShadowEffect;
            btnAddComputer.Effect = null;
            btnManager.Effect = null;
            btnEleres.Effect = null;
            btnManageApplications.Effect = null;

            Container.Content = new StartApplication(Eleres);
        }

        private void btnManageApplications_Click(object sender, RoutedEventArgs e)
        {
            DropShadowEffect dropShadowEffect = new DropShadowEffect
            {
                Opacity = 1,
                BlurRadius = 10,
                ShadowDepth = 1,
                Color = Colors.DarkOrange
            };

            btnStartApplications.Effect = null;
            btnAddComputer.Effect = null;
            btnManager.Effect = null;
            btnEleres.Effect = null;
            btnManageApplications.Effect = dropShadowEffect;

            Container.Content = new ManageApplications(Eleres);
        }

        private void btnEleres_Click(object sender, RoutedEventArgs e)
        {
            DropShadowEffect dropShadowEffect = new DropShadowEffect
            {
                Opacity = 1,
                BlurRadius = 10,
                ShadowDepth = 1,
                Color = Colors.DarkOrange
            };

            btnStartApplications.Effect = null;
            btnAddComputer.Effect = null;
            btnManager.Effect = null;
            btnManageApplications.Effect = null;
            btnEleres.Effect = dropShadowEffect;

            Container.Content = new EleresMegadas(this);
        }
    }
}
