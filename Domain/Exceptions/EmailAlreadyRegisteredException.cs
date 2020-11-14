using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    [DataContract]
    [Serializable()]
    public class EmailAlreadyRegisteredException: Exception {
        public EmailAlreadyRegisteredException() { }

        public EmailAlreadyRegisteredException( string message ) : base( message ) { }

        public EmailAlreadyRegisteredException( string message, Exception innerException ) : base( message, innerException ) { }

    }
}
