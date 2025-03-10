﻿using DuszaProg_IndexOutOfRange;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

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
        public static readonly List<ProgramFolyamat> _alkalmazasok = new();
        private static Storyboard _pulseStoryboard;
        private static string _gyoker = "";
        public const int EGYFOLYAMAT = 4;
        public static ListBox error;
        List<ProgramFolyamat> marAtrakottProgramok = [];


        public ManageApplications(string gyoker)
        {
            InitializeComponent();
            _gyoker = gyoker;
            Betotles();
            error = lblError;
            marAtrakottProgramok.Clear();
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
                    ApplicationProperty window = new(_klaszterLista.Where(x => x.ProgramName == lbKlaszterProgramok.SelectedItem.ToString().Split(" ")[0]).First(), _gyoker);
                    window.ShowDialog();
                    Betotles();
                }
                else MessageBox.Show("Válassz ki valamit!");
            };
            btnProgramokSzetosztasa.Click += (s, e) =>
            {
                ProgramokSzetosztasa();

            };
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            KluszterCucc();
            Vizsgal();
        }
        public void ProgramokSzetosztasa()
        {
            marAtrakottProgramok.Clear();

            List<string> programAzonositok = new();

            var sumMag = 0;
            var sumMemoria = 0;


            foreach (var gep in _szamitogepConfigok)
            {
                var memoriaLines = File.ReadAllLines($"{_gyoker}/{gep.Eleres.Split(@"\").Last()}/.tarhely");
                var maxMag = int.Parse(memoriaLines[0]);
                var maxMemoria = int.Parse(memoriaLines[1]);
                sumMag = 0;
                sumMemoria = 0;
                foreach (string azonosito in gep.ProgramPeldanyAzonositok.ToList())
                {
                    programAzonositok.Add(azonosito);
                    gep.ProgramPeldanyAzonositok.Remove(azonosito);
                    sumMag += int.Parse(File.ReadAllLines($"{_gyoker}/hasznalatbanLevoGepek/{gep.Eleres.Split(@"\").Last()}/{azonosito}")[2]);
                    sumMemoria += int.Parse(File.ReadAllLines($"{_gyoker}/hasznalatbanLevoGepek/{gep.Eleres.Split(@"\").Last()}/{azonosito}")[3]);

                }
                gep.JelenlegiMag = maxMag + sumMag;
                gep.JelenlegiMemoria = maxMemoria + sumMemoria;
                File.WriteAllText(_gyoker + $"/hasznalatbanLevoGepek/{gep.Eleres.Split(@"\").Last()}/.tarhely", $"{maxMag + sumMag}\n{sumMemoria + maxMemoria}");

            }



            foreach (string program in programAzonositok)
            {
                ProgramFolyamat aktProgram = _alkalmazasok.Where(x => x.FajlNeve.ToLower() == program.ToLower()).First();
                SzamitogepConfig? gep;
                if (aktProgram.MemoriaEroforras < aktProgram.ProcesszorEroforras)
                {
                    gep = _szamitogepConfigok?.OrderByDescending(x => x.JelenlegiMag)
                    .FirstOrDefault();
                }
                else
                {
                    gep = _szamitogepConfigok?.OrderByDescending(x => x.JelenlegiMemoria)
                    .FirstOrDefault();
                }

                
                if (gep != null)
                {
                    gep.ProgramPeldanyAzonositok.Add(program);

                    var eredetiGep = _szamitogepekenFutoAlkalmazasok.First(x => x.Key.FajlNeve == aktProgram.FajlNeve).Value;

                    string originalPath = eredetiGep;
                    string newPath = gep.Eleres;
                    string origin = $"{originalPath}\\{aktProgram.FajlNeve}";
                    string newP = $"{newPath}\\{aktProgram.FajlNeve}";
                    if (origin != newP)
                    {
                        File.Copy(origin, newP, true);
                        File.Delete($"{originalPath}\\{aktProgram.FajlNeve}");
                    }
                    Rendezes(gep, aktProgram, eredetiGep);
                }

                _alkalmazasok.Remove(aktProgram);
            }
            Betotles();
            MessageBox.Show("Sikeres rendezés!", ":D", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void Rendezes(SzamitogepConfig gep, ProgramFolyamat aktProgram, string eredetiGep)
        {

            var memoriaLines = File.ReadAllLines($"{_gyoker}/hasznalatbanLevoGepek/{gep.Eleres.Split(@"\").Last()}/.tarhely");

            if (!marAtrakottProgramok.Contains(_szamitogepekenFutoAlkalmazasok.First(x => x.Key.FajlNeve.Contains(aktProgram.FajlNeve)).Key))
            {
                var memoria = int.Parse(memoriaLines[1]) - _szamitogepekenFutoAlkalmazasok.Where(x => x.Key.FajlNeve.Contains(aktProgram.FajlNeve))
                    .Sum(x => x.Key.MemoriaEroforras);

                var millimag = int.Parse(memoriaLines[0]) - _szamitogepekenFutoAlkalmazasok.Where(x => x.Key.FajlNeve.Contains(aktProgram.FajlNeve) )
                    .Sum(x => x.Key.ProcesszorEroforras);

                marAtrakottProgramok.Add(_szamitogepekenFutoAlkalmazasok.First(x => x.Key.FajlNeve.Contains(aktProgram.FajlNeve)).Key);
                gep.JelenlegiMag = millimag;
                gep.JelenlegiMemoria = memoria;
                File.WriteAllText(_gyoker + $"/hasznalatbanLevoGepek/{gep.Eleres.Split(@"\").Last()}/.tarhely", $"{millimag}\n{memoria}");                
            }

        }

        public void Betotles()
        {

            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            KluszterCucc();
            lbKlaszterProgramok.Items.Clear();
            _klaszterLista.Select(x => x.ProgramName).ToList().ForEach(y => lbKlaszterProgramok.Items.Add($"{y} ({_szamitogepConfigok.Sum(z => z.ProgramPeldanyAzonositok.Count(g => g.Contains(y)))}db)"));
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
            };
            lbKlaszterProgramok.Items.Add(gomb);

            ObservableCollection<string> programok = [];
            foreach (var item in _szamitogepConfigok)
                item.ProgramPeldanyAzonositok.ForEach(x => programok.Add($"{item.Eleres.Split(@"\").Last()}: {x}"));

            lbProgrampeldanyok.ItemsSource = programok;
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


            var segedLista = new List<string>();
            foreach (var item in _szamitogepekenFutoAlkalmazasok)
            {
                if (item.Key.FajlNeve.Contains(valasztottProgram) && !segedLista.Contains(item.Value.Split(@"\").Last()))
                {
                    var memoria = int.Parse(File.ReadAllLines($"{_gyoker}/hasznalatbanLevoGepek/{item.Value.Split(@"\").Last()}/.tarhely")
                        .Select(x => x).ToList()[1]) 
                        + _szamitogepekenFutoAlkalmazasok.Where(x => x.Key.FajlNeve.Contains(valasztottProgram) 
                        && x.Value.Contains(item.Value.Split(@"\").Last()))
                        .Sum(x => x.Key.MemoriaEroforras);

                    var millimag = int.Parse(File.ReadAllLines($"{_gyoker}/hasznalatbanLevoGepek/{item.Value.Split(@"\").Last()}/.tarhely")
                        .Select(x => x).ToList()[0]) 
                        + _szamitogepekenFutoAlkalmazasok.Where(x => x.Key.FajlNeve.Contains(valasztottProgram) 
                        && x.Value.Contains(item.Value.Split(@"\").Last()))
                        .Sum(x => x.Key.ProcesszorEroforras);

                    File.WriteAllText(_gyoker + $"/hasznalatbanLevoGepek/{item.Value.Split(@"\").Last()}/.tarhely", $"{millimag}\n{memoria}");
                    segedLista.Add(item.Value.Split(@"\").Last());
                }
            }

            var torlendoProgramKlaszteren = _klaszterLista.Where(x => x.ProgramName == valasztottProgram).First();
            _klaszterLista.Remove(torlendoProgramKlaszteren);
            File.Delete(kluszterPath);
            _klaszterLista.ForEach(x => File.AppendAllText(kluszterPath, $"{x.KiIratas()}\n"));
            var torlendoProgramokGepeken = _szamitogepekenFutoAlkalmazasok.Where(x => x.Key.FajlNeve.Contains(valasztottProgram));

            foreach (var item in torlendoProgramokGepeken)
                File.Delete($"{item.Value}\\hasznalatbanLevoGepek\\{item.Key.FajlNeve}");

            MessageBox.Show("Sikeres Törlés!");
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            Vizsgal();
        }
        public void EgyAdottProgrampeldanyLeallitasa()
        {
            string? name = lbProgrampeldanyok.SelectedItem.ToString().Split(" ").Last();
            var gep = _szamitogepConfigok.Where(x => x.ProgramPeldanyAzonositok.Contains(name)).First().Eleres;
            var gepNev = gep.Split(@"\").Last();



            var memoria = int.Parse(File.ReadAllLines($"{_gyoker}/hasznalatbanLevoGepek/{gepNev}/.tarhely").Select(x => x).ToList()[1]) + _szamitogepekenFutoAlkalmazasok.First(x => x.Key.FajlNeve == name).Key.MemoriaEroforras;
            var millimag = int.Parse(File.ReadAllLines($"{_gyoker}/hasznalatbanLevoGepek/{gepNev}/.tarhely").Select(x => x).ToList()[0]) + _szamitogepekenFutoAlkalmazasok.First(x => x.Key.FajlNeve == name).Key.ProcesszorEroforras;
            File.WriteAllText(_gyoker + $"/hasznalatbanLevoGepek/{gepNev}/.tarhely", $"{millimag}\n{memoria}");

            File.Delete($"{gep}\\{name}");
            MessageBox.Show("Sikeres Törlés!");
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            Vizsgal();
            
        }
        public static void SzamitogepMappakElerese()
        {
            _szamitogepMappakElerese.Clear();
            if (Directory.GetDirectories(_gyoker + "\\hasznalatbanLevoGepek").ToList().Count != 0)
            {
                foreach (var item in Directory.GetDirectories(_gyoker + "\\hasznalatbanLevoGepek").ToList())
                {
                    _szamitogepMappakElerese.Add(item);
                }

            }

        }
        public static void SzamitogepConfigok()
        {
            _szamitogepConfigok.Clear();
            
            foreach (var item in _szamitogepMappakElerese)
            {
                if (Directory.GetFiles(item) != null && Directory.GetFiles(item).Length > 0 && Directory.GetFiles(item).Any(x => x.Contains(".szamitogep_config")))
                {
                    string[] configFajl = File.ReadAllLines(item + "/.szamitogep_config");
                    var memoriaLines = File.ReadAllLines($"{item}/.tarhely");

                    var gep = new SzamitogepConfig(Convert.ToInt32(configFajl[0]), Convert.ToInt32(configFajl[1]), 
                        item, item.Split("\\").Last(), int.Parse(memoriaLines[0]), int.Parse(memoriaLines[1]));

                    foreach (var programok in Directory.GetFiles(item))
                    {
                        if (!programok.Contains(".szamitogep_config") && !programok.Contains(".tarhely") && !programok.Contains(".pozicio"))
                        {
                            gep.ProgramPeldanyAzonositok.Add(programok.Split("\\").Last());
                        }
                    }

                    _szamitogepConfigok.Add(gep);
                }
            }

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
        }
        public static void ProgramokBeolvasása()
        {
            _szamitogepekenFutoAlkalmazasok.Clear();
            _alkalmazasok.Clear();
            foreach (var item in _szamitogepMappakElerese)
            {
                List<string> fajlokNevei = Directory.GetFiles(item).ToList();
                foreach (var fajl in fajlokNevei)
                {
                    if (!fajl.Contains(".szamitogep_config") && !fajl.Contains(".tarhely") && !fajl.Contains(".pozicio"))
                    {
                        string[] fajlElemek = File.ReadAllLines(fajl);
                        _szamitogepekenFutoAlkalmazasok.Add(new ProgramFolyamat(Convert.ToDateTime(fajlElemek[0]), fajlElemek[1], Convert.ToInt32(fajlElemek[2]), Convert.ToInt32(fajlElemek[3]), fajl.Split(@"\").Last()), item);
                        _alkalmazasok.Add(new ProgramFolyamat(Convert.ToDateTime(fajlElemek[0]), fajlElemek[1], Convert.ToInt32(fajlElemek[2]), Convert.ToInt32(fajlElemek[3]), fajl.Split(@"\").Last()));
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

        public void Vizsgal()
        {
            if (_szamitogepConfigok.Count > 0)
            {
                int hibak = 0;

                error.Items.Clear();
                foreach (var item in _klaszterLista)
                {
                    int futniaKene = item.MennyiActive;
                    var i = _szamitogepConfigok.Sum(x => x.ProgramPeldanyAzonositok.Count(y => y.Contains(item.ProgramName)));
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
                        MainWindow.tartoka.Content = new StartApplication(_gyoker);
                        DropShadowEffect dropShadowEffect = new DropShadowEffect
                        {
                            Opacity = 1,
                            BlurRadius = 10,
                            ShadowDepth = 1,
                            Color = Colors.DarkOrange
                        };
                        MainWindow.StartApps.Effect = dropShadowEffect;
                        MainWindow.ManageApps.Effect = null;
                        hibak++;

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

                        DropShadowEffect dropShadowEffect = new DropShadowEffect
                        {
                            Opacity = 1,
                            BlurRadius = 10,
                            ShadowDepth = 1,
                            Color = Colors.DarkOrange
                        };
                        MainWindow.ManageApps.Effect = dropShadowEffect;
                        MainWindow.ClasterManager.Effect = null;
                        error.Items.Add($"A(z) {item.ProgramName} csak {futniaKene} példányban futhat!");
                        hibak++;

                    }

                    if(hibak == 0)
                    {
                        ResetButtonAnimation(MainWindow.StartApps);
                        MainWindow.ClasterManager.IsEnabled = true;
                        MainWindow.AddComputer.IsEnabled = true;
                        MainWindow.DeleteComputer.IsEnabled = true;
                        MainWindow.Path.IsEnabled = true;
                        MainWindow.StartApps.IsEnabled = true;
                        MainWindow.ClasterManager.Opacity = 1;
                        MainWindow.AddComputer.Opacity = 1;
                        MainWindow.DeleteComputer.Opacity = 1;
                        MainWindow.Path.Opacity = 1;
                        MainWindow.StartApps.Opacity = 1;
                        ResetButtonAnimation(MainWindow.ManageApps);
                        lblError.Items.Clear();
                    }
                }
                
            }

        }

    }
}
