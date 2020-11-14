using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    [DataContract]
    [Serializable()]
    class AccountAlreadyRegisteredException: Exception {
        public AccountAlreadyRegisteredException() { }

        public AccountAlreadyRegisteredException( string message ) : base( message ) { }

        public AccountAlreadyRegisteredException( string message, Exception innerException ) : base( message, innerException ) { }

    }

}
