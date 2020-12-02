using Basta.GUI.Login.Lobby;
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
            LobbyWindow.RoomDelected( room );
        }

    }
}
