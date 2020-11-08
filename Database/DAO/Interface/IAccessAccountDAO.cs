using Domain.Domain;
using System;

namespace Database.DAO {
    interface IAccessAccountDAO {
        string GenerateRecoveryCodeByEmail( string email );

        bool ChangePasswordByEmail( string email, string password );

        bool verifyExistingUsername( string username );

        bool verifyExistingEmail( string email );

        AccountState CheckAccountState( string email );

    }
}

