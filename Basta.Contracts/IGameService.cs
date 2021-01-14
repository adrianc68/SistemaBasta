using Domain.Domain;
using System.Collections.Generic;
using System.ServiceModel;

namespace Basta.Contracts {
    [ServiceContract( CallbackContract = typeof( IGameClient ) )]
    public interface IGameService {
        [OperationContract( IsOneWay = true )]
        void StopButtonPressed( Room room, Player player );

        [OperationContract( IsOneWay = true )]
        void OpenChannel( Room room, Player player );

        [OperationContract( IsOneWay = true )]
        void RemoveChannel( Room room, Player player );

        [OperationContract( IsOneWay = true )]
        void ShowMainScreenBorder( Room room, Player player );

        [OperationContract( IsOneWay = true )]
        void ShowGridGameFromRoom( Room room, Player player );

        [OperationContract( IsOneWay = true )]
        void FinishedTimeGame( Room room, Player player );

        [OperationContract( IsOneWay = true )]
        void SendResults( Room room, Player player, Dictionary<GameCategory, Word> answers );


        [OperationContract( IsOneWay = true )]
        void StartNewRound( Room room, Player player );


    }

    [ServiceContract]
    public interface IGameClient {
        [OperationContract( IsOneWay = true )]
        void PlayerHasPressedStopButton( Player player );

        [OperationContract( IsOneWay = true )]
        void StartRound( GameConfiguration gameConfiguration );

        [OperationContract( IsOneWay = true )]
        void StartMainScreenBorder( double pointsWon );

        [OperationContract( IsOneWay = true )]
        void StartShowResults( Dictionary<string, Dictionary<GameCategory, Word>> results, Player player );

        [OperationContract( IsOneWay = true )]
        void StartGridGame();

        [OperationContract( IsOneWay = true )]
        void CloseGame();

        [OperationContract( IsOneWay = true )]
        void SomeoneHasDisconnected( Player player );

        [OperationContract( IsOneWay = true )]
        void SendAnswersToService();

    }

}
