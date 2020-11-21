using Domain.Domain;
using System.Collections.Generic;

namespace Database.DAO.Interface {
    public interface IRoomDAO {

        List<Room> GetRoomAvailable();
        string CreateRoom(Room room);

        bool DeleteRoom(string code);

        bool ConfigureRoom(RoomConfiguration roomConfiguration, string code);

        bool VerifyExistingRoom(string code);

    }
}
