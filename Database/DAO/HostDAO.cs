using Database.DAO.Interface;
using System.Data;

namespace Database.DAO {

    /*
        The PlayerDAO class
        Contains all methods for getting and setting data from database.
    */
    public class HostDAO: IHostDAO {

        // Get attempts from database
        /// <summary>
        /// It get the actually attempts perfomed by a user who is tryng to login to App.
        /// </summary>
        /// <returns>
        /// returns int representing the attempts.
        /// </returns>
        public int getAttemptsByMacAddress( string address ) {
            int attempsByAddress = -1;
            //using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
            //    using ( MySqlCommand command = connection.CreateCommand() ) {
            //        command.CommandText = "SELECT attempts FROM Host WHERE mac_address = @mac_adress";
            //        command.Parameters.Add( "mac_adress", MySqlDbType.VarChar );
            //        command.Parameters[0].Value = address;
            //        using ( MySqlDataReader reader = command.ExecuteReader() ) {
            //            if ( reader.Read() ) {
            //                attempsByAddress = reader.GetInt16(0);
            //            }
            //        }
            //    }
            //}
            return attempsByAddress;
        }

        // Set attempts to 0 in database
        /// <summary>
        /// It should be used to reset the attempts perfomed by a user.
        /// </summary>
        /// <returns>
        /// returns true if any row was affected otherwise false.
        /// </returns>
        public bool resetAttempts( string address ) {
            bool isAttemptsReset = false;
            //using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
            //    using ( MySqlCommand command = connection.CreateCommand() ) {
            //        command.CommandText = "UPDATE Host SET attempts = 0 WHERE mac_address = @mac_address";
            //        command.Parameters.Add( "mac_address", MySqlDbType.VarChar ).Value = address;
            //        int rowsAffected = command.ExecuteNonQuery();
            //        if ( rowsAffected > 0 ) {
            //            isAttemptsReset = true;
            //        }
            //    }
            //}
            return isAttemptsReset;
        }

        // Set an attempt to database
        /// <summary>
        /// Add an attempt to database to a specified macAddress.
        /// </summary>
        /// <returns>
        /// returns true if any row was affected otherwise false.
        /// </returns>
        public bool sendActualMacAddress( string address ) {
            bool isAddressSent = false;
            //try {
            //    using ( MySqlConnection mySqlConnection = databaseConnection.GetConnection() ) {
            //        using ( MySqlCommand callableCommand = new MySqlCommand( "sendAddress", mySqlConnection ) ) {
            //            callableCommand.CommandType = CommandType.StoredProcedure;
            //            callableCommand.Parameters.Add( "dir", MySqlDbType.VarChar ).Value = address;
            //            int result = callableCommand.ExecuteNonQuery();
            //            isAddressSent = ( result != 0 );
            //        }
            //    }
            //} catch ( MySqlException ) {
            //    throw;
            //}
            return isAddressSent;
        }
    }
}
