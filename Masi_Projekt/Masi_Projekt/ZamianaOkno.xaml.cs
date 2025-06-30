using System.Windows;
using System.Windows.Controls;

namespace Masi_Projekt;

public partial class ZamianaOkno : Window
{
    public string SelectedOption { get; private set; }


    public ZamianaOkno()
    {
        InitializeComponent();
    }

    private void Confirm_Click(object sender, RoutedEventArgs e)
    {
        var item = ComboBoxOpcje.SelectedItem as ComboBoxItem;
        if (item != null)
        {
            SelectedOption = item.Content.ToString();
            DialogResult = true;
            Close();
        }
        else
        {
            MessageBox.Show("Wybierz wartość do zamiany.");
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}