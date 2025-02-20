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
    /// Interaction logic for DeleteComputer.xaml
    /// </summary>
    public partial class DeleteComputer : Page
    {
        public string _gyoker = "";
        public List<string> _gépNevek = [];
        public static readonly ObservableCollection<SzamitogepConfig> _szamitogepConfigok = [];
        public static readonly List<string> _szamitogepMappakElerese = [];
        public const int EGYFOLYAMAT = 4;
        public static readonly Dictionary<ProgramFolyamat, string> _szamitogepekenFutoAlkalmazasok = [];
        public static readonly List<Kluszter> _klaszterLista = [];
        public DeleteComputer(string gyoker)
        {
            InitializeComponent();
            _gyoker = gyoker;
            KluszterCucc();
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();
            btnDeleteComputer.Click += (s, e) => SzamitogepTorlese();
            
        }

        public void SzamitogepMappakElerese()
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
        public void SzamitogepConfigok()
        {
            lbComputers.Items.Clear();
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
            _szamitogepConfigok.Select(x => x.Eleres.Split(@"\").Last()).ToList().ForEach(y =>
            {
                string? valasztottGep = y;
                var gep = _szamitogepConfigok.First(x => x.Eleres.Split(@"\").Last() == valasztottGep);
                var sumMemoria = 0;
                gep.ProgramPeldanyAzonositok.Select(x => x.Split("-")[0].ToString()).ToList().ForEach(x => sumMemoria += _klaszterLista.First(y => y.ProgramName == x).Memoria);

                var sumMillimag = 0;
                gep.ProgramPeldanyAzonositok.Select(x => x.Split("-")[0].ToString()).ToList().ForEach(x => sumMillimag += _klaszterLista.First(y => y.ProgramName == x).Millimag);
                //lbComputers.Items.Add($"{y} | Tárhely: {sumMemoria}MB/{gep.Memoria + sumMemoria}MB | Millimag: {sumMillimag}/{gep.Millimag + sumMillimag}");
                if (sumMemoria > 0)
                {
                    StackPanel panel = new StackPanel { Orientation = Orientation.Horizontal };

                    TextBlock textBlock = new TextBlock
                    {
                        Text = $"{y} | Tárhely: {sumMemoria}MB/{gep.Memoria+sumMemoria}MB | Millimag: {sumMillimag}/{gep.Millimag+sumMillimag}",
                        Margin = new Thickness(0, 0, 10, 0) // Add some space between text and image
                    };

                    Image image = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Dusza_WPF;component/Images/warning-sign.png")), 
                        Width = 16,
                        Height = 16
                    };

                    panel.Children.Add(textBlock);
                    panel.Children.Add(image);

                    lbComputers.Items.Add(panel);
                }
                else
                    lbComputers.Items.Add($"{y} | Tárhely: {sumMemoria}MB/{gep.Memoria}MB | Millimag: {sumMillimag}/{gep.Millimag}");

            });

        }

        public void KluszterCucc()
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

        public void SzamitogepTorlese()
        {
            //szamitogepConfigok.ForEach(s => Console.Write($"\n\t{s.Eleres.Split("\\").Last()}"));

            string? mappaNev = "";
            if (lbComputers.SelectedItem is StackPanel panel)
            {
                if (panel.Children[0] is TextBlock box)
                {
                    mappaNev =  box.Text.ToString().Split(" | ").First(); 
                }
                
            }
            else
                mappaNev = lbComputers.SelectedItem.ToString().Split(" | ").First();
            try
            {
                string[] files = Directory.GetFiles(_gyoker + $"\\hasznalatbanLevoGepek\\{mappaNev}").ToList().Select(x => x.Split("\\").Last()).ToArray();
                var futoProgramok = _szamitogepekenFutoAlkalmazasok.Where(x => x.Value == _gyoker + $"\\hasznalatbanLevoGepek\\{mappaNev}").Select(x => x.Key).ToList();

                if (futoProgramok.Any(x => x.IsActive == "AKTÍV"))
                {
                    RelocateApps relocateApps = new RelocateApps(futoProgramok, mappaNev, _gyoker);
                    relocateApps.ShowDialog();
                }

                else
                {
                    File.Delete(_gyoker + $"\\hasznalatbanLevoGepek\\{mappaNev}\\.szamitogep_config");
                    File.Delete(_gyoker + $"\\hasznalatbanLevoGepek\\{mappaNev}\\.pozicio");
                    File.Delete(_gyoker + $"\\hasznalatbanLevoGepek\\{mappaNev}\\.tarhely");
                    Directory.Delete(_gyoker + $"\\hasznalatbanLevoGepek\\{mappaNev}");
                    MessageBox.Show("Sikeres törlés!", ":D", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return;
            }
            SzamitogepMappakElerese();
            SzamitogepConfigok();
            ProgramokBeolvasása();

        }
        public void ProgramokBeolvasása()
        {
            _szamitogepekenFutoAlkalmazasok.Clear();
            foreach (var item in _szamitogepMappakElerese)
            {
                List<string> fajlokNevei = Directory.GetFiles(item).ToList();
                foreach (var fajl in fajlokNevei)
                {
                    if (!fajl.Contains(".szamitogep_config") && !fajl.Contains(".tarhely") && !fajl.Contains(".pozicio"))
                    {
                        string[] fajlElemek = File.ReadAllLines(fajl);
                        _szamitogepekenFutoAlkalmazasok.Add(new ProgramFolyamat(Convert.ToDateTime(fajlElemek[0]), fajlElemek[1], Convert.ToInt32(fajlElemek[2]), Convert.ToInt32(fajlElemek[3]), fajl.Split(@"\").Last()), item);
                    }
                }
            }
            KluszterCucc();

        }
    }
}
