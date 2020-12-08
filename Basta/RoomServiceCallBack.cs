using Basta.GUI.Login.Lobby;
using Basta.GUI.Login.Main;
using Domain.Domain;

namespace Basta {
    public class RoomServiceCallBack: Proxy.IRoomServiceCallback {
        public LobbyWindow LobbyWindow { get; set; }
        public MainWindow MainWindow { get; set; }

        public void PlayerConnected( Player player ) {
            LobbyWindow.PlayerConnected( player );
        }

        public void PlayerDisconnected( Player player ) {
            LobbyWindow.RemovePlayerFromGUI( player );
        }

        public void ReciveMessageRoom( Player player, string message ) {
            LobbyWindow.RecivedMessageFromGlobalChat( player, message );
        }

        public void ReciveMessageFromPlayer( Player player, string message ) {
            LobbyWindow.RecivedMessageFromPlayer( player, message );
        }

        public void RoomDelected( Room room ) {
            LobbyWindow.RoomDelected( room );
        }

        public void PlayerKicked() {
            LobbyWindow.YourPlayerWasKicked();
        }

        public void GameIsFull() {
            MainWindow.GameFull();
        }

        public void PlayersIsKicked() {
            MainWindow.PlayerIsKicked();
        }

        public void PlayerHasJoinToRoom( Player[] players, Room room ) {
            MainWindow.YouEnteredToAnotherRoom( players, room );
        }

        public void ReciveRoomsAvailable( Room[] rooms ) {
            MainWindow.ReciveRooms( rooms );
        }
    }
}



