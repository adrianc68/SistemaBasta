using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    class AccountAlreadyRegisteredException : Exception {
        public AccountAlreadyRegisteredException() : base("La cuenta ya está registrada") {

        }
    }

}
