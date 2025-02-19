using System;
using System.Collections.Generic;
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
    /// Interaction logic for EleresMegadas.xaml
    /// </summary>
    public partial class EleresMegadas : Page
    {
        public event EventHandler<string>? OnPageCompleted;
        private readonly TaskCompletionSource<string> _completionSource = new();

        public EleresMegadas()
        {
            InitializeComponent();

            btnTovabb.Click += (s, e) =>
            {
                if (s is Button tovabb && tbEleres.Text != null && tbEleres.Text != "")
                {
                    try
                    {
                        Directory.GetFiles(tbEleres.Text);
                    }
                    catch (Exception n)
                    {

                        MessageBox.Show(n.Message);
                        return;
                    }


                    OnPageCompleted?.Invoke(this, tbEleres.Text);
                    _completionSource.SetResult(tbEleres.Text);
                    tbEleres.Text = "";
                }
                else
                    MessageBox.Show("Adjon meg a klaszter elérést!");
            };
        }

        public Task<string> GetResultAsync() => _completionSource.Task;
    }
}
