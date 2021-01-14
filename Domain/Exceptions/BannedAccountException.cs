using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    [DataContract]
    [Serializable()]
    public class BannedAccountException: Exception {
        public BannedAccountException() { }

        public BannedAccountException( string message ) : base( message ) { }

        public BannedAccountException( string message, Exception innerException ) : base( message, innerException ) { }
    }
}
