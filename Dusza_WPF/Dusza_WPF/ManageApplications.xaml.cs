using DuszaProg_IndexOutOfRange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dusza_WPF
{
    /// <summary>
    /// Interaction logic for ManageApplications.xaml
    /// </summary>
    public partial class ManageApplications : Page
    {
        public static readonly List<Kluszter> _klaszterLista = [];
        public static readonly List<string> _gépNevek = [];
        public static readonly ObservableCollection<SzamitogepConfig> _szamitogepConfigok = [];
        public static readonly List<string> _szamitogepMappakElerese = [];
        public static readonly Dictionary<ProgramFolyamat, string> _szamitogepekenFutoAlkalmazasok = [];
        private static Storyboard _pulseStoryboard;
        private static string _gyoker = "";
        public const int EGYFOLYAMAT = 4;

        public ManageApplications(string gyoker)
        {
            InitializeComponent();
            _gyoker = gyoker;
            Betotles();

            btnPeldanyLeallitasa.Click += (s, e) =>
            {
                var valasz = MessageBox.Show("Biztos hogy leállítja ezt a példányprogramot?", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (valasz == MessageBoxResult.Yes)
                {
                    EgyAdottProgrampeldanyLeallitasa();
                    Betotles();
                }
                else
                    return;

            };

            btnProgramLeallitas.Click += (s, e) =>
            {
                var valasz = MessageBox.Show("Biztos hogy leállítja ezt a programot?", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (valasz == MessageBoxResult.Yes)
                {
                    EgyProgramLeallitasa();
                    Betotles();
                }
                else
                    return;
            };

            btnProgramSzerkeztese.Click += (s, e) =>
            {
                if (lbKlaszterProgramok.SelectedItem != null)
                {
                    ApplicationProperty window = new(_klaszterLista.Where(x => x.ProgramName == lbKlaszterProgramok.SelectedItem.ToString()).First(), _gyoker);
                    window.ShowDialog();
                    Betotles();
                }
                else MessageBox.Show("Válassz ki valamit!");
            };

        }

        public void Betotles()
        {

            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            KluszterCucc();
            lbKlaszterProgramok.Items.Clear();
            _klaszterLista.Select(x => x.ProgramName).ToList().ForEach(x => lbKlaszterProgramok.Items.Add(x));
            Button gomb = new()
            {
                Width = 150,
                Height = 30,
                Content = "+",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };

            gomb.Click += (s, e) =>
            {
                AddApplication window = new(_gyoker);
                window.ShowDialog();
                Betotles();
                StartPulsingAnimation(MainWindow.StartApps);
            };
            lbKlaszterProgramok.Items.Add(gomb);

            ObservableCollection<string> programok = [];
            foreach (var item in _szamitogepConfigok)
                item.ProgramPeldanyAzonositok.ForEach(x => programok.Add($"{item.Eleres.Split(@"\").Last()}: {x}"));

            lbProgrampeldanyok.ItemsSource = programok;
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

            _pulseStoryboard = new Storyboard();
            _pulseStoryboard.Children.Add(growAnimation);
            Storyboard.SetTarget(growAnimation, button);
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));

            DoubleAnimation growAnimationY = growAnimation.Clone();
            Storyboard.SetTargetProperty(growAnimationY, new PropertyPath("RenderTransform.ScaleY"));
            _pulseStoryboard.Children.Add(growAnimationY);

            _pulseStoryboard.Begin();
        }
        public static void ResetButtonAnimation(Button button)
        {
            if (_pulseStoryboard != null)
            {
                _pulseStoryboard.Stop(); // Stop the animation
            }

            // Reset button transform
            button.RenderTransform = new ScaleTransform(1, 1);
            button.Foreground = new SolidColorBrush(Colors.White);  
        }
        public void EgyProgramLeallitasa()
        {
            string kluszterPath = Directory.GetFiles(_gyoker).Where(x => x.Contains(".klaszter")).First();

            string? valasztottProgram = lbKlaszterProgramok.SelectedItem.ToString();


            var torlendoProgramKlaszteren = _klaszterLista.Where(x => x.ProgramName == valasztottProgram).First();
            _klaszterLista.Remove(torlendoProgramKlaszteren);
            File.Delete(kluszterPath);
            _klaszterLista.ForEach(x => File.AppendAllText(kluszterPath, $"{x.KiIratas()}\n"));
            var torlendoProgramokGepeken = _szamitogepekenFutoAlkalmazasok.Where(x => x.Key.FajlNeve.Contains(valasztottProgram));

            foreach (var item in torlendoProgramokGepeken)
                File.Delete($"{item.Value}\\{item.Key.FajlNeve}");

            MessageBox.Show("Sikeres Törlés!");
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
        }
        public void EgyAdottProgrampeldanyLeallitasa()
        {
            string? name = lbProgrampeldanyok.SelectedItem.ToString().Split(" ").Last();


            var gep = _szamitogepConfigok.Where(x => x.ProgramPeldanyAzonositok.Contains(name)).First().Eleres;
            File.Delete($"{gep}\\{name}");
            MessageBox.Show("Sikeres Törlés!");
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
        }
        public static void SzamitogepMappakElerese()
        {
            _szamitogepMappakElerese.Clear();
            foreach (string eleres in Directory.GetDirectories(_gyoker).ToList())
            {
                _szamitogepMappakElerese.Add(eleres);
                //string[] splittelt = eleres.Split('/');
                //if (splittelt.Last().Contains("szamitogep"))
                //{
                //};
            }
            //KluszterCucc();

        }
        public static void SzamitogepConfigok()
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
        public static void KluszterCucc()
        {
            string kluszterPath = Directory.GetFiles(_gyoker).Where(x => x.Contains(".klaszter")).First();
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
        public static void ProgramokBeolvasása()
        {
            _szamitogepekenFutoAlkalmazasok.Clear();
            foreach (var item in _szamitogepMappakElerese)
            {
                List<string> fajlokNevei = Directory.GetFiles(item).ToList();
                foreach (var fajl in fajlokNevei)
                {
                    if (!fajl.Contains(".szamitogep_config"))
                    {
                        string[] fajlElemek = File.ReadAllLines(fajl);
                        _szamitogepekenFutoAlkalmazasok.Add(new ProgramFolyamat(Convert.ToDateTime(fajlElemek[0]), fajlElemek[1], Convert.ToInt32(fajlElemek[2]), Convert.ToInt32(fajlElemek[3]), fajl.Split(@"\").Last()), item);
                    }
                }
            }
            KluszterCucc();

        }
        private void lbKlaszterProgramok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnProgramLeallitas.IsEnabled = lbKlaszterProgramok.SelectedIndex != -1;
            btnProgramSzerkeztese.IsEnabled = lbKlaszterProgramok.SelectedIndex != -1;
            lbProgrampeldanyok.SelectedIndex = -1;
        }
        private void lbProgrampeldanyok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnPeldanyLeallitasa.IsEnabled = lbProgrampeldanyok.SelectedIndex != -1;
            lbKlaszterProgramok.SelectedIndex = -1;
        }

        private void btnProgramokSzetosztasa_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
