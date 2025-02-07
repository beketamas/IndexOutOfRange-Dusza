using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Button> gepek = [];
        public MainWindow()
        {
            InitializeComponent();

            GenerateButtons();
            lvGepek.ItemsSource = gepek;
        }

        private void GenerateButtons()
        {
            for (int i = 1; i <= 5; i++)
            {
                Button btn = new Button
                {
                    Content = $"Button {i}",
                    Width = 100,
                    Height = 30,
                    Margin = new Thickness(5),
                    Tag = i
                };

                btn.PreviewMouseMove += Button_PreviewMouseMove;
                gepek.Add(btn);
            }
        }

        private void Button_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is Button button)
                DragDrop.DoDragDrop(button, button, DragDropEffects.Move);
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Button)) is Button button)
            {
                if (gepek.Contains(button))
                    gepek.Remove(button);
                    lvGepek.ItemsSource = null;
                    //lvGepek.ItemsSource=gepek;


                if (!cKlaszter.Children.Contains(button))
                    cKlaszter.Children.Add(button);

                Point dropPosition = e.GetPosition(cKlaszter);
                Canvas.SetLeft(button, dropPosition.X);
                Canvas.SetTop(button, dropPosition.Y);

                button.PreviewMouseMove -= Button_PreviewMouseMove;
                button.PreviewMouseMove += CanvasButton_PreviewMouseMove;
            }
        }

        private void CanvasButton_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is Button button)
                DragDrop.DoDragDrop(button, button, DragDropEffects.Move);
        }

        private void ListView_DragOver(object sender, DragEventArgs e) => e.Effects = DragDropEffects.Move;

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Button)) is Button button)
            {
                if (!lvGepek.Items.Contains(button))
                    gepek.Add(button);

                cKlaszter.Children.Remove(button); // Remove from Canvas
                button.PreviewMouseMove -= CanvasButton_PreviewMouseMove;
                button.PreviewMouseMove += Button_PreviewMouseMove; // Restore original drag event
            }
        }
    }
}