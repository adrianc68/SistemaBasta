using Basta.GUI.Login.Lobby;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Basta.GUI.Login.Main
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
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
            LobbyWindow lobbyWindow = new LobbyWindow();
            lobbyWindow.ShowDialog();
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

        private void CancelCreateRoomButtonClicked(object sender, RoutedEventArgs e)
        {
            createRoomStackPanel.Visibility = Visibility.Hidden;
        }

        private void CreateConfiguredRoomButtonClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("CREAR SALA CONFIGURADA");
            // SALA PROPIA
        }

        private void GameLimitComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            List<int> data = new List<int>();
            data.Add(30);
            data.Add(45);
            data.Add(60);
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void PlayerLimitComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            List<int> data = new List<int>();
            data.Add(2);
            data.Add(3);
            data.Add(5);
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void RoundLimitComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            List<int> data = new List<int>();
            data.Add(3);
            data.Add(5);
            data.Add(7);
            data.Add(10);
            data.Add(15);
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }


        private void ConfigureRoomButtonClicked(object sender, RoutedEventArgs e)
        {
            createRoomStackPanel.Visibility = Visibility.Visible;
        }

        private void exitButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }

}
