using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    public class AccessAccountNotFoundException: Exception {
        public AccessAccountNotFoundException( string message) : base( message ) { }
        public AccessAccountNotFoundException( string message, Exception innerException ) : base( message, innerException ) { }
    }
}
