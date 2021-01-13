using Basta.GUI.Login.Lobby;
using Basta.GUI.Login.Main;
using Basta.GUI.Login;
using Domain.Domain;


namespace Basta {
    public class RoomServiceCallBack: Proxy.IRoomServiceCallback {
        public Login LoginWindow { get; set; }
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
            LobbyWindow.RecivedMessageFromPlayer( player, message );
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

        public void OpenGameWindow() {
            LobbyWindow.OpenGameWindow();
        }
    }
}
