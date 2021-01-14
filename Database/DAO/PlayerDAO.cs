using Database.DAO.Interface;
using Database.Entity;
using System;
using System.Collections.Generic;
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
        public bool AddPlayerAccount( Domain.Domain.Player player ) {
            bool isPlayerAdded = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                Player playerDatabase = new Player();
                playerDatabase.age = player.Age;
                playerDatabase.country = player.Country;
                playerDatabase.email = player.Email;
                playerDatabase.location = Location.NORTH;
                playerDatabase.name = player.Name;
                AccessAccount accountDatabase = new AccessAccount();
                accountDatabase.email = player.Email;
                accountDatabase.account_state = AccountState.FREE;
                accountDatabase.recovery_code = null;
                accountDatabase.password = player.AccessAccount.Password;
                accountDatabase.username = player.AccessAccount.Username;
                accountDatabase.Player = playerDatabase;
                playerDatabase.AccessAccount = accountDatabase;
                database.Players.Add( playerDatabase );
                database.AccessAccounts.Add( accountDatabase );
                database.SaveChanges();
                isPlayerAdded = true;
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
        public Domain.Domain.Player GetPlayerAccount( string email, string password ) {
            Domain.Domain.Player player = null;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var account = database.AccessAccounts
                    .Where( b => b.email == email ).Where( b => b.password == password )
                    .FirstOrDefault();
                if ( account != null ) {
                    player = new Domain.Domain.Player();
                    player.Age = account.Player.age;
                    player.Country = account.Player.country;
                    player.Email = account.Player.email;
                    player.Location = (Domain.Domain.Location) account.Player.location;
                    player.Name = account.Player.name;
                    player.AccessAccount = new Domain.Domain.AccessAccount();
                    player.AccessAccount.Email = player.Email;
                    player.AccessAccount.Password = null;
                    player.AccessAccount.Username = account.username;
                    player.AccessAccount.Recovery_code = null;
                    player.AccessAccount.Player = player;
                    player.Score = account.Player.score;
                    player.AccessAccount.Account_state = player.AccessAccount.Account_state;
                }
            }
            return player;
        }

        // Set a score to a player
        /// <summary>
        /// It's used to show in top table the ranking.
        /// </summary>
        public void SetScoreToPlayer( string email, double score ) {
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var account = database.Players.Where( b => b.email == email ).FirstOrDefault();
                if ( account != null ) {
                    account.score += score;
                    database.SaveChanges();
                }
            }
        }

        // Get score from database
        /// <summary>
        /// It gets top 10 player's score.
        /// </summary>
        /// <returns>
        /// return List<Player> with 10 elements
        /// </returns>
        public Queue<Domain.Domain.Player> GetTopTenPlayers() {
            Queue<Domain.Domain.Player> players = null;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var list = database.Players.OrderByDescending( x => x.score).Take(10);
                if ( list != null ) {
                    players = new Queue<Domain.Domain.Player>();
                    foreach ( var account in list ) {
                        Domain.Domain.Player player = null;
                        player = new Domain.Domain.Player();
                        player.Age = account.age;
                        player.Country = account.country;
                        player.Email = account.email;
                        player.Location = (Domain.Domain.Location) account.location;
                        player.Name = account.name;
                        player.Score = account.score;
                        player.AccessAccount = new Domain.Domain.AccessAccount();
                        player.AccessAccount.Email = player.Email;
                        player.AccessAccount.Password = null;
                        player.AccessAccount.Username = account.AccessAccount.username;
                        player.AccessAccount.Recovery_code = null;
                        player.AccessAccount.Player = player;
                        player.AccessAccount.Account_state = player.AccessAccount.Account_state;
                        players.Enqueue( player );
                    }
                }
            }
            return players;
        }
    }

}
