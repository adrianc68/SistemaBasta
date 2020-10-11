using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAO
{
    interface IAccessAccountDAO
    {
        String GenerateRecoveryCodeByEmail(String email);
        bool ChangePasswordByEmail(String email, String password);

    }
}
