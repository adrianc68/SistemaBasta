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
            autentication.RoomServiceCallBack.ClearPlayerListGUI();
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


    }
}
