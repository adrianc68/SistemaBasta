using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace Database {
    public class DatabaseConnection {
        private MySqlConnection connection;

        public MySqlConnection GetConnection() {
            ConnectToDatabase();
            return connection;
        }

        public void CloseConnection() {
            if ( connection != null ) {
                try {
                    if ( connection.State == ConnectionState.Open ) {
                        connection.Close();
                    }
                } catch ( MySqlException ) {
                    throw;
                }
            }
        }

        private void ConnectToDatabase() {
            connection = null;
            try {
                connection = new MySqlConnection( GetConnectionString() );
                connection.Open();
            } catch ( MySqlException ) {
                throw;
            }
        }

        private String GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

    }
}
