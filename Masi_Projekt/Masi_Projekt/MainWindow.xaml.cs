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
    private Canvas _lastSequencingCanvas;
    private string _lastSequencingValue1;
    private string _lastSequencingValue2;
    private string[] _lastEliminationValues;
    
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void InputButton_Click(object sender, RoutedEventArgs e)
    {
        var inputWindow = new PionoweSekwencjonowanie();
        if (inputWindow.ShowDialog() == true)
        {
            _lastSequencingValue1 = inputWindow.Value1;
            _lastSequencingValue2 = inputWindow.Value2;
            AddVerticalOperation(inputWindow.Value1, inputWindow.Value2);
        }
    }

    private void EliminacjaButton_Click(object sender, RoutedEventArgs e)
    {
        var inputWindow = new PoziomaEliminacja();
        if (inputWindow.ShowDialog() == true)
        {
            // Zapisz wartości eliminacji
            _lastEliminationValues = new string[] { inputWindow.Value1, inputWindow.Value2, inputWindow.Value3 };
            AddHorizontalOperation(inputWindow.Value1, inputWindow.Value2, inputWindow.Value3);
        }
    }
    
    private void SwitchTheme_Click(object sender, RoutedEventArgs e)
    {
        ThemeManager.ToggleTheme();
    }
    
    private void AddVerticalOperation(string a, string b)
    {
        VerticalContainer.Child = null;

        Canvas canvas = new Canvas
        {
            Width = 80,
            Height = 140,
            Margin = new Thickness(10)
        };

        Path path = new Path
        {
            Stroke = Brushes.Green,
            StrokeThickness = 4,
            Fill = Brushes.Transparent
        };

        PathGeometry geometry = new PathGeometry();
        PathFigure figure = new PathFigure { StartPoint = new Point(10, 0) };
        figure.Segments.Add(new ArcSegment
        {
            Point = new Point(10, 160),
            Size = new Size(60, 60),
            SweepDirection = SweepDirection.Counterclockwise,
            IsLargeArc = false
        });
        geometry.Figures.Add(figure);
        path.Data = geometry;

        TextBlock text = new TextBlock
        {
            Text = $"{a}\n;\n{b}",
            FontSize = 25,
            Foreground = Brushes.Black,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center
        };

        Canvas.SetLeft(text, -5);
        Canvas.SetTop(text, 30);

        canvas.Children.Add(path);
        canvas.Children.Add(text);

        VerticalContainer.Child = canvas;

        // Zapisz ostatnie sekwencjonowanie
        _lastSequencingCanvas = canvas;
    }

    private void AddHorizontalOperation(string a, string b, string c)
    {
        HorizontalContainer.Child = null;

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

        Line mainLine = new Line
        {
            X1 = 20,
            Y1 = 10,
            X2 = 180,
            Y2 = 10,
            Stroke = Brushes.Red,
            StrokeThickness = 4
        };

        Line startCap = new Line
        {
            X1 = 20,
            Y1 = 1,
            X2 = 20,
            Y2 = 20,
            Stroke = Brushes.Red,
            StrokeThickness = 4
        };

        Line endCap = new Line
        {
            X1 = 180,
            Y1 = 1,
            X2 = 180,
            Y2 = 20,
            Stroke = Brushes.Red,
            StrokeThickness = 4
        };

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

        HorizontalContainer.Child = container;
    }
    
    private void ZamianaButton_Click(object sender, RoutedEventArgs e)
    {
        if (_lastSequencingCanvas == null || HorizontalContainer.Child == null || _lastEliminationValues == null)
        {
            MessageBox.Show("Najpierw dodaj operację sekwencjonowania i eliminacji.");
            return;
        }

        var oknoZamiany = new ZamianaOkno();
        if (oknoZamiany.ShowDialog() == true)
        {
            string selected = oknoZamiany.SelectedOption;
            var eliminacja = HorizontalContainer.Child as StackPanel;
            if (eliminacja != null)
            {
                // Użyj zapisanych wartości eliminacji zamiast parsowania tekstu
                string[] originalValues = _lastEliminationValues;
                
                // Usuń stary tekst
                if (eliminacja.Children.Count > 1)
                {
                    eliminacja.Children.RemoveAt(1);
                }

                // Stwórz nowy kontener z operacją sekwencjonowania zastępującą wybraną wartość
                StackPanel newContainer = CreateReplacementContainer(selected, originalValues);
                
                // Dodaj nowy kontener
                eliminacja.Children.Add(newContainer);
            }
        }
    }

    private StackPanel CreateReplacementContainer(string selectedOption, string[] originalValues)
    {
        StackPanel container = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(10)
        };

        // Określ, która pozycja ma być zastąpiona
        int replaceIndex = selectedOption == "1" ? 0 :
                          selectedOption == "2" ? 1 :
                          selectedOption == "3" ? 2 : -1;

        for (int i = 0; i < originalValues.Length; i++)
        {
            if (i == replaceIndex)
            {
                // Wstaw operację sekwencjonowania
                Border sequencingBorder = CreateSmallSequencingElement();
                container.Children.Add(sequencingBorder);
            }
            else
            {
                // Wstaw oryginalną wartość
                TextBlock valueText = new TextBlock
                {
                    Text = originalValues[i],
                    FontSize = 20,
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5)
                };
                container.Children.Add(valueText);
            }

            // Dodaj separator (;) między elementami, ale nie po ostatnim
            if (i < originalValues.Length - 1)
            {
                TextBlock separator = new TextBlock
                {
                    Text = " ; ",
                    FontSize = 20,
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(2)
                };
                container.Children.Add(separator);
            }
        }

        return container;
    }

    private Border CreateSmallSequencingElement()
    {
        // Użyj Border jako kontenera dla lepszego kontrolowania wymiarów
        Border border = new Border
        {
            Width = 70,
            Height = 60,
            Margin = new Thickness(5),
            VerticalAlignment = VerticalAlignment.Center
        };

        Canvas canvas = new Canvas
        {
            Width = 70,
            Height = 60
        };

        // Zmniejszony półokrąg
        Path path = new Path
        {
            Stroke = Brushes.Green,
            StrokeThickness = 2,
            Fill = Brushes.Transparent
        };

        PathGeometry geometry = new PathGeometry();
        PathFigure figure = new PathFigure { StartPoint = new Point(10, 5) };
        figure.Segments.Add(new ArcSegment
        {
            Point = new Point(10, 55),
            Size = new Size(25, 25), // Znacznie zmniejszony rozmiar łuku
            SweepDirection = SweepDirection.Counterclockwise,
            IsLargeArc = false
        });
        geometry.Figures.Add(figure);
        path.Data = geometry;

        // Tekst wewnątrz półokręgu
        TextBlock text = new TextBlock
        {
            Text = $"{_lastSequencingValue1}\n;\n{_lastSequencingValue2}",
            FontSize = 10, // Bardzo mała czcionka
            Foreground = Brushes.Black,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            LineHeight = 10 // Zmniejszona wysokość linii
        };

        // Lepsze pozycjonowanie tekstu w środku półokręgu
        Canvas.SetLeft(text, 15);
        Canvas.SetTop(text, 15);

        canvas.Children.Add(path);
        canvas.Children.Add(text);
        
        border.Child = canvas;
        return border;
    }

    private Canvas CloneCanvas(Canvas original)
    {
        Canvas clone = new Canvas
        {
            Width = original.Width,
            Height = original.Height,
            Margin = original.Margin
        };

        foreach (UIElement child in original.Children)
        {
            if (child is Path path)
            {
                Path newPath = new Path
                {
                    Stroke = path.Stroke,
                    StrokeThickness = path.StrokeThickness,
                    Fill = path.Fill,
                    Data = path.Data.Clone()
                };
                clone.Children.Add(newPath);
            }
            else if (child is TextBlock textBlock)
            {
                TextBlock newText = new TextBlock
                {
                    Text = textBlock.Text,
                    FontSize = textBlock.FontSize,
                    Foreground = textBlock.Foreground,
                    FontWeight = textBlock.FontWeight,
                    TextAlignment = textBlock.TextAlignment
                };

                Canvas.SetLeft(newText, Canvas.GetLeft(textBlock));
                Canvas.SetTop(newText, Canvas.GetTop(textBlock));

                clone.Children.Add(newText);
            }
        }

        return clone;
    }
}