using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain {
    public class AccessAccount {
        private string email;
        private string username;
        private string password;
        private AccountState accountState;

        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public AccountState AccountState { get => accountState; set => accountState = value ; }

    }
}
