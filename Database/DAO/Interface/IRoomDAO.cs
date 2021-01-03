using Domain.Domain;
using System.Collections.Generic;

namespace Database.DAO.Interface {
    public interface IRoomDAO {
        // Create a Lobby or room
        /// <summary>
        /// It creates a new lobby in database, this lobby will be shown 
        /// in a list
        /// </summary>
        /// <returns>
        /// returns strings representing the room's code
        /// </returns>
        string CreateRoom( Room room );
        // Delete a room
        /// <summary>
        /// a Player deletes a room from database
        /// </summary>
        /// <returns>
        /// returns true if it was deleted otherwise false
        /// </returns>
        bool DeleteRoom( Room room );
        // Set a new configuration to room
        /// <summary>
        /// It sets a new Configuration to a room created
        /// </summary>
        /// <returns>
        /// returns true if it was configured otherwise false
        /// </returns>
        bool ConfigureRoom( RoomConfiguration roomConfiguration, string code );
        // Check existing room
        /// <summary>
        /// It checks in database if a room was created
        /// </summary>
        /// <returns>
        /// returns true if it exists in databaes otherwise false
        /// </returns>
        bool VerifyExistingRoom( string code );
        // Get all rooms available
        /// <summary>
        /// It returns all rooms with PUBLIC state from database
        /// </summary>
        /// <returns>
        /// returns List representing the rooms
        /// </returns>
        List<Room> GetRoomAvailable();
        // Get all rooms from database
        /// <summary>
        /// It returns a list with all rooms from database
        /// </summary>
        /// <returns>
        /// returns List representing the rooms
        /// </returns>
        List<Domain.Domain.Room> GetRooms();
        // Get a room by code
        /// <summary>
        /// It returns a room by a specified code if it exist in database
        /// </summary>
        /// <returns>
        /// returns Room by room's code
        /// </returns>
        Domain.Domain.Room GetRoomByCode( string code );
    }
}
