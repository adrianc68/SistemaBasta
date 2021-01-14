using Basta.GUI.Login.Game;
using Domain.Domain;
using System.Collections.Generic;

namespace Basta {
    public class GameServiceCallBack: Proxy.IGameServiceCallback {
        public GameWindow GameWindow { get; set; }

        public void PlayerHasPressedStopButton( Player player ) {
            GameWindow.SomeonePressedStopButton( player );
        }

        public void StartRound( GameConfiguration gameConfiguration ) {
            GameWindow.StartAnimationLetter( gameConfiguration );
        }

        public void StartMainScreenBorder( double pointsWon ) {
            GameWindow.StartMainScreenBorder( pointsWon );
        }

        public void StartShowResults( Dictionary<string, Dictionary<GameCategory, Word>> results, Player player) {
            GameWindow.StartShowResults(results, player);
        }

        public void StartGridGame() {
            GameWindow.StartGameInGridGame();
        }

        public void CloseGame() {
            GameWindow.Close();
        }

        public void SomeoneHasDisconnected( Player player ) {
            GameWindow.SomeoneDisconnectedFromGame( player );
        }

        public void SendAnswersToService() {
            GameWindow.SendAnswers();
        }
    }
}
