using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Database.DAO {
    public class AccessAccountDAO : IAccessAccountDAO {
        private DatabaseConnection databaseConnection;

        public AccessAccountDAO() {
            databaseConnection = new DatabaseConnection();
        }

        public bool IAccessAccountDAO.ChangePasswordByEmail(string email, string password) {
            try {
                MySqlCommand updateCommand = new MySqlCommand("", databaseConnection.GetConnection() ) {
                    CommandText = "UPDATE AccessAccount SET password = @password WHERE email = @email"
                };
                updateCommand.Parameters.Add("@password", MySqlDbType.VarChar, 75).Value = email;
                 

            } catch (MySqlException e) {
                Console.WriteLine(e + "AccessAccountDAO ");
            }
            return false;
        }

        public string IAccessAccountDAO.GenerateRecoveryCodeByEmail(string email) {
            String codeGenerated = null;
            try {
                MySqlCommand updateCommand = new MySqlCommand("", databaseConnection.GetConnection())
                {
                    CommandText = "UPDATE AccessAccount SET recovery_code = SUBSTRING(MD5(RAND()),-8) WHERE email = @email ORDER BY id_user DESC LIMIT 1"
                };
                updateCommand.Parameters.Add("@email", MySqlDbType.VarChar, 75).Value = email;
                updateCommand.ExecuteNonQuery();
                codeGenerated = GetRecoveryCode(email);
            } catch (MySqlException e) {
                Console.WriteLine(e);
            }
            return codeGenerated;
        }

        private string GetRecoveryCode(String email) {
            String code = null;
            MySqlDataReader reader = null;
            try {
                MySqlCommand queryCommand = new MySqlCommand("", databaseConnection.GetConnection() ) {
                    CommandText = "SELECT recovery_code FROM AccessAccount WHERE email = @email ORDER BY email DESC LIMIT 1"
                };

                queryCommand.Parameters.Add("@email", MySqlDbType.VarChar, 75).Value = email;
                reader = queryCommand.ExecuteReader();
                code = reader.GetString(1);

            } catch (MySqlException) {
                throw;
            } finally {
                if( reader != null ) {
                    reader.Close();
                }
            }
            return code;
        }

    }
}
