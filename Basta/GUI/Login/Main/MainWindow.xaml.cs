using Basta.GUI.Login.Lobby;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Basta.Properties;
using Database.DAO;
using System.ServiceModel;
using Domain.Domain;

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
            RoomDAO roomDAO = new RoomDAO();
            roomDAO.GetRoomAvailable().ForEach( c => Console.WriteLine( c.Code ) );
        }

        private void JoinToRoomButtonClicked( object sender, RoutedEventArgs e ) {

        }

        private void CreateConfiguredRoomButtonClicked( object sender, RoutedEventArgs e ) {
            Room room = new Room();
            RoomConfiguration roomConfigurations = new RoomConfiguration();
            roomConfigurations.PlayerLimit = (int) personLimitComboBox.SelectedItem;
            roomConfigurations.RoomState = RoomState.PUBLIC;

            room.RoomConfiguration = roomConfigurations;

            RoomDAO roomDAO = new RoomDAO();
            room.Code = roomDAO.CreateRoom( room );


            InstanceContext context = new InstanceContext( new RoomServiceCallBack() );
            Proxy.RoomServiceClient server = new Proxy.RoomServiceClient( context );


            Player player = new Player();
            player.Email = "angeladriancamalgarcia@hotmail.com";
            player.Name = "angel adrian";
            AccessAccount account = new AccessAccount();
            account.Email = "angeladriancamalgarcia@hotmail.com";
            account.Account_state = 0;
            account.Player = player;
            account.Username = "root";
            player.AccessAccount = account;

            server.CreateRoom( player, room );
            
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
            Autentication.GetInstance().LogOut();
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
