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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dusza_WPF
{
    /// <summary>
    /// Interaction logic for AddComputer.xaml
    /// </summary>
    public partial class AddComputer : Page
    {
        private string _gyoker = "";
        public AddComputer(string gyoker)
        {
            InitializeComponent();
            _gyoker = gyoker;
            btnPcAdd.Click += (s, e) =>
            {
                string? pcName = tbPcName.Text.ToString();
                if (Regex.IsMatch(pcName.ToLower(), @"[a-z]") == false || Regex.IsMatch(pcName.ToLower(), @"á|é|ő|ű|ú|ó|ü|ö|í"))
                {
                    MessageBox.Show("Adja meg a számítógép nevét! (csak az angol ABC kis- és nagybetűit és számjegyeket tartalmazhat!)");
                    return;
                }
                int millimag = Convert.ToInt32(tbMillimag.Text.ToString());
                int memoria = Convert.ToInt32(tbMemoria.Text.ToString());

                Directory.CreateDirectory(_gyoker + $"/{pcName}");
                File.WriteAllLines(_gyoker + $"/{pcName}/.szamitogep_config", $"{millimag}\n{memoria}".Split("\n"));
                File.WriteAllLines($"{gyoker}/{pcName}/.tarhely", $"{millimag}\n{memoria}".Split('\n'));
                //szamitogepConfigok.Add(new SzamitogepConfig(millimag,memoria,$"{gyoker}\\{pcName}"));

                MessageBox.Show("Sikeres hozzáadás!", "Yay",MessageBoxButton.OK, MessageBoxImage.Information);
                tbMemoria.Text = "";
                tbMillimag.Text = "";
                tbPcName.Text = "";

            };

            tbPcName.TextChanged += (s, e) =>
            {
                if (s is TextBox mezo && mezo.Text != "")
                    btnPcAdd.IsEnabled = true;
                else
                    btnPcAdd.IsEnabled = false;
            };
        }
    }
}
