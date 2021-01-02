using Basta.GUI.Login.Lobby;
using Basta.GUI.Login.Main;
using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;


namespace Basta {
    public class RoomServiceCallBack: Proxy.IRoomServiceCallback {
        public LobbyWindow LobbyWindow { get; set; }
        public MainWindow MainWindow { get; set; }

        public void PlayerConnected( Player player ) {
            LobbyWindow.AddPlayerToGUI( player );
        }

        public void PlayerDisconnected( Player player ) {
            LobbyWindow.RemovePlayerFromGUI( player );
        }

        public void ReciveMessageRoom( Player player, string message ) {
            LobbyWindow.RecivedMessageFromGlobalRoom( player, message );
        }

        public void ReciveMessageFromPlayer( Player player, string message ) {
            LobbyWindow.RecivedMessageFromPlayer(player, message);
        }

        public void RoomDelected( Room room ) {
            LobbyWindow.RoomDelected();
        }

        public void PlayerKicked() {
            LobbyWindow.PlayerWasKicked();
        }

        public void Join() {
            MainWindow.Join();
        }

        public void YouHaveDisconnected() {
            LobbyWindow.RoomDelected();
        }

        public void GameIsFull() {
            MainWindow.GameIsFull();
        }

        public void PlayerAlreadyKicked() {
            MainWindow.PlayerKicked();
        }
    }
}
