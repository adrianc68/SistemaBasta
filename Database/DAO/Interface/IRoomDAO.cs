using Domain.Domain;
using System.Collections.Generic;

namespace Database.DAO.Interface {
    public interface IRoomDAO {

        List<Room> GetRoomAvailable();

        List<Room> GetRooms();

        string CreateRoom( Room room );

        bool DeleteRoom( Room room );

        bool ConfigureRoom( RoomConfiguration roomConfiguration, string code );

        bool VerifyExistingRoom( string code );



    }
}
