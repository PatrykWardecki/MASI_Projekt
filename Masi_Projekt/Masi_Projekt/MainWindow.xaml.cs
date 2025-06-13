using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Masi_Projekt;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void InputButton_Click(object sender, RoutedEventArgs e)
    {
        var inputWindow = new PionoweSekwencjonowanie();
        if (inputWindow.ShowDialog() == true)
        {
            AddVerticalOperation(inputWindow.Value1, inputWindow.Value2);
        }
    }

    private void EliminacjaButton_Click(object sender, RoutedEventArgs e)
    {
        var inputWindow = new PoziomaEliminacja();
        if (inputWindow.ShowDialog() == true)
        {
            AddHorizontalOperation(inputWindow.Value1, inputWindow.Value2, inputWindow.Value3);
        }
    }
    private void SwitchTheme_Click(object sender, RoutedEventArgs e)
    {
        ThemeManager.ToggleTheme();
    }
    private void AddVerticalOperation(string a, string b)
    {
        // Kontener
        Canvas canvas = new Canvas
        {
            Width = 80,
            Height = 140,
            Margin = new Thickness(10)
        };

        // Półkole pionowe (otwarte z lewej)
        Path path = new Path
        {
            Stroke = Brushes.Green,
            StrokeThickness = 4,
            Fill = Brushes.Transparent
        };

        PathGeometry geometry = new PathGeometry();
        PathFigure figure = new PathFigure
        {
            StartPoint = new Point(10, 0)  // Początek łuku (góra)
        };

        figure.Segments.Add(new ArcSegment
        {
            Point = new Point(10, 160),            // Koniec łuku (dół)
            Size = new Size(60, 60),              // Promień łuku
            SweepDirection = SweepDirection.Counterclockwise, // Otwarty z lewej
            IsLargeArc = false
        });

        geometry.Figures.Add(figure);
        path.Data = geometry;

        // Tekst (wewnątrz półkola)
        TextBlock text = new TextBlock
        {
            Text = $"{a}\n;\n{b}",
            FontSize = 25,
            Foreground = Brushes.Black,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
        };

        // Umieszczenie tekstu w środku łuku
        Canvas.SetLeft(text, -5);  // Przesuń trochę w lewo
        Canvas.SetTop(text, 30);   // Wyśrodkuj w pionie

        // Dodanie elementów
        canvas.Children.Add(path);
        canvas.Children.Add(text);

        ResultsPanel.Children.Add(canvas);
    }

    private void AddHorizontalOperation(string a, string b, string c)
    {
        StackPanel container = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(10)
        };

        Canvas canvas = new Canvas
        {
            Width = 200,
            Height = 20
        };

        // Główna pozioma linia
        Line mainLine = new Line
        {
            X1 = 20,
            Y1 = 10,
            X2 = 180,
            Y2 = 10,
            Stroke = Brushes.Red,
            StrokeThickness = 4
        };

        // Pionowa kreska na początku (lewa)
        Line startCap = new Line
        {
            X1 = 20,
            Y1 = 1,
            X2 = 20,
            Y2 = 20,
            Stroke = Brushes.Red,
            StrokeThickness = 4
        };

        // Pionowa kreska na końcu (prawa)
        Line endCap = new Line
        {
            X1 = 180,
            Y1 = 1,
            X2 = 180,
            Y2 = 20,
            Stroke = Brushes.Red,
            StrokeThickness = 4
        };

        // Dodanie wszystkich linii do canvas
        canvas.Children.Add(mainLine);
        canvas.Children.Add(startCap);
        canvas.Children.Add(endCap);

        TextBlock textBlock = new TextBlock
        {
            Text = $"{a} ; {b} ; {c}",
            FontSize = 25,
            Foreground = Brushes.Black,
            FontWeight = FontWeights.Bold,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        container.Children.Add(canvas);
        container.Children.Add(textBlock);

        ResultsPanel.Children.Add(container);
    }


}