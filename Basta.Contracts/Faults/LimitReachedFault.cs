using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basta.Contracts.Faults {
    [DataContract]
    [Serializable()]
    public class LimitReachedFault {
        private string message;

        public LimitReachedFault() {
        }

        public LimitReachedFault( string message ) {
            this.message = message;
        }

        [DataMember]
        public string Message { get { return message; } set { message = value; } }
    }
}
