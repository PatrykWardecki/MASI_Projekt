using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Masi_Projekt;

public partial class InputWindow : Window
{
    public InputWindow()
    {
        InitializeComponent();
    }

    private void Confirm_Click(object sender, RoutedEventArgs e)
    {
        string val1 = InputBox1.Text.Trim();
        string val2 = InputBox2.Text.Trim();

        if (val1 == "Wpisz wartość 1...") val1 = "";
        if (val2 == "Wpisz wartość 2...") val2 = "";

        MessageBox.Show($"Dane zatwierdzone:\n\nWartość 1: {val1}\nWartość 2: {val2}", "Potwierdzenie", MessageBoxButton.OK, MessageBoxImage.Information);
        this.Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox tb)
        {
            if (tb == InputBox1 && tb.Text == "Wpisz wartość 1..." ||
                tb == InputBox2 && tb.Text == "Wpisz wartość 2...")
            {
                tb.Text = "";
                tb.Foreground = Brushes.Black;
            }
        }
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox tb)
        {
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb == InputBox1)
                {
                    tb.Text = "Wpisz wartość 1...";
                }
                else if (tb == InputBox2)
                {
                    tb.Text = "Wpisz wartość 2...";
                }
                tb.Foreground = Brushes.Gray;
            }
        }
    }
}