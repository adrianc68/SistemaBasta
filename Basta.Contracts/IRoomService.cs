﻿using Domain.Domain;
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

        [OperationContract( IsOneWay = false )]
        Queue<Player> GetTopTen();

        [OperationContract( IsOneWay = true )]
        void SendMessageRoomChat( Player player, Room room, string message );

        [OperationContract( IsOneWay = true )]
        void SendMessageRoomChatToPlayer( Player player, Room room, string message, Player toPlayer );

        [OperationContract( IsOneWay = false )]
        List<Player> GetConnectedUsersFromRoom( Room room );

        [OperationContract( IsOneWay = false )]
        List<Room> GetRooms();

        [OperationContract( IsOneWay = false )]
        Room GetRoomByCode( string code );

        [OperationContract( IsOneWay = true )]
        void UserDisconnectedFromRoom( Player player, Room room );

        [OperationContract( IsOneWay = true )]
        void KickPlayer( Player player, Room room );

        [OperationContract( IsOneWay = true )]
        void StartGamePressed( Room room, GameConfiguration gameConfiguration, Player player );


    }

    [ServiceContract]
    public interface IRoomClient {
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
        void Join();

        [OperationContract( IsOneWay = true )]
        void YouHaveDisconnected();

        [OperationContract( IsOneWay = true )]
        void OpenGameWindow();
    }


}
