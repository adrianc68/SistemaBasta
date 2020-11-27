using Database.DAO.Interface;
using Database.Entity;
using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Database.DAO {
    public class RoomDAO: IRoomDAO {

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

        public bool DeleteRoom( Domain.Domain.Room room ) {
            bool isDeleted = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var roomDatabase = database.Rooms
                    .Where( b => b.code == room.Code ).FirstOrDefault();
                database.RomConfigurations.Remove(roomDatabase.RoomConfiguration);
                database.Rooms.Remove(roomDatabase);
                database.SaveChanges();
                isDeleted = true;
            }
            return isDeleted;
        }

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

        private string GenerateRoomCode() {
            string code = null;
            code = Cryptography.RandomString().Substring( 0, 6 );
            while ( VerifyExistingRoom( code ) ) {
                code = Cryptography.RandomString().Substring( 0, 6 );
            }
            return code;
        }

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
    }
}
