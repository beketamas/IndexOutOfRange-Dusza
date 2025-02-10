using DuszaProg_IndexOutOfRange;
using System.Collections.ObjectModel;
using System.Reflection;
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
using System.Windows.Threading;

namespace Dusza_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<SzamitogepConfig> gepek = [];
        readonly List<SzamitogepConfig> klaszter = [];
        readonly Dictionary<SzamitogepConfig, Line> lines = [];
        private Point _initialMousePosition;
        public MainWindow()
        {
            InitializeComponent();
            GenerateButtons();
            lvGepek.ItemsSource = gepek;
        }

        private void GenerateButtons()
        {
            var gep1 = new SzamitogepConfig(5000, 6000, "szamitogep1");
            gep1.PreviewMouseMove += Button_PreviewMouseMove;
            gep1.PreviewMouseDown += Button_PreviewMouseDown; 
            
            var gep2 = new SzamitogepConfig(5000, 6000, "szamitogep2");
            gep2.PreviewMouseMove += Button_PreviewMouseMove;
            gep2.PreviewMouseDown += Button_PreviewMouseDown;

            gepek.Add(gep1);
            gepek.Add(gep2);
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is SzamitogepConfig button)
                _initialMousePosition = e.GetPosition(button);
        }

        private void Button_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is SzamitogepConfig gep && e.LeftButton == MouseButtonState.Pressed)
            {
                var currentPosition = e.GetPosition(gep);
                var distanceMoved = (_initialMousePosition - currentPosition).Length;

                if (distanceMoved > 5)
                    DragDrop.DoDragDrop(gep, gep, DragDropEffects.Move);
            }
        }

        private void CanvasButton_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is SzamitogepConfig button)
            {
                var currentPosition = e.GetPosition(button);
                var distanceMoved = (_initialMousePosition - currentPosition).Length;

                if (distanceMoved > 5)
                    DragDrop.DoDragDrop(button, button, DragDropEffects.Move);
            }
        }

        private void ListView_DragOver(object sender, DragEventArgs e) => e.Effects = DragDropEffects.Move;

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(SzamitogepConfig)) is SzamitogepConfig gep)
            {
                if (lines.Any(x => x.Key == gep))
                {
                    cKlaszter.Children.Remove(lines.Where(x => x.Key == gep).First().Value);                    
                    lines.Remove(gep);
                }

                gepek.Remove(gep);
                if (VisualTreeHelper.GetParent(gep) is ContentPresenter currentParent)
                    currentParent.Content = null;

                Point dropPosition = e.GetPosition(sender as Canvas);
                Canvas.SetLeft(gep, dropPosition.X);
                Canvas.SetTop(gep, dropPosition.Y);

                if (sender is Canvas klaszter && !klaszter.Children.Contains(gep))
                {
                    klaszter.Children.Add(gep);
                    this.klaszter.Add(gep);
                }

                gep.PreviewMouseMove -= Button_PreviewMouseMove;
                gep.PreviewMouseMove += CanvasButton_PreviewMouseMove;
                CreateLine(gep);
                
            }
        }

        private void CreateLine(SzamitogepConfig gep)
        {
            Line dynamicLine = new()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            dynamicLine.SetBinding(Line.X1Property, new Binding
            {
                Source = gep,
                Path = new PropertyPath("(Canvas.Left)"),
            });

            dynamicLine.SetBinding(Line.Y1Property, new Binding
            {
                Source = gep,
                Path = new PropertyPath("(Canvas.Top)")
            });

            dynamicLine.SetBinding(Line.X2Property, new Binding
            {
                Source = btnCenter,
                Path = new PropertyPath("(Canvas.Left)")
            });

            dynamicLine.SetBinding(Line.Y2Property, new Binding
            {
                Source = btnCenter,
                Path = new PropertyPath("(Canvas.Top)")
            });
            cKlaszter.Children.Add(dynamicLine);

            dynamicLine.Loaded += (sender, e) =>
            {
                double gepCenterX = Canvas.GetLeft(gep) + gep.ActualWidth / 2;
                double gepCenterY = Canvas.GetTop(gep) + gep.ActualHeight / 2;

                double btnCenterX = Canvas.GetLeft(btnCenter) + btnCenter.ActualWidth / 2;
                double btnCenterY = Canvas.GetTop(btnCenter) + btnCenter.ActualHeight / 2;

                dynamicLine.X1 = gepCenterX;
                dynamicLine.Y1 = gepCenterY;
                dynamicLine.X2 = btnCenterX;
                dynamicLine.Y2 = btnCenterY;
            };
            lines.Add(gep, dynamicLine);
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(SzamitogepConfig)) is SzamitogepConfig gep && !gepek.Contains(gep))
            {
                if (lines.Any(x => x.Key == gep))
                {
                    cKlaszter.Children.Remove(lines.Where(x => x.Key == gep).First().Value);
                    lines.Remove(gep);
                }

                gepek.Add(gep);
                cKlaszter.Children.Remove(gep);

                gep.PreviewMouseMove -= CanvasButton_PreviewMouseMove;
                gep.PreviewMouseMove += Button_PreviewMouseMove;
            }
        }

    }
}