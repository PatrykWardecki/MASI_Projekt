using System;
using System.Windows;

namespace Masi_Projekt
{
    public static class ThemeManager
    {
        private static string _currentTheme = "Light";

        public static void SetTheme(string themeName)
        {
            var uri = new Uri($"Themes/{themeName}Theme.xaml", UriKind.Relative);

            var newTheme = new ResourceDictionary { Source = uri };

            var appResources = Application.Current.Resources;
            if (appResources.MergedDictionaries.Count > 0)
                appResources.MergedDictionaries.Clear();

            appResources.MergedDictionaries.Add(newTheme);
            _currentTheme = themeName;
        }

        public static void ToggleTheme()
        {
            SetTheme(_currentTheme == "Light" ? "Dark" : "Light");
        }
    }
}