using Database.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class HostDAOTest {

        [TestMethod]
        public void HostDAO_Test_1() {
            string macAddress = "12345";
            var result = new HostDAO().sendActualMacAddress( macAddress );
            var expected = true;
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void HostDAO_Test_2() {
            string macAddress = "12345";
            var unexpected = 1;
            var result = new HostDAO().getAttemptsByMacAddress( macAddress );
            Assert.AreEqual( unexpected, result );
        }

        [TestMethod]
        public void HostDAO_Test_3() {
            string macAddress = "12345";
            var expected = true;
            var result = new HostDAO().resetAttempts( macAddress );
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void HostDAO_Test_4() {
            string macAddress = "Noexisting";
            var expected = false;
            var result = new HostDAO().resetAttempts( macAddress );
            Assert.AreEqual( expected, result );
        }



    }
}
