using Basta.GUI.Login.Game;
using Domain.Domain;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Basta.GUI.Login.Lobby {
    /// <summary>
    /// Lógica de interacción para LobbyWindow.xaml
    /// </summary>
    public partial class LobbyWindow: Window {
        private Room room;
        private Autentication autentication;
        private Dictionary<Player, PlayerGUIElement> playersConnected = new Dictionary<Player, PlayerGUIElement>( new Player.EqualityComparer() );
        private HashSet<GameCategory> categoriesSelected;
        private int MIN_CATEGORIES_SELECTED = 5;
        private int MIN_PLAYERS_TO_START = 0; // change to 2
        private int MIN_TO_SHOW_RESULTS = 1; // change to 30
        private int MIN_TO_START = 3; // change to 10
        private int SECONDS_AFTER_STOP_BUTTON_PRESSED = 1; // change to 5

        public GameConfiguration GameConfiguration { get; set; }

        public LobbyWindow( Room room ) {
            InitializeComponent();
            this.room = room;
            autentication = Autentication.GetInstance();
            ShowPlayerLabelElements();
            playerlimitTopLabel.Content = room.RoomConfiguration.PlayerLimit.ToString();
            playersConnectedTopLabel.Content = 0;
        }

        public void OpenGameWindow() {
            Hide();
            GameWindow gameWindow = new GameWindow( room );
            foreach ( var player in playersConnected ) {
                gameWindow.AddPlayerLateralUserControl( player.Key.AccessAccount.Username );
            }
            gameWindow.Show();

        }

        public void RecivedMessageFromGlobalRoom( Player player, string message ) {
            MessageReceived messageReceived = new MessageReceived();
            messageReceived.usernameLabel.Text = player.AccessAccount.Username;
            messageReceived.messageTextBlock.Text = message;
            messagesWrapPanel.Children.Add( messageReceived );
            PlayChatSound();
        }

        public void RecivedMessageFromPlayer( Player player, string message ) {
            MessageReceived messageReceived = new MessageReceived();
            messageReceived.usernameLabel.Text = player.AccessAccount.Username;
            messageReceived.messageTextBlock.Text = message;
            playersConnected[player].ChatWith.messagesWrapPanel.Children.Add( messageReceived );
            playersConnected[player].UserChat.borderBackground.Background = new SolidColorBrush( Color.FromArgb( 98, 98, 98, 98 ) );
            PlayChatSound();
        }

        public void PlayerWasKicked() {
            RoomDelected();
            MessageBoxResult kickedMessage = MessageBox.Show( Properties.Resource.SystemPlayerKicked, Properties.Resource.SystemMessageTitle, MessageBoxButton.OK, MessageBoxImage.Error );
        }

        public void RoomDelected() {
            ClearPlayerListGUI();
            Close();
        }

        public void RemovePlayerFromGUI( Player player ) {
            if ( playersConnected.ContainsKey( player ) ) {
                playersWrapPanel.Children.Remove( playersConnected[player].UserConnectedLobby );
                playersChatWrapPanel.Children.Remove( playersConnected[player].UserChat );
                chatWithWrapPanel.Children.Remove( playersConnected[player].ChatWith );
                playersConnected.Remove( player );
                RemovePlayerToTopLabel();
            }
        }

        public void ClearPlayerListGUI() {
            playersWrapPanel.Children.Clear();
            playersChatWrapPanel.Children.Clear();
            chatWithWrapPanel.Children.Clear();
            playersConnected.Clear();

        }

        public void AddPlayersConnectedToGUI( Player[] players ) {
            foreach ( var player in players ) {
                AddPlayerToGUI( player );
            }
        }


        public void AddPlayerToGUI( Player player ) {
            if ( !playersConnected.ContainsKey( player ) ) {
                PlayerGUIElement playerConnected = new PlayerGUIElement();

                UserChat userChatGUI = new UserChat();
                userChatGUI.messageTextBlock.Text = player.AccessAccount.Username;
                playerConnected.UserChat = userChatGUI;

                UserConnectedLobby userConnectedLobbyGUI = new UserConnectedLobby();
                userConnectedLobbyGUI.usernameLabel.Text = player.AccessAccount.Username;
                playerConnected.UserConnectedLobby = userConnectedLobbyGUI;

                ChatWith chatWithGUI = new ChatWith();
                chatWithGUI.setInfoPlayer( player );
                playerConnected.ChatWith = chatWithGUI;

                playersWrapPanel.Children.Add( userConnectedLobbyGUI );
                playersChatWrapPanel.Children.Add( userChatGUI );
                chatWithWrapPanel.Children.Add( chatWithGUI );

                playersConnected.Add( player, playerConnected );

                ConfigureUserConnectedLobbyKickButton( player );
                ConfigureUserconnectedLobbySendMessageButton( player );
                ConfigureChatEventByPlayer( player );

                AddPlayerToTopLabel();

                if ( player.Email.Equals( Autentication.GetInstance().Player.Email ) ) {
                    userConnectedLobbyGUI.usernameLabel.Text = Properties.Resource.Lobby_Chat_You;
                    userChatGUI.messageTextBlock.Text = Properties.Resource.Lobby_Chat_You;
                }
            }
        }

        private void AddPlayerToTopLabel() {
            int playersConnected = Int32.Parse( playersConnectedTopLabel.Content.ToString() );
            playersConnected++;
            playersConnectedTopLabel.Content = playersConnected;
            if ( playersConnected == room.RoomConfiguration.PlayerLimit ) {
                countTopLabel.Content = Basta.Properties.Resource.SystemGameFull;
            }
        }

        private void RemovePlayerToTopLabel() {
            int playersConnected = Int32.Parse( playersConnectedTopLabel.Content.ToString() );
            playersConnected--;
            playersConnectedTopLabel.Content = playersConnected;
        }

        private void ConfigureUserConnectedLobbyKickButton( Player player ) {
            playersConnected[player].UserConnectedLobby.kickPlayerButton.MouseDoubleClick += new MouseButtonEventHandler( ( a, b ) => {
                try {
                    Autentication.GetInstance().RoomServer.KickPlayer( player, room );
                } catch ( Exception ex ) {
                    if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                        DropConnectionAlert.ShowDropConnectionAlert();
                        Close();
                    }
                }
            } );
        }

        private void ConfigureUserconnectedLobbySendMessageButton( Player player ) {
            playersConnected[player].UserConnectedLobby.sendMessageToPlayerButton.MouseDoubleClick += new MouseButtonEventHandler( ( a, b ) => {
                chatGrid.Visibility = Visibility.Visible;
                chatWithDockPanel.Visibility = Visibility.Visible;
                chatWithWrapPanel.Children.Clear();
                chatWithWrapPanel.Children.Add( playersConnected[player].ChatWith );
            } );
        }

        private void ConfigureChatEventByPlayer( Player player ) {
            playersConnected[player].ChatWith.messageTextBox.KeyDown += new KeyEventHandler( ( a, b ) => {
                if ( b.Key == Key.Enter ) {
                    SendMessageToSpecifiedPlayer( player, playersConnected[player].ChatWith.messageTextBox.Text.Trim() );
                }
            } );

            playersConnected[player].UserChat.MouseLeftButtonDown += new MouseButtonEventHandler( ( sender, e ) => {
                playersConnected[player].UserChat.borderBackground.Background = new SolidColorBrush( Color.FromArgb( 100, 44, 44, 44 ) );
                chatWithDockPanel.Visibility = Visibility.Visible;
                chatWithWrapPanel.Children.Clear();
                chatWithWrapPanel.Children.Add( playersConnected[player].ChatWith );
            } );
        }

        private void ShowPlayerLabelElements() {
            playersLimitLabel.Text = room.RoomConfiguration.PlayerLimit.ToString();
            roomCodeLabel.Text = room.Code;
            roomPrivacyLabel.Text = ( room.RoomConfiguration.RoomState == RoomState.PUBLIC ) ? Properties.Resource.Main_Label_Public : Properties.Resource.Main_Label_Private;
        }

        private void ExitButtonClicked( object sender, RoutedEventArgs e ) {
            ClearPlayerListGUI();
            Close();
        }

        private void OnCloseWindow( object sender, EventArgs e ) {
            try {
                autentication.RoomServer.UserDisconnectedFromRoom( Autentication.GetInstance().Player, room );
                autentication.RoomServiceCallBack.MainWindow.Show();
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    autentication.RoomServiceCallBack.MainWindow.Close();
                }
            }
        }

        private void DeleteRoomButtonClicked( object sender, RoutedEventArgs e ) {
            try {
                autentication.RoomServer.DeleteRoom( room );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    Close();
                }
            }
        }

        private void ChatButtonClicked( object sender, RoutedEventArgs e ) {
            chatGrid.Visibility = Visibility.Visible;
        }

        private void CloseChatButtonClicked( object sender, MouseButtonEventArgs e ) {
            chatGrid.Visibility = Visibility.Hidden;
        }

        private void MessageKeyPressed( object sender, KeyEventArgs e ) {
            if ( e.Key == Key.Enter ) {
                SendMessage();
            }
        }

        private void SendMessageButtonClicked( object sender, MouseButtonEventArgs e ) {
            SendMessage();
        }

        private void SendMessage() {
            string messageText = messageTextBox.Text.Trim();
            if ( messageTextBox.Text.Length > 0 ) {
                MessageSent message = new MessageSent();
                message.messageTextBlock.Text = messageTextBox.Text.Trim();
                messagesWrapPanel.Children.Add( message );
                try {
                    autentication.RoomServer.SendMessageRoomChat( Autentication.GetInstance().Player, room, messageTextBox.Text.Trim() );
                } catch ( Exception ex ) {
                    if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                        DropConnectionAlert.ShowDropConnectionAlert();
                        Close();
                    }
                }
                messageTextBox.Text = null;
                messageScrollViewer.ScrollToEnd();
                PlayChatSound();
            }
        }

        private void SendMessageToSpecifiedPlayer( Player toPlayer, string message ) {
            if ( message.Length > 0 ) {
                MessageSent messageUserControl = new MessageSent();
                messageUserControl.messageTextBlock.Text = message;
                Player actualPlayer = Autentication.GetInstance().Player;
                playersConnected[toPlayer].ChatWith.messagesWrapPanel.Children.Add( messageUserControl );
                try {
                    autentication.RoomServer.SendMessageRoomChatToPlayer( actualPlayer, room, message, toPlayer );
                } catch ( Exception ex ) {
                    if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                        DropConnectionAlert.ShowDropConnectionAlert();
                        Close();
                    }
                }
                playersConnected[toPlayer].ChatWith.messageTextBox.Text = null;
                playersConnected[toPlayer].ChatWith.messageScrollViewer.ScrollToEnd();
                PlayChatSound();
            }
        }

        private void GlobalChatButtonPressed( object sender, MouseButtonEventArgs e ) {
            chatWithDockPanel.Visibility = Visibility.Hidden;
        }

        private void PlayChatSound() {
            Sound sound = Sound.GetInstance();
            sound.PlayChatSound();
        }

        private void StartButtonClicked( object sender, RoutedEventArgs e ) {
            if ( playersConnected.Count > MIN_PLAYERS_TO_START ) {
                categoriesSelected = new HashSet<GameCategory>( MIN_CATEGORIES_SELECTED );
                UncheckCheckBox();
                gameConfigurationStackPanel.Visibility = Visibility.Visible;
            } else {
                PlayersNotEnoughtAlert();
            }
        }

        private void PlayersNotEnoughtAlert() {
            MessageBoxResult kickedMessage = MessageBox.Show( Properties.Resource.Lobby_Message_Playersnotenought, Properties.Resource.SystemMessageTitle, MessageBoxButton.OK, MessageBoxImage.Error );

        }

        private void SetUpRoomButtonClicked( object sender, RoutedEventArgs e ) {
            roomConfigurationStackPanel.Visibility = Visibility.Visible;
        }

        private void SetUpGameButtonClicked( object sender, RoutedEventArgs e ) {
            if(categoriesSelected.Count > 0) {
                GameConfiguration gameConfiguration = new GameConfiguration();
                gameConfiguration.RoundsLimit = (double) roundLimitComboBox.SelectedItem;
                gameConfiguration.TimeToEndRound = (double) roundTimeComboBox.SelectedItem;
                gameConfiguration.TimeToStart = MIN_TO_START;
                gameConfiguration.TimeToShowResults = MIN_TO_SHOW_RESULTS;
                gameConfiguration.TimeToStopGameOnButtonStopPressed = SECONDS_AFTER_STOP_BUTTON_PRESSED;
                gameConfiguration.ActualRound = 1;
                gameConfiguration.GameCategories = new Queue<GameCategory>();
                foreach ( var category in categoriesSelected ) {
                    gameConfiguration.GameCategories.Enqueue( category );
                }
                try {
                    Autentication.GetInstance().RoomServer.StartGamePressed( room, gameConfiguration, Autentication.GetInstance().Player );
                } catch ( Exception ex ) {
                    if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                        DropConnectionAlert.ShowDropConnectionAlert();
                        Close();
                    }
                }
                gameConfigurationStackPanel.Visibility = Visibility.Hidden;
            }
        }

        private void UncheckCheckBox() {
            animalCheckBox.IsChecked = false;
            cityCheckBox.IsChecked = false;
            objectCheckBox.IsChecked = false;
            colorCheckBox.IsChecked = false;
            countryCheckBox.IsChecked = false;
            fruitVegetableCheckBox.IsChecked = false;
            lastnameCheckBox.IsChecked = false;
            nameCheckBox.IsChecked = false;

        }

        private void CancelGameConfigurationButtonClicked( object sender, RoutedEventArgs e ) {
            gameConfigurationStackPanel.Visibility = Visibility.Hidden;
        }

        private void CountryCheckBoxClick( object sender, RoutedEventArgs e ) {
            AddCategoryToList( GameCategory.COUNTRY, countryCheckBox );
        }

        private void AddCategoryToList( GameCategory category, CheckBox checkBox ) {
            if ( (bool) checkBox.IsChecked ) {
                if ( categoriesSelected.Count < MIN_CATEGORIES_SELECTED ) {
                    categoriesSelected.Add( category );
                } else {
                    checkBox.IsChecked = false;
                }
            } else {
                categoriesSelected.Remove( category );
            }
        }

        private void ObjectCheckBoxClick( object sender, RoutedEventArgs e ) {
            AddCategoryToList( GameCategory.OBJECT, objectCheckBox );
        }

        private void NameCheckBoxClick( object sender, RoutedEventArgs e ) {
            AddCategoryToList( GameCategory.NAME, nameCheckBox );
        }

        private void LastNameCheckBoxClick( object sender, RoutedEventArgs e ) {
            AddCategoryToList( GameCategory.LAST_NAME, lastnameCheckBox );
        }

        private void ColorCheckBoxClick( object sender, RoutedEventArgs e ) {
            AddCategoryToList( GameCategory.COLOR, colorCheckBox );
        }

        private void CityCheckBoxClick( object sender, RoutedEventArgs e ) {
            AddCategoryToList( GameCategory.CITY, cityCheckBox );
        }

        private void FruitVegetableCheckBoxClick( object sender, RoutedEventArgs e ) {
            AddCategoryToList( GameCategory.FRUIT_VEGETABLE, fruitVegetableCheckBox );
        }

        private void AnimalCheckBoxClick( object sender, RoutedEventArgs e ) {
            AddCategoryToList( GameCategory.ANIMAL, animalCheckBox );
        }

        private void LimitRoundComboBoxLoaded( object sender, RoutedEventArgs e ) {
            List<double> data = new List<double>();
            data.Add( 1 ); // REMOVE THIS
            data.Add( 3 );
            data.Add( 5 );
            data.Add( 8 );
            data.Add( 15 );
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void RoundTimeComboBoxLoaded( object sender, RoutedEventArgs e ) {
            List<double> data = new List<double>();
            data.Add( 30 );
            data.Add( 45 );
            data.Add( 60 );
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void CancelRoomConfigurationButtonClicked( object sender, RoutedEventArgs e ) {
            roomConfigurationStackPanel.Visibility = Visibility.Hidden;
        }

        private void SetUpRoomConfigurationButtonClicked( object sender, RoutedEventArgs e ) {
            room.RoomConfiguration.RoomState = ( roomStateComboBox.SelectedIndex == 0 ) ? RoomState.PUBLIC : RoomState.PRIVATE;
        }

        private void RoomStateComboBoxLoaded( object sender, RoutedEventArgs e ) {
            List<string> data = new List<string>();
            data.Add( Properties.Resource.Main_Label_Public );
            data.Add( Properties.Resource.Main_Label_Private );
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            // Ternary operator here
            combo.SelectedIndex = ( room.RoomConfiguration.RoomState == RoomState.PUBLIC ) ? combo.SelectedIndex = 0 : combo.SelectedIndex = 1;
        }
    }
}
