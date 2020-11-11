using Database.DAO.Interface;
using Database.Entity;
using System;
using System.Data;
using System.Linq;

namespace Database.DAO {

    /*
        The PlayerDAO class
        Contains all methods for getting and setting data from database.
    */
    public class PlayerDAO: IPlayerDAO {
   
        // Add a Player in Database.
        /// <summary>
        /// It adds a specified Player in database.
        /// </summary>
        /// <returns>
        /// returns true if callableStatement was perfomed otherwise false.
        /// </returns>
        public bool AddPlayerAccount( Player player ) {
            bool isPlayerAdded = false;
            try {
                using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                    player.AccessAccount.account_state = AccountState.FREE;
                    player.location = Location.NORTH;
                    database.AccessAccounts.Add( player.AccessAccount );
                    database.Players.Add( player );
                    database.SaveChanges();
                    isPlayerAdded = true;
                }
            } catch(Exception ) {
                throw;
            }
            return isPlayerAdded;
        }

        // Get a Player from Database.
        /// <summary>
        /// It gets a Player by a email and password. It is used by a Login system.
        /// </summary>
        /// <returns>
        /// returns Player if exist Player otherwise null.
        /// </returns>
        public Player GetPlayerAccount( string email, string password ) {
            Player playerDatabase = null;
            try {
                using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                    var account = database.AccessAccounts
                        .Where( b => b.email == email ).Where( b => b.password == password)
                        .FirstOrDefault();

                    var player = database.Players
                        .Where( b => b.email == email )
                        .FirstOrDefault();

                    if(account != null) {
                        player.AccessAccount = account;
                    }
                    playerDatabase = player;
                }
            } catch ( Exception ) {
                throw;
            }
            return playerDatabase;
        }
    }

}
