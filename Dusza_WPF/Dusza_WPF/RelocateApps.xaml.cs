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
using System.Windows.Shapes;

namespace Dusza_WPF
{
    /// <summary>
    /// Interaction logic for RelocateApps.xaml
    /// </summary>
    public partial class RelocateApps : Window
    {
        public readonly ObservableCollection<SzamitogepConfig> _szamitogepConfigok = [];
        public readonly List<string> _szamitogepMappakElerese = [];
        public string _gyoker = "";
        string? destinationName = "";
        public RelocateApps(List<ProgramFolyamat> futoProgramok, string originalName, string gyoker)
        {
            InitializeComponent();
            //ProgramAthelyezese(futoProgramok, originalName);
            _gyoker = gyoker;
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            foreach (var item in _szamitogepConfigok.Where(x => x.Eleres.Split(@"\").Last() != originalName))
                cbValasztottGep.Items.Add(item.Eleres.Split(@"\").Last());

            futoProgramok.Select(x => x.FajlNeve).ToList().ForEach(x => lbTorlendoGepProgramok.Items.Add(x));


            btnAthelyez.Click += (s, e) =>
            {

                var valasz = MessageBox.Show("Biztos áthelyezi? Ez a folyamat végleg áthelyezi a programot és nem lehet visszacsinálni!", "Hűha", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (valasz == MessageBoxResult.Yes)
                {
                    if (lbTorlendoGepProgramok.SelectedIndex != -1 && cbValasztottGep.SelectedIndex != -1)
                    {
                        var athelyezendoProgram = lbTorlendoGepProgramok.SelectedItem.ToString();
                        var valasztottGep = cbValasztottGep.SelectedItem.ToString();
                        var p = futoProgramok.First(x => x.FajlNeve == athelyezendoProgram);

                        _szamitogepConfigok.First(x => x.Eleres.Split("\\").Last() == originalName).ProgramPeldanyAzonositok.Remove(athelyezendoProgram);
                        _szamitogepConfigok.First(x => x.Eleres.Split("\\").Last() == valasztottGep).ProgramPeldanyAzonositok.Add(athelyezendoProgram);

                        string originalPath = $"{_gyoker}\\{originalName}";
                        string newPath = $"{_gyoker}\\{destinationName}";

                        File.Copy($"{originalPath}\\{futoProgramok.First(x => x.IsActive == "AKTÍV" && x.FajlNeve == athelyezendoProgram).FajlNeve}", $"{newPath}\\{futoProgramok
                            .First(x => x.IsActive == "AKTÍV").FajlNeve}", true);
                        File.Delete($"{originalPath}\\{athelyezendoProgram}");
                        futoProgramok.Remove(p);
                    }
                    else
                        MessageBox.Show("Válassz ki egy gépet, és a programot amit áthyelyeznél.", "Hűha", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    return;

                lbTorlendoGepProgramok.Items.Clear();
                lbValasztottGepProgramok.Items.Clear();
                lbValasztottGepProgramok.Items.Clear();

                var gep = _szamitogepConfigok.First(x => x.Eleres.Split(@"\").Last() == cbValasztottGep.SelectedItem.ToString());
                gep.ProgramPeldanyAzonositok.ForEach(x => lbValasztottGepProgramok.Items.Add(x));
                futoProgramok.Select(x => x.FajlNeve).ToList().ForEach(x => lbTorlendoGepProgramok.Items.Add(x));

                if (futoProgramok.Count == 0)
                    btnBefejezes.IsEnabled = true;

            };

            btnBefejezes.Click += (s, e) =>
            {
                var valasz = MessageBox.Show("Ezzel a folyamattal a korábban kiálasztott gép törlésre kerül. Folytatja?", "Hűha", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (valasz == MessageBoxResult.Yes)
                {
                    string originalPath = $"{_gyoker}\\{originalName}";

                    File.Delete(_gyoker + $"\\{originalName}\\.szamitogep_config");
                    Directory.Delete(_gyoker + $"\\{originalName}");
                    MessageBox.Show("Sikeres törlése!");
                    Close();
                }

            };
        }

        public void SzamitogepMappakElerese()
        {
            _szamitogepMappakElerese.Clear();
            foreach (string eleres in Directory.GetDirectories(_gyoker).ToList())
            {
                _szamitogepMappakElerese.Add(eleres);
            }

        }
        public void SzamitogepConfigok()
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
                        if (!programok.Contains(".szamitogep_config") && !programok.Contains(".tarhely"))
                        {
                            gep.ProgramPeldanyAzonositok.Add(programok.Split("\\").Last());
                        }
                    }

                    _szamitogepConfigok.Add(gep);
                }
            }

        }

        private void cbValasztottGep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbValasztottGep.SelectedIndex != -1)
            {
                lbValasztottGepProgramok.Items.Clear();
                var gep = _szamitogepConfigok.First(x => x.Eleres.Split(@"\").Last() == cbValasztottGep.SelectedItem.ToString());
                gep.ProgramPeldanyAzonositok.ForEach(x => lbValasztottGepProgramok.Items.Add(x));
                destinationName = cbValasztottGep.SelectedItem.ToString();
            }
        }

        private void lbTorlendoGepProgramok_SelectionChanged(object sender, SelectionChangedEventArgs e) => lbValasztottGepProgramok.SelectedIndex = -1;

        private void lbValasztottGepProgramok_SelectionChanged(object sender, SelectionChangedEventArgs e) => lbTorlendoGepProgramok.SelectedIndex = -1;
    }
}
