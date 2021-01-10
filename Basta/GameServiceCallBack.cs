using Basta.GUI.Login.Game;
using Domain.Domain;

namespace Basta {
    public class GameServiceCallBack: Proxy.IGameServiceCallback {
        public GameWindow GameWindow { get; set; }

        public void GameHasStarted() {
            GameWindow.StartRound();
        }

        public void PlayerHasPressedStopButton( Player player ) {
            GameWindow.SomeonePressedStopButton( player );
        }
    }
}
