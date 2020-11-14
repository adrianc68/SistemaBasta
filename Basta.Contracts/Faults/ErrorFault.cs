using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basta.Contracts.Faults {
    [DataContract]
    [Serializable()]
    public class ErrorFault {
        private string message;

        public ErrorFault() {
        }

        public ErrorFault( string message ) {
            this.message = message;
        }

        [DataMember]
        public string Message { get { return message; } set { message = value; } }

    }
}
