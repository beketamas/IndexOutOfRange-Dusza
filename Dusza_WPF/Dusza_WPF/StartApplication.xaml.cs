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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dusza_WPF
{
    /// <summary>
    /// Interaction logic for StartApplication.xaml
    /// </summary>
    public partial class StartApplication : Page
    {
        private static List<string> betuk = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "x", "y", "z"];
        public const int EGYFOLYAMAT = 4;
        public static readonly List<Kluszter> _klaszterLista = [];
        public static readonly List<string> _gépNevek = [];
        public static readonly ObservableCollection<SzamitogepConfig> _szamitogepConfigok = [];
        public static readonly List<string> _szamitogepMappakElerese = [];

        private static string _gyoker;

        public StartApplication(string gyoker)
        {
            InitializeComponent();
            _gyoker = gyoker;
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            KluszterCucc();
            _gépNevek.Clear();
            cbValasztahtoProgramok.ItemsSource = _klaszterLista.Select(x => x.ProgramName);
            _szamitogepConfigok.ToList().ForEach(x => _gépNevek.Add($"{x.Eleres.Split(@"\").Last()}"));
            cbValasztahtoGepek.ItemsSource = _gépNevek;
            btnStart.Click += (s, e) => ProgramPeldanyFuttatasa();
            Vizsgal();


        }

        public void Vizsgal()
        {
            if (_szamitogepConfigok.Count >0)
            {
                lblWarning.Items.Clear();
                int hibak = 0;
                foreach (var item in _klaszterLista)
                {
                    int futniaKene = item.MennyiActive;
                    var i = _szamitogepConfigok.Sum(x => x.ProgramPeldanyAzonositok.Count(y => y.Contains(item.ProgramName)));
                    if (i < futniaKene)
                    {
                        lblWarning.Items.Add($"{futniaKene} {item.ProgramName}-nak/nek futnia kell! (Jelenleg {i}db fut)");
                        hibak++;
                    }

                    else if (i > futniaKene)
                    {
                        MainWindow.ClasterManager.IsEnabled = false;
                        MainWindow.AddComputer.IsEnabled = false;
                        MainWindow.DeleteComputer.IsEnabled = false;
                        MainWindow.Path.IsEnabled = false;
                        MainWindow.StartApps.IsEnabled = false;
                        MainWindow.ClasterManager.Opacity = 0.5;
                        MainWindow.AddComputer.Opacity = 0.5;
                        MainWindow.DeleteComputer.Opacity = 0.5;
                        MainWindow.Path.Opacity = 0.5;
                        MainWindow.StartApps.Opacity = 0.5;
                        DropShadowEffect dropShadowEffect = new DropShadowEffect
                        {
                            Opacity = 1,
                            BlurRadius = 10,
                            ShadowDepth = 1,
                            Color = Colors.DarkOrange
                        };
                        MainWindow.StartApps.Effect = null;
                        MainWindow.ManageApps.Effect = dropShadowEffect;
                        ManageApplications.StartPulsingAnimation(MainWindow.ManageApps);
                        MainWindow.tartoka.Content = new ManageApplications(_gyoker);
                        hibak++;
                        ManageApplications.error.Items.Add($"A(z) {item.ProgramName} csak {futniaKene} példányban futhat!");
                        
                    }
                }
                if (hibak == 0)
                {
                    ManageApplications.ResetButtonAnimation(MainWindow.StartApps);
                    MainWindow.ClasterManager.IsEnabled = true;
                    MainWindow.AddComputer.IsEnabled = true;
                    MainWindow.ManageApps.IsEnabled = true;
                    MainWindow.DeleteComputer.IsEnabled = true;
                    MainWindow.Path.IsEnabled = true;
                    MainWindow.ClasterManager.Opacity = 1;
                    MainWindow.AddComputer.Opacity = 1;
                    MainWindow.ManageApps.Opacity = 1;
                    MainWindow.DeleteComputer.Opacity = 1;
                    MainWindow.Path.Opacity = 1;
                }
                
            }
        }

        public void ProgramPeldanyFuttatasa()
        {

            string? programNeve = cbValasztahtoProgramok.SelectedItem.ToString();
            string? valasztottGep = cbValasztahtoGepek.SelectedItem.ToString();

            if (_szamitogepConfigok.First(x => x.Eleres.Split(@"\").Last() == valasztottGep).Memoria > _klaszterLista.First(x => x.ProgramName == programNeve).Memoria &&
                _szamitogepConfigok.First(x => x.Eleres.Split(@"\").Last() == valasztottGep).Millimag > _klaszterLista.First(x => x.ProgramName == programNeve).Millimag)
            {

                var memoria = int.Parse(File.ReadAllLines($"{_gyoker}/hasznalatbanLevoGepek/{valasztottGep}/.tarhely").Select(x => x).ToList()[1]) - _klaszterLista.First(x => x.ProgramName == programNeve).Memoria;
                var millimag = int.Parse(File.ReadAllLines($"{_gyoker}/hasznalatbanLevoGepek/{valasztottGep}/.tarhely").Select(x => x).ToList()[0]) - _klaszterLista.First(x => x.ProgramName == programNeve).Millimag;
                //File.WriteAllLines(_gyoker + $"/{valasztottGep}/.szamitogep_config", _szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).Select(x => x.KiIratas()).ToArray());
                File.WriteAllText(_gyoker + $"/hasznalatbanLevoGepek/{valasztottGep}/.tarhely", $"{millimag}\n{memoria}");
                var startDate = DateTime.Now.ToString();
                string[] tomb = [startDate, "AKTÍV", $"{_klaszterLista.First(x => x.ProgramName == programNeve).Millimag}", $"{_klaszterLista.First(x => x.ProgramName == programNeve).Memoria}"];
                string randomAzonosito = "";
                Random rnd = new();
                for (int i = 0; i < 6; i++)
                    randomAzonosito += betuk[rnd.Next(0, betuk.Count)];

                File.WriteAllLines(_gyoker + $"/hasznalatbanLevoGepek/{valasztottGep}/{programNeve}-{randomAzonosito}", tomb);
                _szamitogepConfigok.First(x => x.Eleres.Split(@"\").Last() == valasztottGep).ProgramPeldanyAzonositok.Add($"{programNeve}-{randomAzonosito}");
                cbValasztahtoProgramok.SelectedIndex = -1;
                cbValasztahtoGepek.SelectedIndex = -1;
                lblGepMemoria.Content = "";
                lblGepMillimag.Content = "";
                lblProgramMemoria.Content = "";
                lblProgramMillimag.Content = "";
                MessageBox.Show("Sikeres Futtatás!", "Yay", MessageBoxButton.OK, MessageBoxImage.Information);
                Vizsgal();
            }
            else
            {
                MessageBox.Show("Nincs elég erőforrás!", "Hűha", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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

        private void cbValasztahtoProgramok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbValasztahtoProgramok.SelectedIndex != -1)
            {
                string? programNeve = cbValasztahtoProgramok.SelectedItem.ToString();
                var valasztottProgram = _klaszterLista.First(x => x.ProgramName == programNeve);
                lblProgramMemoria.Content = $"Memória: {valasztottProgram.Memoria}MB";
                lblProgramMillimag.Content = $"Millimag: {valasztottProgram.Millimag}";                
            }

        }

        private void cbValasztahtoGepek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbValasztahtoGepek.SelectedIndex != -1)
            {
                string? valasztottGep = cbValasztahtoGepek.SelectedItem.ToString();
                var gep = _szamitogepConfigok.First(x => x.Eleres.Split(@"\").Last() == valasztottGep);
                var sumMemoria = 0;
                gep.ProgramPeldanyAzonositok.Select(x => x.Split("-")[0].ToString()).ToList().ForEach(x => sumMemoria += _klaszterLista.First(y => y.ProgramName == x).Memoria);

                var sumMillimag = 0;
                gep.ProgramPeldanyAzonositok.Select(x => x.Split("-")[0].ToString()).ToList().ForEach(x => sumMillimag += _klaszterLista.First(y => y.ProgramName == x).Millimag);
                lblGepMemoria.Content = $"Tárhely: {sumMemoria}MB/{gep.Memoria}MB";
                lblGepMillimag.Content = $"Millimag: {sumMillimag}/{gep.Millimag}";                
            }
        }

    }
}
