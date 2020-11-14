using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    [DataContract]
    [Serializable()]
    public class LimitReachedException: Exception {
        public LimitReachedException() { }

        public LimitReachedException( string message ) : base( message ) { }

        public LimitReachedException( string message, Exception innerException ) : base( message, innerException ) { }

    }
}
