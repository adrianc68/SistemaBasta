using Database.DAO;
using Domain.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class Z_AccessAccountDAOTest {
        [TestMethod]
        public void AccessAccountDAO_TestMethod_1() {
            string accesssAccount = "root";
            var expected = true;
            var result = new AccessAccountDAO().verifyExistingUsername( accesssAccount );
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void AccessAccountDAO_TestMethod_2() {
            string accesssAccount = "NoExistingAccountInUsername";
            var expected = false;
            var result = new AccessAccountDAO().verifyExistingUsername( accesssAccount );
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void AccessAccountDAO_TestMethod_3() {
            string accesssAccount = "angeladriancamalgarcia@hotmail.com";
            var expected = true;
            var result = new AccessAccountDAO().verifyExistingEmail( accesssAccount );
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void AccessAccountDAO_TestMethod_4() {
            string accesssAccount = "thisEmaildoesntexistindatabase@hotmail.com";
            var expected = false;
            var result = new AccessAccountDAO().verifyExistingEmail( accesssAccount );
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void AccessAccountDAO_TestMethod_5() {
            string accesssAccount = "angeladriancamalgarcia@hotmail.com";
            string newPassword = "holacomoestas";
            var expected = true;
            var result = new AccessAccountDAO().ChangePasswordByEmail( accesssAccount, newPassword );
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void AccessAccountDAO_TestMethod_6() {
            string accesssAccount = "NoExistingAccountAndEmail@hotmail.com";
            string newPassword = "holacomoestas";
            var expected = false;
            var result = new AccessAccountDAO().ChangePasswordByEmail( accesssAccount, newPassword );
            Assert.AreEqual( expected, result );
        }

        [TestMethod]
        public void AccessAccountDAO_TestMethod_7() {
            string accesssAccount = "angeladriancamalgarcia@hotmail.com";
            var result = new AccessAccountDAO().GenerateRecoveryCodeByEmail( accesssAccount );
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AccessAccountDAO_TestMethod_8() {
            string accesssAccount = "NoExistingInDatabaseEmail@hotmail.com";
            var result = new AccessAccountDAO().GenerateRecoveryCodeByEmail( accesssAccount );
            Assert.IsNull( result );
        }

        [TestMethod]
        public void AccessAccountDAO_TestMethod_9() {
            string accesssAccount = "angeladriancamalgarcia@hotmail.com";
            AccountState accountStateExpected = AccountState.FREE;
            var result = new AccessAccountDAO().CheckAccountState( accesssAccount );
            Assert.AreEqual( accountStateExpected, result );
        }




    }
}
