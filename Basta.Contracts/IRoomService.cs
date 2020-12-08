using Domain.Domain;
using System.Collections.Generic;
using System.ServiceModel;

namespace Basta.Contracts {
    [ServiceContract( CallbackContract = typeof( IRoomClient ) )]
    public interface IRoomService {
        [OperationContract( IsOneWay = false )]
        string CreateRoom( Player player, Room room );

        [OperationContract( IsOneWay = true )]
        void JoinRoom( Player player, Room room );

        [OperationContract( IsOneWay = true )]
        void DeleteRoom( Room room );

        [OperationContract( IsOneWay = false )]
        void SetUpRoom();

        [OperationContract( IsOneWay = true )]
        void SendMessageRoomChat( Room room, string message );

        [OperationContract( IsOneWay = true )]
        void SendMessageRoomChatToPlayer( Room room, string message, Player toPlayer );

        [OperationContract( IsOneWay = true )]
        void GetRooms();

        [OperationContract( IsOneWay = true )]
        void UserDisconnectedFromRoom( Player player, Room room );

        [OperationContract( IsOneWay = true )]
        void KickPlayer( Player player, Room room );

    }

    [ServiceContract]
    public interface IRoomClient {
        [OperationContract( IsOneWay = true )]
        void ReciveRoomsAvailable(List<Room> rooms);

        [OperationContract( IsOneWay = true )]
        void ReciveMessageFromPlayer( Player player, string message );

        [OperationContract( IsOneWay = true )]
        void ReciveMessageRoom( Player player, string message );

        [OperationContract( IsOneWay = true )]
        void PlayerConnected( Player player );

        [OperationContract( IsOneWay = true )]
        void PlayerDisconnected( Player player );

        [OperationContract( IsOneWay = true )]
        void RoomDelected( Room room );

        [OperationContract( IsOneWay = true )]
        void PlayerKicked();

        [OperationContract( IsOneWay = true )]
        void GameIsFull();

        [OperationContract( IsOneWay = true )]
        void PlayersIsKicked();

        [OperationContract( IsOneWay = true )]
        void PlayerHasJoinToRoom(List<Player> players, Room room);
    }


}
