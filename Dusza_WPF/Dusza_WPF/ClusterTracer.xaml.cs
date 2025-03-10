﻿using DuszaProg_IndexOutOfRange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Dusza_WPF
{
    /// <summary>
    /// Interaction logic for ClusterTracer.xaml
    /// </summary>
    public partial class ClusterTracer : Page
    {
        public const int EGYFOLYAMAT = 4;
        public static string gyoker = "";
        public static readonly string[] files = [];
        public static readonly List<string> nemHasznaltszamitogepMappakElerese = [];
        public static readonly List<string> HasznaltszamitogepMappakElerese = [];
        public static readonly List<SzamitogepConfig> hasznalatbanLevoGepek = [];
        public static readonly ObservableCollection<SzamitogepConfig> szamitogepConfigok = [];
        public static readonly Dictionary<ProgramFolyamat, string> szamitogepekenFutoAlkalmazasok = [];
        public static readonly List<Kluszter> klaszterLista = [];
        public static readonly List<string> gépNevek = [];
        public static readonly List<string> betuk = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "x", "y", "z"];



        public static readonly ObservableCollection<SzamitogepConfig> nemHasznaltgepek = [];
        readonly List<SzamitogepConfig> klaszter = [];
        readonly Dictionary<SzamitogepConfig, Line> lines = [];
        private static Point _initialMousePosition;
        public ClusterTracer(string eleres)
        {
            InitializeComponent();
            try
            {
                Inditas(eleres);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            lvGepek.ItemsSource = nemHasznaltgepek;

            hasznalatbanLevoGepek.ForEach(l =>
            {
                cKlaszter.Children.Add(l);
                Random rnd = new Random();
                double left = rnd.Next(0, Convert.ToInt32(cKlaszter.Width));
                double top = rnd.Next(0, Convert.ToInt32(cKlaszter.Height));

                if (l.PozicioX == 0 && l.PozicioY == 0)
                {
                    Canvas.SetLeft(l, left);
                    Canvas.SetTop(l, top);
                    l.PozicioX = left;
                    l.PozicioY = top;
                    File.WriteAllLines($"{l.Eleres}/.pozicio", $"{Convert.ToDouble(l.PozicioX)}\n{Convert.ToDouble(l.PozicioY)}".Split("\n"));
                }
                else
                {
                    Canvas.SetLeft(l, l.PozicioX);
                    Canvas.SetTop(l, l.PozicioY);
                }


            });
            Canvas.SetLeft(btnCenter, 220);
            Canvas.SetTop(btnCenter, 220);
        }

        #region

        private static void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is SzamitogepConfig button)
                _initialMousePosition = e.GetPosition(button);
        }

        private static void Button_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is SzamitogepConfig gep && e.LeftButton == MouseButtonState.Pressed)
            {
                var currentPosition = e.GetPosition(gep);
                var distanceMoved = (_initialMousePosition - currentPosition).Length;

                if (distanceMoved > 5)
                    DragDrop.DoDragDrop(gep, gep, DragDropEffects.Move);
            }
        }

        private static void CanvasButton_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is SzamitogepConfig button)
            {
                var currentPosition = e.GetPosition(button);
                var distanceMoved = (_initialMousePosition - currentPosition).Length;

                if (distanceMoved > 5)
                    DragDrop.DoDragDrop(button, button, DragDropEffects.Move);
            }
        }

        private void ListView_DragOver(object sender, DragEventArgs e) => e.Effects = DragDropEffects.Move;

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(SzamitogepConfig)) is SzamitogepConfig gep)
            {
                if (lines.Any(x => x.Key == gep))
                {
                    cKlaszter.Children.Remove(lines.Where(x => x.Key == gep).First().Value);
                    lines.Remove(gep);
                }

                nemHasznaltgepek.Remove(gep);
                if (VisualTreeHelper.GetParent(gep) is ContentPresenter currentParent)
                    currentParent.Content = null;

                Point dropPosition = e.GetPosition(sender as Canvas);
                Canvas.SetLeft(gep, dropPosition.X);
                Canvas.SetTop(gep, dropPosition.Y);
                gep.PozicioX = dropPosition.X;
                gep.PozicioY = dropPosition.Y;
                File.WriteAllText($"{gep.Eleres}/.pozicio", $"{Convert.ToDouble(gep.PozicioX)}\n{Convert.ToDouble(gep.PozicioY)}");

                gep.PreviewMouseMove -= Button_PreviewMouseMove;
                gep.PreviewMouseMove += CanvasButton_PreviewMouseMove;
                CreateLine(gep);
                gep.Background = new SolidColorBrush(Colors.Green);

                if (!hasznalatbanLevoGepek.Contains(gep) && !cKlaszter.Children.Contains(gep))
                {
                    hasznalatbanLevoGepek.Add(gep);
                    var destination = gyoker + $"\\hasznalatbanLevoGepek\\{gep.Eleres.Split("\\").Last()}";
                    Directory.Move(gep.Eleres, destination);
                    gep.Eleres = destination;
                }

                if (sender is Canvas klaszter && !klaszter.Children.Contains(gep))
                {
                    klaszter.Children.Add(gep);
                    this.klaszter.Add(gep);
                }
            }

            Vizsgal();
        }
        public void Vizsgal()
        {
            if (hasznalatbanLevoGepek.Count > 0)
            {
                foreach (var item in klaszterLista)
                {
                    int futniaKene = item.MennyiActive;
                    var i = hasznalatbanLevoGepek.Sum(x => x.ProgramPeldanyAzonositok.Count(y => y.Contains(item.ProgramName)));
                    if (i < futniaKene)
                    {
                        StartPulsingAnimation(MainWindow.StartApps);
                        MainWindow.ClasterManager.IsEnabled = false;
                        MainWindow.AddComputer.IsEnabled = false;
                        MainWindow.ManageApps.IsEnabled = false;
                        MainWindow.DeleteComputer.IsEnabled = false;
                        MainWindow.Path.IsEnabled = false;
                        MainWindow.ClasterManager.Opacity = 0.5;
                        MainWindow.AddComputer.Opacity = 0.5;
                        MainWindow.ManageApps.Opacity = 0.5;
                        MainWindow.DeleteComputer.Opacity = 0.5;
                        MainWindow.Path.Opacity = 0.5;
                        MainWindow.tartoka.Content = new StartApplication(gyoker);
                        DropShadowEffect dropShadowEffect = new DropShadowEffect
                        {
                            Opacity = 1,
                            BlurRadius = 10,
                            ShadowDepth = 1,
                            Color = Colors.DarkOrange
                        };
                        MainWindow.StartApps.Effect = dropShadowEffect;
                        MainWindow.ClasterManager.Effect = null;
                    }
                    else if (i > futniaKene)
                    {
                        StartPulsingAnimation(MainWindow.ManageApps);
                        MainWindow.ClasterManager.IsEnabled = false;
                        MainWindow.AddComputer.IsEnabled = false;
                        MainWindow.StartApps.IsEnabled = false;
                        MainWindow.DeleteComputer.IsEnabled = false;
                        MainWindow.Path.IsEnabled = false;
                        MainWindow.ClasterManager.Opacity = 0.5;
                        MainWindow.AddComputer.Opacity = 0.5;
                        MainWindow.StartApps.Opacity = 0.5;
                        MainWindow.DeleteComputer.Opacity = 0.5;
                        MainWindow.Path.Opacity = 0.5;
                        MainWindow.tartoka.Content = new ManageApplications(gyoker);
                        DropShadowEffect dropShadowEffect = new DropShadowEffect
                        {
                            Opacity = 1,
                            BlurRadius = 10,
                            ShadowDepth = 1,
                            Color = Colors.DarkOrange
                        };
                        MainWindow.ManageApps.Effect = dropShadowEffect;
                        MainWindow.ClasterManager.Effect = null;
                        break;
                    }
                }
                
            }
        }

        public static void StartPulsingAnimation(Button button)
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

        private void CreateLine(SzamitogepConfig gep)
        {
            Line dynamicLine = new()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            dynamicLine.SetBinding(Line.X1Property, new Binding
            {
                Source = gep,
                Path = new PropertyPath("(Canvas.Left)"),
            });

            dynamicLine.SetBinding(Line.Y1Property, new Binding
            {
                Source = gep,
                Path = new PropertyPath("(Canvas.Top)")
            });

            dynamicLine.SetBinding(Line.X2Property, new Binding
            {
                Source = btnCenter,
                Path = new PropertyPath("(Canvas.Left)")
            });

            dynamicLine.SetBinding(Line.Y2Property, new Binding
            {
                Source = btnCenter,
                Path = new PropertyPath("(Canvas.Top)")
            });
            cKlaszter.Children.Add(dynamicLine);

            dynamicLine.Loaded += (sender, e) =>
            {

                double gepCenterX = Canvas.GetLeft(gep) + gep.ActualWidth / 2;
                double gepCenterY = Canvas.GetTop(gep) + gep.ActualHeight / 2;

                double btnCenterX = Canvas.GetLeft(btnCenter) + btnCenter.ActualWidth / 2;
                double btnCenterY = Canvas.GetTop(btnCenter) + btnCenter.ActualHeight / 2;

                dynamicLine.X1 = gepCenterX;
                dynamicLine.Y1 = gepCenterY;
                dynamicLine.X2 = btnCenterX;
                dynamicLine.Y2 = btnCenterY;
            };
            lines.Add(gep, dynamicLine);
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(SzamitogepConfig)) is SzamitogepConfig gep && !nemHasznaltgepek.Contains(gep))
            {
                if (lines.Any(x => x.Key == gep))
                {
                    cKlaszter.Children.Remove(lines.Where(x => x.Key == gep).First().Value);
                    lines.Remove(gep);
                }

                if (!nemHasznaltgepek.Contains(gep))
                {
                    hasznalatbanLevoGepek.Remove(gep);
                    Directory.Move(gep.Eleres.Replace(@"/",@"\"), gyoker+ $@"\{gep.Eleres.Split(@"\").Last()}");
                    gep.Eleres = gyoker + $@"\{gep.Eleres.Split(@"\").Last()}";
                    nemHasznaltgepek.Add(gep);
                    cKlaszter.Children.Remove(gep);

                    gep.PreviewMouseMove -= CanvasButton_PreviewMouseMove;
                    gep.PreviewMouseMove += Button_PreviewMouseMove;
                    gep.Background = new SolidColorBrush(Colors.Red);
                }
            }

            Vizsgal();
        }
        #endregion

        #region
        public void Inditas(string eleres)
        {
            gyoker = eleres;
            if (Directory.GetFiles(gyoker) != null && Directory.GetFiles(gyoker).Length > 0 && Directory.GetFiles(gyoker).Any(x => x.Contains(".klaszter")))
            {
                SzamitogepMappakElerese();
                SzamitogepConfigok();
                ProgramokBeolvasása();
                KluszterCucc();
                //MenuKiiratasa();

            }
            else
            {
                MessageBox.Show("Hoppá");
            }
        }

        public static void SzamitogepMappakElerese()
        {

            nemHasznaltszamitogepMappakElerese.Clear();
            HasznaltszamitogepMappakElerese.Clear();
            hasznalatbanLevoGepek.Clear();
            foreach (string eleres in Directory.GetDirectories(gyoker))
            {
                if (!eleres.Contains("hasznalatbanLevoGepek"))
                {
                    nemHasznaltszamitogepMappakElerese.Add(eleres);
                    
                }
            }
            if (Directory.GetDirectories(gyoker + "\\hasznalatbanLevoGepek").ToList().Count != 0)
            {
                foreach (var item in Directory.GetDirectories(gyoker+ "\\hasznalatbanLevoGepek").ToList())
                {
                    HasznaltszamitogepMappakElerese.Add(item);
                }

            }


            KluszterCucc();

        }

        public void SzamitogepConfigok()
        {
            hasznalatbanLevoGepek.Clear();
            nemHasznaltgepek.Clear();
            szamitogepConfigok.Clear();
            foreach (var item in nemHasznaltszamitogepMappakElerese)
            {
                if (Directory.GetFiles(item) != null && Directory.GetFiles(item).Length > 0 && Directory.GetFiles(item).Any(x => x.Contains(".szamitogep_config")))
                {
                    if (!item.Contains("hasznalatbanLevoGepek"))
                    {
                        var gep  = Beolvasas(item);
                        gep.Background = new SolidColorBrush(Colors.Red);
                        gep.PreviewMouseMove += Button_PreviewMouseMove;
                        gep.PreviewMouseDown += Button_PreviewMouseDown;
                        szamitogepConfigok.Add(gep);
                        nemHasznaltgepek.Add(gep);
                        
                    }

                }
            }

            foreach (var item in HasznaltszamitogepMappakElerese)
            {
                if (Directory.GetFiles(item) != null && Directory.GetFiles(item).Length > 0 && Directory.GetFiles(item).Any(x => x.Contains(".szamitogep_config")))
                {
                    var gep = Beolvasas(item);
                    gep.PreviewMouseMove -= Button_PreviewMouseMove;
                    gep.PreviewMouseMove += CanvasButton_PreviewMouseMove;
                    CreateLine(gep);
                    gep.Background = new SolidColorBrush(Colors.Green);
                    hasznalatbanLevoGepek.Add(gep);
                }
            }

            KluszterCucc();

        }

        public static SzamitogepConfig Beolvasas(string item)
        {
            string[] configFajl = File.ReadAllLines(item + "/.szamitogep_config");
            var memoriaLines = File.ReadAllLines($"{item}/.tarhely");



            var gep = new SzamitogepConfig(Convert.ToInt32(configFajl[0]), Convert.ToInt32(configFajl[1]),
                item, item.Split("\\").Last(), int.Parse(memoriaLines[0]), int.Parse(memoriaLines[1]));
            try
            {
                var poziciok = File.ReadAllLines($"{item}/.pozicio");
                gep.PozicioX = Convert.ToDouble(poziciok[0]);
                gep.PozicioY = Convert.ToDouble(poziciok[1]);
            }
            catch (Exception)
            {
                gep.PozicioX = 0;
                gep.PozicioY = 0;
            }
            foreach (var programok in Directory.GetFiles(item))
            {
                if (!programok.Contains(".szamitogep_config") && !programok.Contains(".tarhely") && !programok.Contains(".pozicio"))
                {
                    gep.ProgramPeldanyAzonositok.Add(programok.Split("\\").Last());
                }
            }


            gep.Click += (s, e) =>
            {
                if (s is SzamitogepConfig szamitoGep)
                {
                    Properties properties = new(szamitoGep.Content.ToString(), szamitoGep.Eleres, szamitoGep.Millimag, szamitoGep.Memoria, szamitogepekenFutoAlkalmazasok);
                    properties.Show();

                }
            };

            return gep;

        }

        public static void ProgramokBeolvasása()
        {
            szamitogepekenFutoAlkalmazasok.Clear();
            foreach (var item in nemHasznaltszamitogepMappakElerese)
            {
                List<string> fajlokNevei = Directory.GetFiles(item).ToList();
                foreach (var fajl in fajlokNevei)
                {
                    if (!fajl.Contains(".szamitogep_config") && !fajl.Contains(".tarhely") && !fajl.Contains(".pozicio"))
                    {
                        string[] fajlElemek = File.ReadAllLines(fajl);
                        szamitogepekenFutoAlkalmazasok.Add(new ProgramFolyamat(Convert.ToDateTime(fajlElemek[0]), fajlElemek[1], Convert.ToInt32(fajlElemek[2]), Convert.ToInt32(fajlElemek[3]), fajl.Split(@"\").Last()), item);
                    }
                }
            }
            KluszterCucc();

        }

        public static void KluszterCucc()
        {
            string kluszterPath = Directory.GetFiles(gyoker).Where(x => x.Contains(".klaszter")).First();
            klaszterLista.Clear();
            for (int i = 0; i < File.ReadAllLines(kluszterPath).Select(x => x).ToList().Count; i++)
            {
                if (i == 0 || i % EGYFOLYAMAT == 0)
                {
                    int num = i + EGYFOLYAMAT;
                    klaszterLista.Add(new Kluszter(string.Join(";", File.ReadAllLines(kluszterPath).Select(x => x.ToString()).ToList()[i..num]).Split(";")));
                }
            }
            //kluszterLista.ForEach(x => Console.WriteLine($"{x.ProgramName};{x.MennyiActive};{x.Millimag};{x.Memoria}"));
        }
        #endregion
    }
}
