using System.Windows;

namespace Masi_Projekt;

public partial class InputWindow : Window
{
    public InputWindow()
    {
        InitializeComponent();
    }

    private void Confirm_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Dane zatwierdzone!");
        this.Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}