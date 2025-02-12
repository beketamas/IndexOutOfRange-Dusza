using DuszaProg_IndexOutOfRange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Properties.xaml
    /// </summary>
    
    public partial class Properties : Window
    {
        readonly ObservableCollection<ProgramFolyamat> programok = [];
        public Properties(string nev, string eleres, int millimag, int memoria, Dictionary<ProgramFolyamat, string> programok)
        {
            InitializeComponent();
            lbGepNEv.Content = nev;
            lbGepEleres.Items.Add(eleres);
            lbGepMillimag.Content = millimag;
            lbGepMemoria.Content = memoria;

            foreach (var item in programok.Where(x => x.Value.Contains(nev)).ToDictionary())
                this.programok.Add(item.Key);

            cbProgramok.ItemsSource = this.programok.Select(x => $"{x.FajlNeve}");
            //cbProgramok.SelectedIndex = 1;



        }
    }
}
