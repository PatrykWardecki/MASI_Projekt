using System.Configuration;
using System.Data;
using System.Windows;

namespace Masi_Projekt;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static class ThemeManager
    {
        public static void SetTheme(string theme)
        {
            var dict = new ResourceDictionary();
            switch (theme)
            {
                case "Dark":
                    dict.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
                    break;
            }

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}