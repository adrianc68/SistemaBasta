using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Basta.GUI.Login.Lobby {
    /// <summary>
    /// Lógica de interacción para LobbyWindow.xaml
    /// </summary>
    public partial class LobbyWindow: Window {
        private Room room;
        private Autentication autentication;
        public LobbyWindow( Room room ) {
            InitializeComponent();
            this.room = room;
            autentication = Autentication.GetInstance();
            ShowPlayerLabelElements();
        }


        Dictionary<Player, PlayerGUIElement> playersConnected = new Dictionary<Player, PlayerGUIElement>( new Player.EqualityComparer() );


        public void RecivedMessageFromGlobalRoom( Player player, string message ) {
            MessageReceived messageReceived = new MessageReceived();
            messageReceived.usernameLabel.Text = player.Name;
            messageReceived.messageTextBlock.Text = message;
            messagesWrapPanel.Children.Add( messageReceived );
        }

        public void RecivedMessageFromPlayer( Player player, string message ) {
            MessageReceived messageReceived = new MessageReceived();
            messageReceived.usernameLabel.Text = player.Name;
            messageReceived.messageTextBlock.Text = message;
            playersConnected[player].ChatWith.messagesWrapPanel.Children.Add( messageReceived );
            playersConnected[player].UserChat.borderBackground.Background = new SolidColorBrush( Color.FromArgb( 98, 98, 98, 98 ) );

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
                userChatGUI.messageTextBlock.Text = player.Name;
                playerConnected.UserChat = userChatGUI;

                UserConnectedLobby userConnectedLobbyGUI = new UserConnectedLobby();
                userConnectedLobbyGUI.usernameLabel.Text = player.Name;
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

            }
        }

        private void ConfigureUserConnectedLobbyKickButton( Player player ) {
            playersConnected[player].UserConnectedLobby.kickPlayerButton.MouseDoubleClick += new MouseButtonEventHandler( ( a, b ) => {
                Autentication.GetInstance().RoomServer.KickPlayer( player, room );
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
            roundsLimitLabel.Text = "";
            roundTimeLabel.Text = "";
        }

        private void ExitButtonClicked( object sender, RoutedEventArgs e ) {
            ClearPlayerListGUI();
            Close();
        }

        private void OnCloseWindow( object sender, EventArgs e ) {
            autentication.RoomServer.UserDisconnectedFromRoom( Autentication.GetInstance().Player, room );
        }

        private void DeleteRoomButtonClicked( object sender, RoutedEventArgs e ) {
            autentication.RoomServer.DeleteRoom( room );
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
                autentication.RoomServer.
                    SendMessageRoomChat( Autentication.GetInstance().Player, room, messageTextBox.Text.Trim() );
                messageTextBox.Text = null;
                messageScrollViewer.ScrollToEnd();
            }
        }

        private void SendMessageToSpecifiedPlayer( Player toPlayer, string message ) {
            if ( message.Length > 0 ) {
                MessageSent messageUserControl = new MessageSent();
                messageUserControl.messageTextBlock.Text = message;
                Player actualPlayer = Autentication.GetInstance().Player;

                playersConnected[toPlayer].ChatWith.messagesWrapPanel.Children.Add( messageUserControl );
                autentication.RoomServer.SendMessageRoomChatToPlayer( actualPlayer, room, message, toPlayer );
                playersConnected[toPlayer].ChatWith.messageTextBox.Text = null;
                playersConnected[toPlayer].ChatWith.messageScrollViewer.ScrollToEnd();

            }
        }




        private void GlobalChatButtonPressed( object sender, MouseButtonEventArgs e ) {
            chatWithDockPanel.Visibility = Visibility.Hidden;
        }

    }
}
