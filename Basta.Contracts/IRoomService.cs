using Domain.Domain;
using System.Collections.Generic;
using System.ServiceModel;

namespace Basta.Contracts {
    [ServiceContract( CallbackContract = typeof( IRoomClient ) )]
    public interface IRoomService {
        [OperationContract( IsOneWay = false )]
        void CreateRoom(Player player, Room room);

        [OperationContract( IsOneWay = true )]
        void JoinRoom(Player player, Room room);

        [OperationContract( IsOneWay = false )]
        void DeleteRoom();

        [OperationContract( IsOneWay = false )]
        void SetUpRoom();

        [OperationContract( IsOneWay = true )]
        void ConnectToRoom( Player player );

        [OperationContract( IsOneWay = true )]
        void SendMessageRoomChat( string message );

        [OperationContract( IsOneWay = false )]
        List<Player> GetConnectedUsersFromRoom(string codeRoom);

    }

    [ServiceContract]
    public interface IRoomClient {
        [OperationContract( IsOneWay = true )]
        void ReciveMessageRoom( Player player, string message );

    }


}
