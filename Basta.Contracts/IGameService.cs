using Domain.Domain;
using System.ServiceModel;

namespace Basta.Contracts {
    [ServiceContract( CallbackContract = typeof( IGameClient ) )]
    public interface IGameService {
        [OperationContract( IsOneWay = true )]
        void StopButtonPressed(Room room, Player player);

        [OperationContract( IsOneWay = true )]
        void OpenChannel( Room room, Player player );

        [OperationContract( IsOneWay = true )]
        void RemoveChannel( Room room, Player player );
    }

    [ServiceContract]
    public interface IGameClient {
        [OperationContract( IsOneWay = true )]
        void PlayerHasPressedStopButton( Player player );

        [OperationContract( IsOneWay = true )]
        void GameHasStarted();

    }

}
