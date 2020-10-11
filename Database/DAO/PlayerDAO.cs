using Database.DAO.Interface;
using Domain.Domain;
using MySql.Data.MySqlClient;
using System.Data;

namespace Database.DAO {
    public class PlayerDAO : IPlayerDAO {
        private DatabaseConnection databaseConnection;

        public bool AddPlayerAccount(Player player) {
            bool isPlayerAdded = false;
            try {
                MySqlCommand callableCommand = new MySqlCommand( "addPlayerAccount", databaseConnection.GetConnection() );
                callableCommand.CommandType = CommandType.StoredProcedure;
                callableCommand.Parameters.Add("email_in", MySqlDbType.VarChar).Value = player.AccessAccount.Email;
                callableCommand.Parameters.Add("age_in", MySqlDbType.Int32).Value = player.Age;
                callableCommand.Parameters.Add("country_in", MySqlDbType.VarChar).Value = player.Country;
                callableCommand.Parameters.Add("username_in", MySqlDbType.VarChar).Value = player.AccessAccount.Username;
                callableCommand.Parameters.Add("password_in", MySqlDbType.VarChar).Value = player.AccessAccount.Password;
                int result = callableCommand.ExecuteNonQuery();
                isPlayerAdded = (result != 0);
            }
            catch (MySqlException) {
                throw;
            }
            return isPlayerAdded;
        }
    }

}
