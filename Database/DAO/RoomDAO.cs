using Database.DAO.Interface;
using Database.Entity;
using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using Room = Domain.Domain.Room;

namespace Database.DAO {
    public class RoomDAO: IRoomDAO {

        // Create a Lobby or room
        /// <summary>
        /// It creates a new lobby in database, this lobby will be shown 
        /// in a list
        /// </summary>
        /// <returns>
        /// returns strings representing the room's code
        /// </returns>
        public string CreateRoom( Domain.Domain.Room room ) {
            string roomCode = null;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                Entity.Room roomDatabase = new Database.Entity.Room();
                roomDatabase.code = GenerateRoomCode();
                roomDatabase.RoomConfiguration = new Entity.RoomConfiguration();
                roomDatabase.RoomConfiguration.playerLimit = room.RoomConfiguration.PlayerLimit;
                roomDatabase.RoomConfiguration.code = roomDatabase.code;
                roomDatabase.RoomConfiguration.roomState = (Entity.RoomState) room.RoomConfiguration.RoomState;
                roomCode = roomDatabase.code;
                database.Rooms.Add( roomDatabase );
                database.RomConfigurations.Add( roomDatabase.RoomConfiguration );
                database.SaveChanges();
            }
            return roomCode;
        }


        // Delete a room
        /// <summary>
        /// a Player deletes a room from database
        /// </summary>
        /// <returns>
        /// returns true if it was deleted otherwise false
        /// </returns>
        public bool DeleteRoom( Domain.Domain.Room room ) {
            bool isDeleted = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var roomDatabase = database.Rooms
                    .Where( b => b.code == room.Code ).FirstOrDefault();
                database.RomConfigurations.Remove( roomDatabase.RoomConfiguration );
                database.Rooms.Remove( roomDatabase );
                database.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

        // Set a new configuration to room
        /// <summary>
        /// It sets a new Configuration to a room created
        /// </summary>
        /// <returns>
        /// returns true if it was configured otherwise false
        /// </returns>
        public bool ConfigureRoom( Domain.Domain.RoomConfiguration roomConfiguration, string code ) {
            bool isUpdated = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var room = database.Rooms
                    .Where( b => b.code == code )
                    .FirstOrDefault();
                Database.Entity.RoomConfiguration roomConfigurationDatabase = new Database.Entity.RoomConfiguration();
                roomConfigurationDatabase.roomState = (Entity.RoomState) roomConfiguration.RoomState;
                roomConfigurationDatabase.code = roomConfiguration.Code;
                roomConfigurationDatabase.playerLimit = roomConfiguration.PlayerLimit;
                roomConfigurationDatabase.Room = room;
                room.RoomConfiguration = roomConfigurationDatabase;
                database.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }

        // Check existing room
        /// <summary>
        /// It checks in database if a room was created
        /// </summary>
        /// <returns>
        /// returns true if it exists in databaes otherwise false
        /// </returns>
        public bool VerifyExistingRoom( string code ) {
            bool isCodeUsed = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var room = database.Rooms
                    .Where( b => b.code == code )
                    .FirstOrDefault();
                isCodeUsed = room != null;
            }
            return isCodeUsed;
         }

        // Get all rooms available
        /// <summary>
        /// It returns all rooms with PUBLIC state from database
        /// </summary>
        /// <returns>
        /// returns List representing the rooms
        /// </returns>
        public List<Domain.Domain.Room> GetRoomAvailable() {
            List<Domain.Domain.Room> roomsAvailable = null;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var room = database.Rooms.Where( b => b.RoomConfiguration.roomState == Entity.RoomState.PUBLIC ).ToList();
                roomsAvailable = new List<Domain.Domain.Room>();
                foreach ( var roomDatabase in room ) {
                    Domain.Domain.Room roomDomain = new Domain.Domain.Room();
                    roomDomain.Code = roomDatabase.code;
                    roomDomain.RoomConfiguration = new Domain.Domain.RoomConfiguration();
                    roomDomain.RoomConfiguration.Code = roomDatabase.code;
                    roomDomain.RoomConfiguration.PlayerLimit = roomDatabase.RoomConfiguration.playerLimit;
                    roomDomain.RoomConfiguration.RoomState = (Domain.Domain.RoomState) roomDatabase.RoomConfiguration.roomState;
                    roomDomain.RoomConfiguration.Room = roomDomain;
                    roomsAvailable.Add( roomDomain );
                }
            }
            return roomsAvailable;
        }

        // Get a room by code
        /// <summary>
        /// It returns a room by a specified code if it exist in database
        /// </summary>
        /// <returns>
        /// returns Room by room's code
        /// </returns>
        public Domain.Domain.Room GetRoomByCode( string code ) {
            Room room = null;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var roomDatabase = database.Rooms.Where( b => b.code == code ).FirstOrDefault();
                if ( roomDatabase != null ) {
                    room = new Domain.Domain.Room();
                    room.Code = roomDatabase.code;
                    room.RoomConfiguration = new Domain.Domain.RoomConfiguration();
                    room.RoomConfiguration.Code = roomDatabase.code;
                    room.RoomConfiguration.PlayerLimit = roomDatabase.RoomConfiguration.playerLimit;
                    room.RoomConfiguration.RoomState = (Domain.Domain.RoomState) roomDatabase.RoomConfiguration.roomState;
                }
            }
            return room;
        }

        // Get all rooms from database
        /// <summary>
        /// It returns a list with all rooms from database
        /// </summary>
        /// <returns>
        /// returns List representing the rooms
        /// </returns>
        public List<Domain.Domain.Room> GetRooms() {
            List<Domain.Domain.Room> rooms = null;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var room = database.Rooms.ToList();
                rooms = new List<Domain.Domain.Room>();
                foreach ( var roomDatabase in room ) {
                    Domain.Domain.Room roomDomain = new Domain.Domain.Room();
                    roomDomain.Code = roomDatabase.code;
                    roomDomain.RoomConfiguration = new Domain.Domain.RoomConfiguration();
                    roomDomain.RoomConfiguration.Code = roomDatabase.code;
                    roomDomain.RoomConfiguration.PlayerLimit = roomDatabase.RoomConfiguration.playerLimit;
                    roomDomain.RoomConfiguration.RoomState = (Domain.Domain.RoomState) roomDatabase.RoomConfiguration.roomState;
                    roomDomain.RoomConfiguration.Room = roomDomain;
                    rooms.Add( roomDomain );
                }
            }
            return rooms;
        }

        private string GenerateRoomCode() {
            string code = null;
            code = Cryptography.RandomString().Substring( 0, 6 );
            while ( VerifyExistingRoom( code ) ) {
                code = Cryptography.RandomString().Substring( 0, 6 );
            }
            return code;
        }


    }
}
