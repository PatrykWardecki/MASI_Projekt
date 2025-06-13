using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Masi_Projekt;

public partial class PionoweSekwencjonowanie : Window
{
    public string Value1 { get; private set; }
    public string Value2 { get; private set; }
    public PionoweSekwencjonowanie()
    {
       
        InitializeComponent();
    }

    private void Confirm_Click(object sender, RoutedEventArgs e)
    {
        Value1 = InputBox1.Text.Trim() == "Wpisz wartość 1..." ? "" : InputBox1.Text.Trim();
        Value2 = InputBox2.Text.Trim() == "Wpisz wartość 2..." ? "" : InputBox2.Text.Trim();
        DialogResult = true;
        Close();
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