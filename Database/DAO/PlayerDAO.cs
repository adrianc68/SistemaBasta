using Database.DAO.Interface;
using Domain.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Database.DAO {

    /*
        The PlayerDAO class
        Contains all methods for getting and setting data from database.
    */
    public class PlayerDAO: IPlayerDAO {
        private DatabaseConnection databaseConnection;

        public PlayerDAO() {
            databaseConnection = new DatabaseConnection();
        }

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
                using ( MySqlConnection mySqlConnection = databaseConnection.GetConnection() ) {
                    using ( MySqlCommand callableCommand = new MySqlCommand( "addPlayerAccount", mySqlConnection ) ) {
                        callableCommand.CommandType = CommandType.StoredProcedure;
                        callableCommand.Parameters.Add( "email_in", MySqlDbType.VarChar ).Value = player.AccessAccount.Email;
                        callableCommand.Parameters.Add( "age_in", MySqlDbType.Int32 ).Value = player.Age;
                        callableCommand.Parameters.Add( "country_in", MySqlDbType.VarChar ).Value = player.Country;
                        callableCommand.Parameters.Add( "username_in", MySqlDbType.VarChar ).Value = player.AccessAccount.Username;
                        callableCommand.Parameters.Add( "password_in", MySqlDbType.VarChar ).Value = player.AccessAccount.Password;
                        callableCommand.Parameters.Add( "name_in", MySqlDbType.VarChar ).Value = player.Name;
                        int result = callableCommand.ExecuteNonQuery();
                        isPlayerAdded = ( result != 0 );
                    }
                }
            } catch ( MySqlException ) {
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
            Player player = null;
            using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
                using ( MySqlCommand command = connection.CreateCommand() ) {
                    command.CommandText = "SELECT ACA.username, ACA.email, ACA.password, ACA.account_state, PLA.name, PLA.age, PLA.country, PLA.location FROM AccessAccount AS ACA INNER JOIN Player AS PLA ON ACA.email = PLA.email AND ACA.email = @email AND ACA.password = @password";
                    command.Parameters.Add( "email", MySqlDbType.VarChar ).Value = email;
                    command.Parameters.Add( "password", MySqlDbType.VarChar ).Value = password;
                    using ( MySqlDataReader reader = command.ExecuteReader() ) {
                        if ( reader.Read() ) {
                            AccessAccount account = new AccessAccount();
                            account.AccountState = (AccountState) Enum.Parse( typeof( AccountState ), reader.GetString( "account_state" ) ); ;
                            account.Email = reader.GetString( "email" );
                            account.Password = reader.GetString( "password" );
                            account.Username = reader.GetString( "username" );
                            player = new Player();
                            player.AccessAccount = account;
                            player.Age = reader.GetInt16( "age" );
                            player.Country = reader.GetString( "country" );
                            player.Name = reader.GetString( "name" );
                        }
                    }
                }
            }
            return player;
        }
    }

}
