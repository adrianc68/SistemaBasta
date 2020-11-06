using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Basta.GUI.Login.Main
{
    /// <summary>
    /// Lógica de interacción para MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayButtonClicked(object sender, RoutedEventArgs e)
        {
            selectStackPanel.Visibility = Visibility.Visible;
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            selectStackPanel.Visibility = Visibility.Hidden;
        }

        private void JoinToRoomButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void SaveSettingsButtonCllicked(object sender, RoutedEventArgs e)
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }

        private void CancelSettingsButtonClicked(object sender, RoutedEventArgs e)
        {
            settingsStackPanel.Visibility = Visibility.Hidden;
        }

        private void SettingsButtonClicked(object sender, RoutedEventArgs e)
        {
            settingsStackPanel.Visibility = Visibility.Visible;
        }

        private void LanguageComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Inglés");
            data.Add("Español");
            languageComboBox = sender as ComboBox;
            languageComboBox.ItemsSource = data;
            Console.WriteLine("CurrentUICulture is {0}.", CultureInfo.CurrentUICulture.Name);
            _ = CultureInfo.CurrentUICulture.Name == "en-US" ? languageComboBox.SelectedIndex = 0 : languageComboBox.SelectedIndex = 1;
        }
    }
}
