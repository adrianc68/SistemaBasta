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
        private Room selectedRoom;

        public MainWindow() {
            InitializeComponent();
            autentication = Autentication.GetInstance();
            InstanceContext context = new InstanceContext( Autentication.GetInstance().RoomServiceCallBack );
            autentication.RoomServer = new Proxy.RoomServiceClient( context );
            autentication.RoomServiceCallBack.MainWindow = this;
            ConfigureVolumeSlider();
            usernameLabel.Text = autentication.Player.Name;
            GetTopTen();
        }

        public void GameIsFull() {
            MessageBoxResult kickedMessage = MessageBox.Show( Properties.Resource.SystemGameFull, Properties.Resource.SystemGameFull, MessageBoxButton.OK, MessageBoxImage.Error );
        }

        public void PlayerKicked() {
            MessageBoxResult kickedMessage = MessageBox.Show( Properties.Resource.SystemPlayerKicked, Properties.Resource.SystemPlayerKicked, MessageBoxButton.OK, MessageBoxImage.Error );
        }

        public void Join() {
            Hide();
            autentication.RoomServiceCallBack.LobbyWindow = new LobbyWindow( selectedRoom );
            autentication.RoomServiceCallBack.LobbyWindow.AddPlayerToGUI( Autentication.GetInstance().Player );
            autentication.RoomServiceCallBack.LobbyWindow.AddPlayersConnectedToGUI( playersInRoom );
            autentication.RoomServiceCallBack.LobbyWindow.Show();
        }

        private void JoinToRoomButtonClicked( object sender, RoutedEventArgs e ) {
            selectedRoom = (Room) roomsListView.SelectedItem;
            if ( selectedRoom != null ) {
                ExecuteJoinRoomMethodServer( selectedRoom );
            } else if ( roomCodeTextField.Text.Length > 0 ) {
                try {
                    selectedRoom = Autentication.GetInstance().RoomServer.GetRoomByCode( roomCodeTextField.Text );
                } catch ( Exception ex ) {
                    if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                        DropConnectionAlert.ShowDropConnectionAlert();
                        autentication.LogOut();
                        Close();
                    }
                }
                if ( selectedRoom != null ) {
                    ExecuteJoinRoomMethodServer( selectedRoom );
                } else {
                    MessageBoxResult kickedMessage = MessageBox.Show( Properties.Resource.SystemRoomNotFound, Properties.Resource.SystemMessageTitle, MessageBoxButton.OK, MessageBoxImage.Error );
                }
                roomCodeTextField.Clear();
            }
        }

        private void ExecuteJoinRoomMethodServer( Room selectedRoom ) {
            try {
                playersInRoom = autentication.RoomServer.GetConnectedUsersFromRoom( selectedRoom );
                Autentication.GetInstance().RoomServer.JoinRoom( Autentication.GetInstance().Player, selectedRoom );
                selectStackPanel.Visibility = Visibility.Hidden;
                createRoomStackPanel.Visibility = Visibility.Hidden;
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    autentication.LogOut();
                    Close();
                }
            }
        }

        private void CreateConfiguredRoomButtonClicked( object sender, RoutedEventArgs e ) {
            Room room = new Room();
            room.RoomConfiguration = new RoomConfiguration();
            room.RoomConfiguration.PlayerLimit = (int) personLimitComboBox.SelectedItem;
            // Ternary operator here !
            room.RoomConfiguration.RoomState = ( roomPrivacyComboBox.SelectedIndex == 0 ) ? RoomState.PUBLIC : RoomState.PRIVATE;
            try {
                room.Code = Autentication.GetInstance().RoomServer.CreateRoom( Autentication.GetInstance().Player, room );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    autentication.LogOut();
                    Close();
                }
            }
            room.RoomConfiguration.Code = room.Code;
            if ( room.Code != null ) {
                Hide();
                selectStackPanel.Visibility = Visibility.Hidden;
                createRoomStackPanel.Visibility = Visibility.Hidden;
                autentication.RoomServiceCallBack.LobbyWindow = new LobbyWindow( room );
                autentication.RoomServiceCallBack.LobbyWindow.AddPlayerToGUI( Autentication.GetInstance().Player );
                autentication.RoomServiceCallBack.LobbyWindow.ShowDialog();
            }
        }

        private void GetTopTen() {
            topTenListView.Items.Clear();
            try {
                int position = 0;
                foreach ( var players in autentication.RoomServer.GetTopTen() ) {
                    position += 1;
                    TopPlayerUserControl topPlayerUserControl = new TopPlayerUserControl();
                    topPlayerUserControl.usernameLabel.Text = players.AccessAccount.Username;
                    topPlayerUserControl.positionNumber.Text = position.ToString();
                    topPlayerUserControl.pointsGotIt.Text = players.Score.ToString();
                    topTenListView.Items.Add( topPlayerUserControl );
                }
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    autentication.LogOut();
                    Close();
                }
            }
        }

        private void GetRoomsFromServer() {
            roomsListView.Items.Clear();
            try {
                foreach ( var room in autentication.RoomServer.GetRooms() ) {
                    roomsListView.Items.Add( room );
                }
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    autentication.LogOut();
                    Close();
                }
            }
        }

        private void exitButtonClicked( object sender, RoutedEventArgs e ) {
            try {
                autentication.LoginServer.LogOut( Autentication.GetInstance().Player );
            } catch ( Exception ex ) {
                // LOG ME PLSSSSSSS
                Console.WriteLine( ex );
            }
            Autentication.GetInstance().LogOut();
            Close();
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

        private void MainClosed( object sender, EventArgs e ) {
            if ( Autentication.GetInstance().Player != null ) {
                try {
                    autentication.LoginServer.LogOut( Autentication.GetInstance().Player );
                } catch ( Exception ex ) {
                    // LOG ME PLSSSSSSS
                    Console.WriteLine( ex );
                }
                Autentication.GetInstance().LogOut();
            }
            Sound.GetInstance().StopMusic();
            Autentication.GetInstance().RoomServiceCallBack.LoginWindow.Show();
        }

        private void SaveSettingsButtonCllicked( object sender, RoutedEventArgs e ) {
            if ( languageComboBox.SelectedIndex == 0 ) {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo( "en-US" );
                Login loginWindow = new Login();
                loginWindow.ChangeLanguage( false );
                autentication.RoomServiceCallBack.LoginWindow = loginWindow;
                Close();
            } else {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo( "es-ES" );
                Login loginWindow = new Login();
                loginWindow.ChangeLanguage( true );
                autentication.RoomServiceCallBack.LoginWindow = loginWindow;
                Close();
            }
        }

        private void CancelSettingsButtonClicked( object sender, RoutedEventArgs e ) {
            settingsStackPanel.Visibility = Visibility.Hidden;
        }

        private void GameLimitComboBoxLoaded( object sender, RoutedEventArgs e ) {
            List<string> data = new List<string>();
            data.Add( Resource.Main_Label_Public );
            data.Add( Resource.Main_Label_Private );
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

        private void EnterButtonPressed( object sender, System.Windows.Input.KeyEventArgs e ) {
            Sound sound = Sound.GetInstance();
            if ( e.Key == System.Windows.Input.Key.Enter ) {
                sound.StopMusic();
                sound.RandomizePlayListIntroMusic();
                introTheme.Visibility = Visibility.Hidden;
            } else if ( e.Key == System.Windows.Input.Key.Escape ) {
                sound.StopMusic();
            }

        }

        private void VolumeChanged( object sender, RoutedPropertyChangedEventArgs<double> e ) {
            if ( e.NewValue < e.OldValue ) {
                Sound.GetInstance().VolumenUp( 1 );
            } else {
                Sound.GetInstance().VolumenDown( 1 );
            }
        }

        private void ConfigureVolumeSlider() {
            volumeSettingsSlider.Maximum = 100;
            volumeSettingsSlider.Minimum = 0;
        }


    }

}
