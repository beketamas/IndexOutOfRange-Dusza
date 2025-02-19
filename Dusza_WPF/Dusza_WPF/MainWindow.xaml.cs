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
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;

namespace Dusza_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Eleres = "";
        private bool IsDarkModeOn = true;
        public static Button StartApps;
        public static readonly ObservableCollection<SzamitogepConfig> _szamitogepConfigok = [];
        public static readonly List<Kluszter> _klaszterLista = [];
        public static readonly List<string> _szamitogepMappakElerese = [];
        public const int EGYFOLYAMAT = 4;



        public MainWindow()
        {
            InitializeComponent();
            LoadPage();
            StartApps = btnStartApplications;

        }
        public void Vizsgal()
        {
            foreach (var item in _klaszterLista)
            {
                int futniaKene = item.MennyiActive;
                var i = _szamitogepConfigok.Sum(x => x.ProgramPeldanyAzonositok.Count(y => y.Contains(item.ProgramName)));
                if (i < futniaKene)
                    StartPulsingAnimation(btnStartApplications);
            }
        }

        private async void LoadPage()
        {
            EleresMegadas eleres = new EleresMegadas();
            Container.Content = eleres;
            Eleres = await eleres.GetResultAsync();
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
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            KluszterCucc();
            Vizsgal();
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

        private void StartPulsingAnimation(Button button)
        {
            button.Foreground = new SolidColorBrush(Colors.Red);
            ScaleTransform scale = new ScaleTransform(1, 1);
            button.RenderTransform = scale;
            button.RenderTransformOrigin = new Point(0.5, 0.5);

            DoubleAnimation growAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.2,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(growAnimation);

            Storyboard.SetTarget(growAnimation, button);
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));

            DoubleAnimation growAnimationY = growAnimation.Clone();
            Storyboard.SetTargetProperty(growAnimationY, new PropertyPath("RenderTransform.ScaleY"));
            storyboard.Children.Add(growAnimationY);

            storyboard.Begin();
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

            Container.Content = new EleresMegadas();
        }

        public void SzamitogepMappakElerese()
        {
            _szamitogepMappakElerese.Clear();
            foreach (string eleres in Directory.GetDirectories(Eleres).ToList())
            {
                _szamitogepMappakElerese.Add(eleres);
                //string[] splittelt = eleres.Split('/');
                //if (splittelt.Last().Contains("szamitogep"))
                //{
                //};
            }
            //KluszterCucc();

        }
        public void SzamitogepConfigok()
        {
            _szamitogepConfigok.Clear();
            foreach (var item in _szamitogepMappakElerese)
            {
                if (Directory.GetFiles(item) != null && Directory.GetFiles(item).Length > 0 && Directory.GetFiles(item).Any(x => x.Contains(".szamitogep_config")))
                {
                    string[] configFajl = File.ReadAllLines(item + "/.szamitogep_config");
                    var gep = new SzamitogepConfig(Convert.ToInt32(configFajl[0]), Convert.ToInt32(configFajl[1]), item, item.Split("\\").Last());

                    foreach (var programok in Directory.GetFiles(item))
                    {
                        if (!programok.Contains(".szamitogep_config"))
                        {
                            gep.ProgramPeldanyAzonositok.Add(programok.Split("\\").Last());
                        }
                    }

                    _szamitogepConfigok.Add(gep);
                }
            }
            //KluszterCucc();

        }
        public void KluszterCucc()
        {
            string kluszterPath = Directory.GetFiles(Eleres).Where(x => x.Contains(".klaszter")).First();
            _klaszterLista.Clear();
            for (int i = 0; i < File.ReadAllLines(kluszterPath).Select(x => x).ToList().Count; i++)
            {
                if (i == 0 || i % EGYFOLYAMAT == 0)
                {
                    int num = i + EGYFOLYAMAT;
                    _klaszterLista.Add(new Kluszter(string.Join(";", File.ReadAllLines(kluszterPath).Select(x => x.ToString()).ToList()[i..num]).Split(";")));
                }
            }
            //kluszterLista.ForEach(x => Console.WriteLine($"{x.ProgramName};{x.MennyiActive};{x.Millimag};{x.Memoria}"));
        }
    }
}
