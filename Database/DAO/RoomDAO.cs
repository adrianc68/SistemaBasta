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
                Entity.RoomConfiguration roomConfigurationDatabase = new Database.Entity.RoomConfiguration();
                roomDatabase.RoomConfiguration = roomConfigurationDatabase;
                roomDatabase.RoomConfiguration.code = roomDatabase.code;
                roomCode = room.Code;
                database.Rooms.Add( roomDatabase );
                database.RomConfigurations.Add( roomDatabase.RoomConfiguration );
                database.SaveChanges();
            }
            return roomCode;
        }

        public bool DeleteRoom( string code ) {
            bool isDeleted = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var room = database.Rooms
                    .Where( b => b.code == code )
                    .FirstOrDefault();
                database.Rooms.Remove( room );
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
            code = Cryptography.RandomString().Substring( 0,6 );
            while( VerifyExistingRoom(code) ) {
                code = Cryptography.RandomString().Substring( 0,6 );
            }
            return code;
        }

        public List<Domain.Domain.Room> GetRoomAvailable() {
            List<Domain.Domain.Room> roomsAvailable = null;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var room = database.Rooms.Where( b => b.RoomConfiguration.roomState == Entity.RoomState.PUBLIC ).ToList();
                roomsAvailable = new List<Domain.Domain.Room>();
                foreach(var roomDatabase in room) {
                    Domain.Domain.Room roomDomain = new Domain.Domain.Room();
                    roomDomain.Code = roomDatabase.code;
                    Domain.Domain.RoomConfiguration roomConfigurationDomain = new Domain.Domain.RoomConfiguration();
                    roomConfigurationDomain.Code = roomDatabase.code;
                    roomConfigurationDomain.PlayerLimit = roomDatabase.RoomConfiguration.playerLimit;
                    roomConfigurationDomain.RoomState = (Domain.Domain.RoomState) roomDatabase.RoomConfiguration.roomState;
                    roomConfigurationDomain.Room = roomDomain;
                    roomsAvailable.Add( roomDomain );
                }
            }
            return roomsAvailable;
        }
    }
}
