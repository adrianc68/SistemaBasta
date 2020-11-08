using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions {
    public class LimitReachedException: Exception {

        public LimitReachedException( string message ) : base( message ) { }
        public LimitReachedException( string message, Exception innerException ) : base( message, innerException ) { }

    }
}
