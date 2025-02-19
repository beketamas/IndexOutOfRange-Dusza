using DuszaProg_IndexOutOfRange;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dusza_WPF
{
    /// <summary>
    /// Interaction logic for AddApplication.xaml
    /// </summary>
    public partial class AddApplication : Window
    {
        private static string _gyoker = "";
        public const int EGYFOLYAMAT = 4;
        public static readonly List<Kluszter> _klaszterLista = [];
        public AddApplication(string gyoker)
        {
            InitializeComponent();
            _gyoker = gyoker;
            KluszterCucc();
            
            btnLetrehoz.Click += (s, e) =>
            {
                if (tbMemoria.Text != "" && tbMennyiAktiv.Text != "" && tbMillimag.Text != "" && tbNév.Text != "")
                {
                    var tomb = $"{tbNév.Text};{tbMennyiAktiv.Text};{tbMillimag.Text};{tbMemoria.Text}";
                    _klaszterLista.Add(new Kluszter(tomb.Split(";")));
                    File.WriteAllLines(_gyoker + "/.klaszter", _klaszterLista.Select(x => x.KiIratas()).ToArray());
                    MessageBox.Show("Sikeres Létrehozás!", ":D", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                    StartApplication.letrehozottProgramok.Add(tbNév.Text, int.Parse(tbMennyiAktiv.Text));
                }
            };
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
        }
        private void TextInput(object sender, TextCompositionEventArgs e) => e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);

    }
}
