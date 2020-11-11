using Basta.GUI.Login.Lobby;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Basta.Properties;
using Domain.Domain;
using Domain.Domain.gameConfiguration;

namespace Basta.GUI.Login.Main {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void PlayButtonClicked( object sender, RoutedEventArgs e ) {
            selectStackPanel.Visibility = Visibility.Visible;
        }

        private void JoinToRoomButtonClicked( object sender, RoutedEventArgs e ) {

        }

        private void CreateConfiguredRoomButtonClicked( object sender, RoutedEventArgs e ) {
            //Room room = new Room();
            //RoomConfiguration roomConfiguration = new RoomConfiguration();
            //roomConfiguration.Playerslimit = (int) personLimitComboBox.SelectedItem;
            //roomConfiguration.RoomState = RoomState.PUBLIC;

            //Game game = new Game();
            //game.Players = new List<Player>( (int) personLimitComboBox.SelectedItem );
            //game.Players.Add( Autentication.GetInstance().GetPlayer() );

            //GameConfiguration gameConfiguration = new GameConfiguration();
            //gameConfiguration.Rounds = new List<Round>( (int) roundLimitComboBox.SelectedItem );
            //gameConfiguration.Timer = (double) gameTimeComboBox.SelectedItem;

            //game.GameConfiguration = gameConfiguration;
            //room.Game = game;

            //Console.WriteLine( room.ToString() );
            LobbyWindow lobbyWindow = new LobbyWindow();
            lobbyWindow.ShowDialog();
            // SALA PROPIA
        }

        private void CancelCreateRoomButtonClicked( object sender, RoutedEventArgs e ) {
            createRoomStackPanel.Visibility = Visibility.Hidden;
        }

        private void ConfigureRoomButtonClicked( object sender, RoutedEventArgs e ) {
            createRoomStackPanel.Visibility = Visibility.Visible;
        }

        private void CancelButtonClicked( object sender, RoutedEventArgs e ) {
            selectStackPanel.Visibility = Visibility.Hidden;
        }

        private void SettingsButtonClicked( object sender, RoutedEventArgs e ) {
            settingsStackPanel.Visibility = Visibility.Visible;
        }

        private void SaveSettingsButtonCllicked( object sender, RoutedEventArgs e ) {
            // Fix this
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo( "en-US" );
        }

        private void CancelSettingsButtonClicked( object sender, RoutedEventArgs e ) {
            settingsStackPanel.Visibility = Visibility.Hidden;
        }

        private void exitButtonClicked( object sender, RoutedEventArgs e ) {
            Close();
        }


        private void GameLimitComboBoxLoaded( object sender, RoutedEventArgs e ) {
            List<double> data = new List<double>();
            data.Add( 30 );
            data.Add( 45 );
            data.Add( 60 );
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void LanguageComboBoxLoaded( object sender, RoutedEventArgs e ) {
            List<string> data = new List<string>();
            data.Add( Resource.Main_ChoiceBox_English );
            data.Add( Resource.Main_ChoiceBox_Spanish );
            languageComboBox = sender as ComboBox;
            languageComboBox.ItemsSource = data;
            // Ternary Operator here!
            _ = CultureInfo.CurrentUICulture.Name == "en-US" ? languageComboBox.SelectedIndex = 0 : languageComboBox.SelectedIndex = 1;
        }

        private void PlayerLimitComboBoxLoaded( object sender, RoutedEventArgs e ) {
            List<int> data = new List<int>();
            data.Add( 2 );
            data.Add( 3 );
            data.Add( 5 );
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void RoundLimitComboBoxLoaded( object sender, RoutedEventArgs e ) {
            List<int> data = new List<int>();
            data.Add( 3 );
            data.Add( 5 );
            data.Add( 7 );
            data.Add( 10 );
            data.Add( 15 );
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }



    }

}
