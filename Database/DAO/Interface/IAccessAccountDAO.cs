using Domain.Domain;
using System;

namespace Database.DAO {
    interface IAccessAccountDAO {
        // Generate a random string with 8 characters from database.
        /// <summary>
        /// It updates the column recovery code generating a random string with 8 characters.
        /// </summary>
        /// <returns>
        /// returns the updated random string.
        /// </returns>
        string GenerateRecoveryCodeByEmail( string email );
        // Update AccessAccount's password in database.
        /// <summary>
        /// It changes the specified account's password in database using an email and a password
        /// </summary>
        /// <returns>
        /// returns true if any row was affected otherwise false.
        /// </returns>
        bool ChangePasswordByEmail( string email, string password );
        // Verify username in database
        /// <summary>
        /// It verify if username exist in database.
        /// </summary>
        /// <returns>
        /// returns true if username exist in database otherwise false.
        /// </returns>
        bool verifyExistingUsername( string username );
        // Verify email in database
        /// <summary>
        /// It verify if email exist in database.
        /// </summary>
        /// <returns>
        /// returns true if email exist in database otherwise false.
        /// </returns>
        bool verifyExistingEmail( string email );
        // Returns the Account's AccountState from database
        /// <summary>
        /// It returns a AccountState by a specified email from database.
        /// </summary>
        /// <returns>
        /// returns the Account's AccountState.
        /// </returns>
        AccountState CheckAccountState( string email );

    }
}

