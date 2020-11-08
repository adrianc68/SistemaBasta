using Database.DAO.Interface;
using MySql.Data.MySqlClient;
using System.Data;

namespace Database.DAO {
    public class HostDAO: IHostDAO {
        private DatabaseConnection databaseConnection;

        public HostDAO() {
            databaseConnection = new DatabaseConnection();
        }

        public int getAttemptsByMacAddress( string address ) {
            int attempsByAddress = -1;
            using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
                using ( MySqlCommand command = connection.CreateCommand() ) {
                    command.CommandText = "SELECT attempts FROM Host WHERE mac_address = @mac_adress";
                    command.Parameters.Add( "mac_adress", MySqlDbType.VarChar );
                    command.Parameters[0].Value = address;
                    using ( MySqlDataReader reader = command.ExecuteReader() ) {
                        if ( reader.Read() ) {
                            attempsByAddress = reader.GetInt16(0);
                        }
                    }
                }
            }
            return attempsByAddress;
        }

        public bool resetAttempts( string address ) {
            bool isAttemptsReset = false;
            using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
                using ( MySqlCommand command = connection.CreateCommand() ) {
                    command.CommandText = "UPDATE Host SET attempts = 0 WHERE mac_address = @mac_address";
                    command.Parameters.Add( "mac_address", MySqlDbType.VarChar ).Value = address;
                    int rowsAffected = command.ExecuteNonQuery();
                    if ( rowsAffected > 0 ) {
                        isAttemptsReset = true;
                    }
                }
            }
            return isAttemptsReset;
        }

        public bool sendActualMacAddress( string address ) {
            bool isAddressSent = false;
            try {
                using ( MySqlConnection mySqlConnection = databaseConnection.GetConnection() ) {
                    using ( MySqlCommand callableCommand = new MySqlCommand( "sendAddress", mySqlConnection ) ) {
                        callableCommand.CommandType = CommandType.StoredProcedure;
                        callableCommand.Parameters.Add( "dir", MySqlDbType.VarChar ).Value = address;
                        int result = callableCommand.ExecuteNonQuery();
                        isAddressSent = ( result != 0 );
                    }
                }
            } catch ( MySqlException ) {
                throw;
            }
            return isAddressSent;
        }
    }
}
