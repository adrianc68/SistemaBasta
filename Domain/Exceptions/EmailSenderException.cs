using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    public class EmailSenderException: Exception {
        public EmailSenderException( string message ) : base( message ) { }
        public EmailSenderException( string message, Exception innerException ) : base( message, innerException ) { }
    }
}
