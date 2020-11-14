using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    [DataContract]
    [Serializable()]
    public class UsernameRegisteredAlreadyException: Exception {
        public UsernameRegisteredAlreadyException() { }

        public UsernameRegisteredAlreadyException( string message ) : base( message ) { }

        public UsernameRegisteredAlreadyException( string message, Exception innerException ) : base( message, innerException ) { }
    }
}
