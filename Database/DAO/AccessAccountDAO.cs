using Domain.Domain;
using MySql.Data.MySqlClient;
using System;

namespace Database.DAO {
    public class AccessAccountDAO: IAccessAccountDAO {
        private DatabaseConnection databaseConnection;

        public AccessAccountDAO() {
            databaseConnection = new DatabaseConnection();
        }

        public bool verifyExistingUsername( string username ) {
            bool isUsernameRegistered = false;
            using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
                using ( MySqlCommand command = connection.CreateCommand() ) {
                    command.CommandText = "SELECT username FROM AccessAccount WHERE username = @username;";
                    command.Parameters.Add( "username", MySqlDbType.VarChar );
                    command.Parameters[0].Value = username;
                    using ( MySqlDataReader reader = command.ExecuteReader() ) {
                        if ( reader.Read() ) {
                            isUsernameRegistered = true;
                        }
                    }
                }
            }
            return isUsernameRegistered;
        }

        public bool verifyExistingEmail( string email ) {
            bool isEmailRegistered = false;
            using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
                using ( MySqlCommand command = connection.CreateCommand() ) {
                    command.CommandText = "SELECT email FROM AccessAccount WHERE email = @email;";
                    command.Parameters.Add( "email", MySqlDbType.VarChar );
                    command.Parameters[0].Value = email;
                    using ( MySqlDataReader reader = command.ExecuteReader() ) {
                        if ( reader.Read() ) {
                            isEmailRegistered = true;
                        }
                    }
                }
            }
            return isEmailRegistered;
        }

        public bool ChangePasswordByEmail( string email, string password ) {
            bool isPasswordChanged = false;
            using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
                using ( MySqlCommand command = connection.CreateCommand() ) {
                    command.CommandText = "UPDATE AccessAccount SET password = @password WHERE email = @email";
                    command.Parameters.Add( "password", MySqlDbType.VarChar ).Value = password;
                    command.Parameters.Add( "email", MySqlDbType.VarChar ).Value = email;
                    int rowsAffected = command.ExecuteNonQuery();
                    if ( rowsAffected > 0 ) {
                        isPasswordChanged = true;
                    }
                }
            }
            return isPasswordChanged;
        }

        public string GenerateRecoveryCodeByEmail( string email ) {
            string recoveryCode;
            using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
                using ( MySqlCommand command = connection.CreateCommand() ) {
                    command.CommandText = "UPDATE AccessAccount SET recovery_code = SUBSTRING(MD5(RAND()),-8) WHERE email = @email";
                    command.Parameters.Add( "email", MySqlDbType.VarChar );
                    command.Parameters[0].Value = email;
                    command.ExecuteNonQuery();
                    recoveryCode = GetRecoveryCode( email );
                }
            }
            return recoveryCode;
        }

        private string GetRecoveryCode( string email ) {
            string code = null;
            using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
                using ( MySqlCommand command = connection.CreateCommand() ) {
                    command.CommandText = "SELECT recovery_code FROM AccessAccount WHERE email = @email ORDER BY email DESC LIMIT 1";
                    command.Parameters.Add( "email", MySqlDbType.VarChar );
                    command.Parameters[0].Value = email;
                    using ( MySqlDataReader reader = command.ExecuteReader() ) {
                        if ( reader.Read() ) {
                            code = reader.GetString( 0 );
                        }
                    }
                }
            }
            return code;
        }

        public AccountState CheckAccountState( string email ) {
            AccountState state = AccountState.NULL;
            using ( MySqlConnection connection = databaseConnection.GetConnection() ) {
                using ( MySqlCommand command = connection.CreateCommand() ) {
                    command.CommandText = "SELECT account_state FROM AccessAccount WHERE email = @email";
                    command.Parameters.Add( "email", MySqlDbType.VarChar );
                    command.Parameters[0].Value = email;
                    using ( MySqlDataReader reader = command.ExecuteReader() ) {
                        if ( reader.Read() ) {
                            state = (AccountState) Enum.Parse( typeof( AccountState ), reader.GetString( 0 ) );
                        }
                    }
                }
            }
            return state;
        }
    }
}
