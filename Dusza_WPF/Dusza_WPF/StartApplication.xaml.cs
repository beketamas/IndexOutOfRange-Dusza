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
            cbValasztahtoGepek.SelectedIndex = 1;
            cbValasztahtoProgramok.SelectedIndex = 1;
            btnStart.Click += (s, e) => ProgramPeldanyFuttatasa();
        }


        public void ProgramPeldanyFuttatasa()
        {

            string? programNeve = cbValasztahtoProgramok.SelectedItem.ToString();
            string? valasztottGep = cbValasztahtoGepek.SelectedItem.ToString();

            if (_szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().Memoria > _klaszterLista.Where(x => x.ProgramName == programNeve).First().Memoria &&
                _szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().Millimag > _klaszterLista.Where(x => x.ProgramName == programNeve).First().Millimag)
            {
                _szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().Memoria -= _klaszterLista.Where(x => x.ProgramName == programNeve).First().Memoria;
                _szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().Millimag -= _klaszterLista.Where(x => x.ProgramName == programNeve).First().Millimag;
                File.WriteAllLines(_gyoker + $"/{valasztottGep}/.szamitogep_config", _szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).Select(x => x.KiIratas()).ToArray());
                var startDate = DateTime.Now.ToString();
                string[] tomb = [startDate, "AKTÍV", $"{_klaszterLista.Where(x => x.ProgramName == programNeve).First().Millimag}", $"{_klaszterLista.Where(x => x.ProgramName == programNeve).First().Memoria}"];
                string randomAzonosito = "";
                Random rnd = new();
                for (int i = 0; i < 6; i++)
                    randomAzonosito += betuk[rnd.Next(0, betuk.Count)];

                File.WriteAllLines(_gyoker + $"/{valasztottGep}/{programNeve}-{randomAzonosito}", tomb);
                _szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() == valasztottGep).First().ProgramPeldanyAzonositok.Add($"{programNeve}-{randomAzonosito}");
                MessageBox.Show("Sikeres Futtatás!");
            }
            else
            {
                MessageBox.Show("Nincs elég erőforrás!");
                return;
            }
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
    }
}
