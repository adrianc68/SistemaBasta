using Database.Entity;
using System;
using System.Linq;
using System.Security.Cryptography;
using Utils;

namespace Database.DAO {

    /*
        The AccessAccountDAO class
        Contains all methods for getting and setting data from database.
    */
    public class AccessAccountDAO {

        // Verify username in database
        /// <summary>
        /// It verify if username exist in database.
        /// </summary>
        /// <returns>
        /// returns true if username exist in database otherwise false.
        /// </returns>
        public bool verifyExistingUsername( string username ) {
            bool isUsernameRegistered = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var usernameQuery = database.AccessAccounts
                    .Where( b => b.username == username )
                    .FirstOrDefault();
                isUsernameRegistered = usernameQuery != null;
            }
            return isUsernameRegistered;
        }

        // Verify email in database
        /// <summary>
        /// It verify if email exist in database.
        /// </summary>
        /// <returns>
        /// returns true if email exist in database otherwise false.
        /// </returns>
        public bool verifyExistingEmail( string email ) {
            bool isEmailRegistered = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var emailQuery = database.AccessAccounts
                    .Where( b => b.email == email )
                    .FirstOrDefault();
                isEmailRegistered = emailQuery != null;
            }
            return isEmailRegistered;
        }

        // Update AccessAccount's password in database.
        /// <summary>
        /// It changes the specified account's password in database using an email and a password
        /// </summary>
        /// <returns>
        /// returns true if any row was affected otherwise false.
        /// </returns>
        public bool ChangePasswordByEmail( string email, string password ) {
            bool isPasswordChanged = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var account = database.AccessAccounts
                    .Where( s => s.email == email ).First();
                account.password = password;
                database.SaveChanges();
                isPasswordChanged = true;
            }
            return isPasswordChanged;
        }

        // Generate a random string with 8 characters from database.
        /// <summary>
        /// It updates ]the column recovery code generating a random string with 8 characters.

        /// </summary>
        /// <returns>
        /// returns the updated random string.
        /// </returns>
        public string GenerateRecoveryCodeByEmail( string email ) {
            string recoveryCode = null;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var account = database.AccessAccounts
                    .Where( s => s.email == email ).First();
                account.recovery_code = Cryptography.RandomString().Substring( 0, 8 );
                database.SaveChanges();
                recoveryCode = account.recovery_code;
            }
            return recoveryCode;
        }

        // Returns the Account's AccountState from database
        /// <summary>
        /// It returns a AccountState by a specified email from database.
        /// </summary>
        /// <returns>
        /// returns the Account's AccountState.
        /// </returns>
        public AccountState CheckAccountState( string email ) {
            AccountState state = AccountState.FREE;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var account = database.AccessAccounts
                    .Where( s => s.email == email ).First();
                if ( account != null ) {
                    state = account.account_state;
                }
            }
            return state;
        }
    }
}
