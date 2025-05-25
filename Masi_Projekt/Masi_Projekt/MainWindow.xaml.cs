using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
        var inputWindow = new InputWindow();
        inputWindow.ShowDialog();
    }
    private void SwitchTheme_Click(object sender, RoutedEventArgs e)
    {
        App.ThemeManager.SetTheme("Dark"); // albo "Light"
    }
}