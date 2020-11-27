using Basta.GUI.Login.Lobby;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Basta.Properties;
using System.ServiceModel;
using Domain.Domain;

namespace Basta.GUI.Login.Main {

    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        Autentication autentication;

        public MainWindow() {
            InitializeComponent();
            autentication = Autentication.GetInstance();
            autentication.RoomServiceCallBack = new RoomServiceCallBack();
            InstanceContext context = new InstanceContext( Autentication.GetInstance().RoomServiceCallBack );
            autentication.RoomServer = new Proxy.RoomServiceClient( context );
        }

        private void PlayButtonClicked( object sender, RoutedEventArgs e ) {
            selectStackPanel.Visibility = Visibility.Visible;
            GetRoomsFromServer();
        }

        private void JoinToRoomButtonClicked( object sender, RoutedEventArgs e ) {
            Room selectedRoom = (Room) roomsListView.SelectedItem;
            if ( selectedRoom != null ) {
                try {
                    Autentication.GetInstance().RoomServer.JoinRoom( Autentication.GetInstance().Player, selectedRoom );
                    selectStackPanel.Visibility = Visibility.Hidden;
                    Hide();
                    autentication.RoomServiceCallBack.LobbyWindow = new LobbyWindow( selectedRoom );
                    autentication.RoomServiceCallBack.AddPlayerToGUI( Autentication.GetInstance().Player );
                    autentication.RoomServiceCallBack.AddPlayersConnectedToGUI( autentication.RoomServer.GetConnectedUsersFromRoom( selectedRoom ) );
                    autentication.RoomServiceCallBack.LobbyWindow.ShowDialog();
                    ShowDialog();
                } catch ( FaultException eka ) {
                    Console.WriteLine( eka.ToString() );
                }
                selectStackPanel.Visibility = Visibility.Hidden;
                createRoomStackPanel.Visibility = Visibility.Hidden;
            }

        }

        private void CreateConfiguredRoomButtonClicked( object sender, RoutedEventArgs e ) {
            Room room = new Room();
            room.RoomConfiguration = new RoomConfiguration();
            room.RoomConfiguration.PlayerLimit = (int) personLimitComboBox.SelectedItem;
            room.RoomConfiguration.RoomState = RoomState.PUBLIC;
            try {
                room.Code = Autentication.GetInstance().RoomServer.CreateRoom( Autentication.GetInstance().Player, room );
                room.RoomConfiguration.Code = room.Code;
            } catch ( FaultException ) {
                Console.WriteLine( "Unable to create room" );
            }
            if ( room.Code != null ) {
                selectStackPanel.Visibility = Visibility.Hidden;
                createRoomStackPanel.Visibility = Visibility.Hidden;
                Hide();
                autentication.RoomServiceCallBack.LobbyWindow = new LobbyWindow( room );
                autentication.RoomServiceCallBack.AddPlayerToGUI( Autentication.GetInstance().Player );
                autentication.RoomServiceCallBack.LobbyWindow.ShowDialog();
                ShowDialog();
            }
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
            autentication.LoginServer.LogOut( Autentication.GetInstance().Player );
            Autentication.GetInstance().LogOut();
            Close();
        }

        private void GetRoomsFromServer() {
            roomsListView.Items.Clear();
            try {
                foreach ( var room in autentication.RoomServer.GetRooms() ) {
                    Console.WriteLine( room.Code );
                    roomsListView.Items.Add( room );
                }
            } catch ( FaultException e ) {
                Console.WriteLine( e );
            }
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

        private void MainClosed( object sender, EventArgs e ) {
            if ( Autentication.GetInstance().Player != null ) {
                autentication.LoginServer.LogOut( Autentication.GetInstance().Player );
                Autentication.GetInstance().LogOut();
            }
        }
    }

}
