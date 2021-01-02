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

        private Autentication autentication;
        private Player[] playersInRoom;

        public MainWindow() {
            InitializeComponent();
            autentication = Autentication.GetInstance();
            autentication.RoomServiceCallBack = new RoomServiceCallBack();
            InstanceContext context = new InstanceContext( Autentication.GetInstance().RoomServiceCallBack );
            autentication.RoomServer = new Proxy.RoomServiceClient( context );
            autentication.RoomServiceCallBack.MainWindow = this;
        }

        public void GameIsFull() {
            MessageBoxResult kickedMessage = MessageBox.Show( Properties.Resource.SystemGameFull, Properties.Resource.SystemGameFull, MessageBoxButton.OK, MessageBoxImage.Error );
        }

        public void PlayerKicked() {
            MessageBoxResult kickedMessage = MessageBox.Show( Properties.Resource.SystemPlayerKicked, Properties.Resource.SystemPlayerKicked, MessageBoxButton.OK, MessageBoxImage.Error );
        }

        private void JoinToRoomButtonClicked( object sender, RoutedEventArgs e ) {
            Room selectedRoom = (Room) roomsListView.SelectedItem;
            if ( selectedRoom != null ) {
                ExecuteJoinRoomMethodServer( selectedRoom );
            } else if ( roomCodeTextField.Text.Length > 0 ) {
                selectedRoom = Autentication.GetInstance().RoomServer.GetRoomByCode( roomCodeTextField.Text );
                if ( selectedRoom != null ) {
                    ExecuteJoinRoomMethodServer( selectedRoom );
                } else {
                    MessageBoxResult kickedMessage = MessageBox.Show( Properties.Resource.SystemRoomNotFound, Properties.Resource.SystemMessageTitle, MessageBoxButton.OK, MessageBoxImage.Error );
                }
            }
        }

        private void ExecuteJoinRoomMethodServer( Room selectedRoom ) {
            playersInRoom = autentication.RoomServer.GetConnectedUsersFromRoom( selectedRoom );
            Autentication.GetInstance().RoomServer.JoinRoom( Autentication.GetInstance().Player, selectedRoom );
            selectStackPanel.Visibility = Visibility.Hidden;
            createRoomStackPanel.Visibility = Visibility.Hidden;
        }

        public void Join() {
            Room selectedRoom = (Room) roomsListView.SelectedItem;
            autentication.RoomServiceCallBack.LobbyWindow = new LobbyWindow( selectedRoom );
            autentication.RoomServiceCallBack.LobbyWindow.AddPlayerToGUI( Autentication.GetInstance().Player );
            autentication.RoomServiceCallBack.LobbyWindow.AddPlayersConnectedToGUI( playersInRoom );
            autentication.RoomServiceCallBack.LobbyWindow.Show();
        }

        private void CreateConfiguredRoomButtonClicked( object sender, RoutedEventArgs e ) {
            Room room = new Room();
            room.RoomConfiguration = new RoomConfiguration();
            room.RoomConfiguration.PlayerLimit = (int) personLimitComboBox.SelectedItem;
            room.RoomConfiguration.RoomState = RoomState.PUBLIC;
            room.Code = Autentication.GetInstance().RoomServer.CreateRoom( Autentication.GetInstance().Player, room );
            room.RoomConfiguration.Code = room.Code;
            if ( room.Code != null ) {
                Hide();
                selectStackPanel.Visibility = Visibility.Hidden;
                createRoomStackPanel.Visibility = Visibility.Hidden;
                autentication.RoomServiceCallBack.LobbyWindow = new LobbyWindow( room );
                autentication.RoomServiceCallBack.LobbyWindow.AddPlayerToGUI( Autentication.GetInstance().Player );
                autentication.RoomServiceCallBack.LobbyWindow.ShowDialog();
                ShowDialog();
            }
        }

        private void GetRoomsFromServer() {
            roomsListView.Items.Clear();
            foreach ( var room in autentication.RoomServer.GetRooms() ) {
                roomsListView.Items.Add( room );
            }
        }

        private void MainClosed( object sender, EventArgs e ) {
            if ( Autentication.GetInstance().Player != null ) {
                autentication.LoginServer.LogOut( Autentication.GetInstance().Player );
                Autentication.GetInstance().LogOut();
            }
        }

        private void PlayButtonClicked( object sender, RoutedEventArgs e ) {
            selectStackPanel.Visibility = Visibility.Visible;
            GetRoomsFromServer();
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
