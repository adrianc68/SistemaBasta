using Basta.GUI.Login.Lobby;
using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Basta {
    public class RoomServiceCallBack: Proxy.IRoomServiceCallback {

        Dictionary<Player, UserConnectedLobby> playersGUI = new Dictionary<Player, UserConnectedLobby>( new Player.EqualityComparer() );
        Dictionary<string, UserChat> playersGUIChat = new Dictionary<string, UserChat>();


        public LobbyWindow LobbyWindow { get; set; }

        public void PlayerConnected( Player player ) {
            AddPlayerToGUI( player );
        }

        public void PlayerDisconnected( Player player ) {
            RemovePlayerFromGUI( player );
        }

        public void ReciveMessageRoom( Player player, string message ) {
            MessageReceived messageReceived = new MessageReceived();
            messageReceived.usernameLabel.Text = player.Name;
            messageReceived.messageTextBlock.Text = message;
            LobbyWindow.messagesWrapPanel.Children.Add( messageReceived );
        }

        public void AddPlayerToGUI( Player player ) {
            if ( !playersGUI.ContainsKey( player ) ) {
                UserConnectedLobby userConnectedLobby = new UserConnectedLobby();
                userConnectedLobby.usernameLabel.Text = player.Name;

                UserChat userChat = new UserChat();
                userChat.messageTextBlock.Text = player.Name;

                playersGUI.Add( player, userConnectedLobby );
                playersGUIChat.Add( player.Name, userChat );

                LobbyWindow.playersWrapPanel.Children.Add( playersGUI[player] );
                LobbyWindow.playersChatWrapPanel.Children.Add( playersGUIChat[player.Name] );
            }
        }

        public void RemovePlayerFromGUI( Player player ) {
            if ( playersGUI.ContainsKey( player ) && playersGUIChat.ContainsKey( player.Name ) ) {
                LobbyWindow.playersWrapPanel.Children.Remove( playersGUI[player] );
                LobbyWindow.playersChatWrapPanel.Children.Remove( playersGUIChat[player.Name] );
                
                playersGUIChat.Remove( player.Name );
                playersGUI.Remove( player );
            }
        }

        public void ClearPlayerListGUI() {
            playersGUI.Clear();
            playersGUIChat.Clear();
        }

        public void AddPlayersConnectedToGUI( Player[] players ) {
            foreach ( var player in players ) {
                AddPlayerToGUI( player );
            }
        }

        public void RoomDelected( Room room ) {
            playersGUI.Clear();
            playersGUIChat.Clear();
            LobbyWindow.Close();
        }
    }
}
