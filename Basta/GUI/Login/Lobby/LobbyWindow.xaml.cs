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
                autentication.RoomServer.SendMessageRoomChat( Autentication.GetInstance().Player, room, messageTextBox.Text.Trim() );
                messageTextBox.Text = null;
                messageScrollViewer.ScrollToEnd();
            }
        }

        private void GlobalChatButtonPressed( object sender, MouseButtonEventArgs e ) {
            chatWithDockPanel.Visibility = Visibility.Hidden;
        }



        Dictionary<Player, UserConnectedLobby> playersLobbyGUI = new Dictionary<Player, UserConnectedLobby>( new Player.EqualityComparer() );
        Dictionary<string, UserChat> playerLobbyChatGUI = new Dictionary<string, UserChat>();
        Dictionary<string, ChatWith> chatsGUI = new Dictionary<string, ChatWith>();



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
            chatsGUI[player.Email].messagesWrapPanel.Children.Add( messageReceived );
            playerLobbyChatGUI[player.Email].borderBackground.Background = new SolidColorBrush( Color.FromRgb( 98, 98, 98 ) );
        }


        public void RoomDelected( Room room ) {
            playersLobbyGUI.Clear();
            playerLobbyChatGUI.Clear();
            chatsGUI.Clear();
            Close();
        }

        public void AddPlayerToGUI( Player player ) {
            if ( !playersLobbyGUI.ContainsKey( player ) ) {
                AddUserConnectedControl( player );
                AddUserChatControl( player );
            }
        }

        public void RemovePlayerFromGUI( Player player ) {
            if ( playersLobbyGUI.ContainsKey( player ) && playerLobbyChatGUI.ContainsKey( player.Email ) ) {
                playersWrapPanel.Children.Remove( playersLobbyGUI[player] );
                playersChatWrapPanel.Children.Remove( playerLobbyChatGUI[player.Email] );
                playerLobbyChatGUI.Remove( player.Email );
                playersLobbyGUI.Remove( player );
            }
        }

        public void ClearPlayerListGUI() {
            playersLobbyGUI.Clear();
            playerLobbyChatGUI.Clear();
        }

        public void AddPlayersConnectedToGUI( Player[] players ) {
            foreach ( var player in players ) {
                AddPlayerToGUI( player );
            }
        }

        private void AddUserConnectedControl( Player player ) {
            UserConnectedLobby userConnectedLobby = new UserConnectedLobby();
            userConnectedLobby.Player = player;
            userConnectedLobby.usernameLabel.Text = player.Name;
            playersLobbyGUI.Add( player, userConnectedLobby );
            playersWrapPanel.Children.Add( playersLobbyGUI[player] );

        }

        private void AddUserChatControl( Player player ) {
            UserChat userChat = new UserChat();
            userChat.Player = player;
            userChat.Background = new SolidColorBrush( Color.FromArgb( 100, 44, 44, 44 ) );
            userChat.messageTextBlock.Text = player.Name;
            playerLobbyChatGUI.Add( player.Email, userChat );
            playersChatWrapPanel.Children.Add( playerLobbyChatGUI[player.Email] );
            AddChatWithUserControl( player );
        }

        private void AddChatWithUserControl( Player player) {
            ChatWith chatWith = new ChatWith();
            chatWith.Player = player;
            chatWith.setInfoPlayer();
            if ( !chatsGUI.ContainsKey( player.Email ) ) {
                chatsGUI.Add( player.Email, chatWith );
                configureChatEventByPlayer(player, chatWith );
            }
        }

        private void configureChatEventByPlayer(Player player, ChatWith chatWith ) {
            chatsGUI[player.Email].messageTextBox.KeyDown += new KeyEventHandler( ( a, b ) => {
                if ( b.Key == Key.Enter ) {
                    SendMessageToSpecifiedPlayer( player, chatsGUI[player.Email].messageTextBox.Text.Trim() );
                }
            } );

            playerLobbyChatGUI[ player.Email].MouseLeftButtonDown += new MouseButtonEventHandler( ( sender, e ) => {
                playerLobbyChatGUI[player.Email].borderBackground.Background = new SolidColorBrush( Color.FromArgb( 100, 44, 44, 44 ) );
                chatWithDockPanel.Visibility = Visibility.Visible;
                chatWithWrapPanel.Children.Clear();
                chatWithWrapPanel.Children.Add( chatWith );
            } );
        }

        private void SendMessageToSpecifiedPlayer( Player toPlayer, string message ) {
            if ( message.Length > 0 ) {
                MessageSent messageUserControl = new MessageSent();
                messageUserControl.messageTextBlock.Text = message;
                Player actualPlayer = Autentication.GetInstance().Player;
                chatsGUI[toPlayer.Email].messagesWrapPanel.Children.Add( messageUserControl );
                autentication.RoomServer.SendMessageRoomChatToPlayer( actualPlayer, room, message, toPlayer );
                chatsGUI[toPlayer.Email].messageTextBox.Text = null;
                chatsGUI[toPlayer.Email].messageScrollViewer.ScrollToEnd();
            }
        }









    }
}
