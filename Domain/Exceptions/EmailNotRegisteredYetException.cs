using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    public class EmailNotRegisteredYetException: Exception {
        public EmailNotRegisteredYetException( String message ) : base( message ) { }

        public EmailNotRegisteredYetException( string message, Exception innerException ) : base( message, innerException ) { }

    }
}
