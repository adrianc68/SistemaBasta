using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    [DataContract]
    [Serializable()]
    public class EmailNotRegisteredYetException: Exception {
        public EmailNotRegisteredYetException() { }

        public EmailNotRegisteredYetException( String message ) : base( message ) { }

        public EmailNotRegisteredYetException( string message, Exception innerException ) : base( message, innerException ) { }

    }
}
