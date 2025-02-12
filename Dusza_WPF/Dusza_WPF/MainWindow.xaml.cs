﻿using DuszaProg_IndexOutOfRange;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Navigation;

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
        public MainWindow() : this("") { }

        public MainWindow(string eleres)
        {
            InitializeComponent(); // ✅ Ensure InitializeComponent() is called
            this.Eleres = eleres;
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
            btnDeleteComputer.Effect = null;

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
            btnDeleteComputer.Effect = null;

            Container.Content = new AddComputer(Eleres);
        }
    }
}
