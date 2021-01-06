using System.Data.Entity.Validation;
using Database.DAO;
using Domain.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;

namespace Test {
    [TestClass]
    public class PlayerDAOTest {
        [TestMethod]
        public void SignUpMethod_Test_1() {
            Player player = new Player();
            player.Age = (short) 4;
            player.Country = "Mexico";
            player.Name = "Angel Adrian Camal Garcia";
            player.Email = "angeladriancamalgarcia@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "root";
            accessAccount.Email = "angeladriancamalgarcia@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "12345678" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SignUpMethod_Test_2() {
            Player player = new Player();
            player.Age = (short) 120;
            player.Country = "Mexico";
            player.Name = "Roberto Jimenez Hernandez";
            player.Email = "zS18012172@estudiantes.uv.mx";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "robertjimenez32";
            accessAccount.Email = "zS18012172@estudiantes.uv.mx";
            accessAccount.Password = Cryptography.SHA256_Hash( "87654321" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_3() {
            Player player = new Player();
            player.Age = (short) 5;
            player.Country = "Estados Unidos";
            player.Name = "Javier Iniesta Wolfram";
            player.Email = "javiini@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "javiiniesta12";
            accessAccount.Email = "javiini@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "contraseña" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_4() {
            Player player = new Player();
            player.Age = (short) 121;
            player.Country = "Mexico";
            player.Name = "Esmeralda Jimenez Cortes";
            player.Email = "esmeraldajim@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "esmeralda";
            accessAccount.Email = "esmeraldajim@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "contraseña123" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_5() {
            Player player = new Player();
            player.Age = (short) 3;
            player.Country = "Jamaica";
            player.Name = "Usain Bolt Sherwood";
            player.Email = "usain@gob.jam";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "usainBolt";
            accessAccount.Email = "usain@gob.jam";
            accessAccount.Password = Cryptography.SHA256_Hash( "usainbolt1313" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;
            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );

        }

        [TestMethod]
        public void SignUpMethod_Test_6() {
            Player player = new Player();
            player.Age = (short) 119;
            player.Country = "Estados Unidos";
            player.Name = "Morgan Freeman Brashaw";
            player.Email = "morgan@freeman.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "god";
            accessAccount.Email = "morgan@freeman.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "morganfreeman453a" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );

        }

        [TestMethod]
        public void SignUpMethod_Test_7() {
            Player player = new Player();
            player.Age = (short) 60;
            player.Country = "Mexico";
            player.Name = "Josue Capistran Garcia";
            player.Email = "josuecapistran@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "josuecapi";
            accessAccount.Email = "josuecapistran@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "capistran1413" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_8() {
            Player player = new Player();
            player.Age = (short) 60;
            player.Country = "Mexico";
            player.Name = "Josue Romulo Sosa Ortiz";
            player.Email = "josuejose@gob.mx";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "josejoseabcA$";
            accessAccount.Email = "josuejose@gob.mx";
            accessAccount.Password = Cryptography.SHA256_Hash( "josejose" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_9() {
            Player player = new Player();
            player.Age = (short) 60;
            player.Country = "Mexico";
            player.Name = "Adrian Aquiles Jimenez";
            player.Email = "charger19@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "charger1917";
            accessAccount.Email = "charger19@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "castro14" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_10() {
            Player player = new Player();
            player.Age = (short) 25;
            player.Country = "Mexico";
            player.Name = "Angel Adrian Camal Garcia";
            player.Email = "angelrobebrto@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "root";
            accessAccount.Email = "angelrobebrto@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "as09kdf" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;
        }

        [TestMethod]
        public void SignUpMethod_Test_11() {
            Player player = new Player();
            player.Age = (short) 25;
            player.Country = "Mexico";
            player.Name = "Angel Adrian Camal Garcia";
            player.Email = "angel@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "neverhaving";
            accessAccount.Email = "angel@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "as09kdf" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_12() {
            Player player = new Player();
            player.Age = (short) 25;
            player.Country = "Mexico";
            player.Name = "·$\"%$·%·$ Camal Garcia";
            player.Email = "random@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "random";
            accessAccount.Email = "random@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "random13932" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_13() {
            Player player = new Player();
            player.Age = (short) 35;
            player.Country = "Mexico";
            player.Name = "Husseim \"·$=$I·%·$";
            player.Email = "random2@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "husek";
            accessAccount.Email = "random2@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "galaxystart3202" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_14() {
            Player player = new Player();
            player.Age = (short) 35;
            player.Country = "3$%·$\"·";
            player.Name = "Husseim Bradshaw";
            player.Email = "random3@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "hloop";
            accessAccount.Email = "random3@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "rubiyoro" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_15() {
            Player player = new Player();
            player.Age = (short) 35;
            player.Country = "Dubai";
            player.Name = "Husseim Bradshaw";
            player.Email = "random4@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "·$%·$%$";
            accessAccount.Email = "random4@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "oroplatino" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_16() {
            Player player = new Player();
            player.Age = (short) 45;
            player.Country = "Mexico";
            player.Name = "Angel Adrian Camal Garcia";
            player.Email = "distance@hotmail.com";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "jukingA";
            accessAccount.Email = "distance@hotmail.com";
            accessAccount.Password = Cryptography.SHA256_Hash( "kskSpd3$" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_17() {
            Player player = new Player();
            player.Age = (short) 21;
            player.Country = "Mexico";
            player.Name = "Juan Francisco Salazar";
            player.Email = null;
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = null;
            accessAccount.Email = null;
            accessAccount.Password = null;
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;
            Assert.ThrowsException<DbEntityValidationException>( () => new PlayerDAO().AddPlayerAccount( player ) );
        }

        [TestMethod]
        public void SignUpMethod_Test_18() {
            Player player = new Player();
            player.Age = (short) 21;
            player.Country = null;
            player.Name = null;
            player.Email = null;
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = null;
            accessAccount.Email = null;
            accessAccount.Password = null;
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;
            Assert.ThrowsException<DbEntityValidationException>( () => new PlayerDAO().AddPlayerAccount( player ) );
        }

        [TestMethod]
        public void SignUpMethod_Test_19() {
            Player player = new Player();
            player.Age = (short) 37;
            player.Country = "Mexico";
            player.Name = "Francisco Ayala Ruiz";
            player.Email = "micorreo";
            AccessAccount accessAccount = new AccessAccount();
            accessAccount.Account_state = AccountState.FREE;
            accessAccount.Username = "francisco";
            accessAccount.Email = "micorreo";
            accessAccount.Password = Cryptography.SHA256_Hash( "ayala9061" );
            accessAccount.Player = player;
            player.AccessAccount = accessAccount;

            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.AddPlayerAccount( player );
            Assert.IsTrue( result );
        }

        [TestMethod]
        public void SignUpMethod_Test_20() {
            Player player = new Player();
            player.Age = (short) 37;
            player.Country = "Mexico";
            player.Name = "Francisco Ayala Ruiz";
            player.Email = "micorreo";
            player.AccessAccount = null;
            PlayerDAO playerDAO = new PlayerDAO();
            var result = playerDAO.GetPlayerAccount(player.Email, Cryptography.SHA256_Hash( "ayala9061" ) );
            result.AccessAccount = null;
            Assert.AreEqual(player.Name, result.Name);
        }






    }

}
