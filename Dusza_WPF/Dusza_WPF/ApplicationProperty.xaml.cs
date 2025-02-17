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
    /// Interaction logic for ApplicationProperty.xaml
    /// </summary>
    public partial class ApplicationProperty : Window
    {
        public const int EGYFOLYAMAT = 4;
        private string _gyoker;
        public static readonly List<Kluszter> _klaszterLista = [];

        public ApplicationProperty(Kluszter program, string gyoker)
        {
            InitializeComponent();
            _gyoker = gyoker;
            KluszterCucc();
            tbMemoria.Text = program.Memoria.ToString();
            tbMillimag.Text = program.Millimag.ToString();
            tbMennyiAktiv.Text = program.MennyiActive.ToString();

            btnMentes.Click += (s, e) =>
            {

                var valasz = MessageBox.Show("Bizots menti?", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (valasz == MessageBoxResult.Yes)
                {
                    _klaszterLista.Where(x => x.ProgramName == program.ProgramName).First().MennyiActive = int.Parse(tbMennyiAktiv.Text.ToString());
                    _klaszterLista.Where(x => x.ProgramName == program.ProgramName).First().Millimag = int.Parse(tbMillimag.Text.ToString());
                    _klaszterLista.Where(x => x.ProgramName == program.ProgramName).First().Memoria = int.Parse(tbMemoria.Text.ToString());
                    File.WriteAllLines(_gyoker + "/.klaszter", _klaszterLista.Select(x => x.KiIratas()).ToArray());
                    MessageBox.Show("Sikeres Mentés!", ":D", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else return;

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
