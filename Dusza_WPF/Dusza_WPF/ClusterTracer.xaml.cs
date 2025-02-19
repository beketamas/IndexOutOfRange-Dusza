using DuszaProg_IndexOutOfRange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static readonly List<string> szamitogepMappakElerese = [];
        public static readonly ObservableCollection<SzamitogepConfig> szamitogepConfigok = [];
        public static readonly Dictionary<ProgramFolyamat, string> szamitogepekenFutoAlkalmazasok = [];
        public static readonly List<Kluszter> klaszterLista = [];
        public static readonly List<string> gépNevek = [];
        public static readonly List<string> betuk = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "x", "y", "z"];



        public static readonly ObservableCollection<SzamitogepConfig> gepek = [];
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
            lvGepek.ItemsSource = gepek;
            Canvas.SetLeft(btnCenter, 230);

            Canvas.SetTop(btnCenter, 210);
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

        private void CanvasButton_PreviewMouseMove(object sender, MouseEventArgs e)
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

                gepek.Remove(gep);
                if (VisualTreeHelper.GetParent(gep) is ContentPresenter currentParent)
                    currentParent.Content = null;

                Point dropPosition = e.GetPosition(sender as Canvas);
                Canvas.SetLeft(gep, dropPosition.X);
                Canvas.SetTop(gep, dropPosition.Y);

                if (sender is Canvas klaszter && !klaszter.Children.Contains(gep))
                {
                    klaszter.Children.Add(gep);
                    this.klaszter.Add(gep);
                }

                gep.PreviewMouseMove -= Button_PreviewMouseMove;
                gep.PreviewMouseMove += CanvasButton_PreviewMouseMove;
                CreateLine(gep);
                gep.Background = new SolidColorBrush(Colors.Green);
            }
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
            if (e.Data.GetData(typeof(SzamitogepConfig)) is SzamitogepConfig gep && !gepek.Contains(gep))
            {
                if (lines.Any(x => x.Key == gep))
                {
                    cKlaszter.Children.Remove(lines.Where(x => x.Key == gep).First().Value);
                    lines.Remove(gep);
                }

                gepek.Add(gep);
                cKlaszter.Children.Remove(gep);

                gep.PreviewMouseMove -= CanvasButton_PreviewMouseMove;
                gep.PreviewMouseMove += Button_PreviewMouseMove;
                gep.Background = new SolidColorBrush(Colors.Red);
            }
        }
        #endregion

        #region
        public static void Inditas(string eleres)
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
            szamitogepMappakElerese.Clear();
            foreach (string eleres in Directory.GetDirectories(gyoker).ToList())
            {
                szamitogepMappakElerese.Add(eleres);
                //string[] splittelt = eleres.Split('/');
                //if (splittelt.Last().Contains("szamitogep"))
                //{
                //};
            }
            KluszterCucc();

        }

        public static void SzamitogepConfigok()
        {
            gepek.Clear();
            szamitogepConfigok.Clear();
            foreach (var item in szamitogepMappakElerese)
            {
                if (Directory.GetFiles(item) != null && Directory.GetFiles(item).Length > 0 && Directory.GetFiles(item).Any(x => x.Contains(".szamitogep_config")))
                {
                    string[] configFajl = File.ReadAllLines(item + "/.szamitogep_config");
                    var gep = new SzamitogepConfig(Convert.ToInt32(configFajl[0]), Convert.ToInt32(configFajl[1]), item, item.Split("\\").Last());
                    gep.Background = new SolidColorBrush(Colors.Red);
                    gep.PreviewMouseMove += Button_PreviewMouseMove;
                    gep.PreviewMouseDown += Button_PreviewMouseDown;
                    foreach (var programok in Directory.GetFiles(item))
                    {
                        if (!programok.Contains(".szamitogep_config"))
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
                    szamitogepConfigok.Add(gep);
                    gepek.Add(gep);
                }
            }
            KluszterCucc();

        }

        public static void ProgramokBeolvasása()
        {
            szamitogepekenFutoAlkalmazasok.Clear();
            foreach (var item in szamitogepMappakElerese)
            {
                List<string> fajlokNevei = Directory.GetFiles(item).ToList();
                foreach (var fajl in fajlokNevei)
                {
                    if (!fajl.Contains(".szamitogep_config"))
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
