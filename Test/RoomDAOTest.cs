using Database.DAO;
using Domain.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class RoomDAOTest {
        private static Room room;

        [TestMethod]
        public void RoomDAO_Test_1() {
            room = new Room();
            room.Code = null; 
            room.RoomConfiguration = new RoomConfiguration();
            room.RoomConfiguration.PlayerLimit = 2;
            room.RoomConfiguration.RoomState = RoomState.PUBLIC;
            var result = new RoomDAO().CreateRoom(room);
            room.Code = result;
            room.RoomConfiguration.Code = room.Code;
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void RoomDAO_Test_2() {
            var expected = true;
            var result = new RoomDAO().VerifyExistingRoom( room.Code );
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void RoomDAO_Test_3() {
            var expected = 1;
            var result = new RoomDAO().GetRoomAvailable().Count;
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void RoomDAO_Test_4() {
            var result = new RoomDAO().GetRoomByCode( room.Code );
            Assert.IsNotNull( result );
        }

        [TestMethod]
        public void RoomDAO_Test_5() {
            var expected = 1;
            var result = new RoomDAO().GetRooms().Count;
            Assert.AreEqual( expected, result );
        }







    }
}
