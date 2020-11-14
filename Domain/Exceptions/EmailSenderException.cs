using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    [DataContract]
    [Serializable()]
    public class EmailSenderException: Exception {
        public EmailSenderException() { }

        public EmailSenderException( string message ) : base( message ) { }

        public EmailSenderException( string message, Exception innerException ) : base( message, innerException ) { }
    }
}
