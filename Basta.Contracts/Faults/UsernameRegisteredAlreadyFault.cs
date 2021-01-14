using System;
using System.Runtime.Serialization;

namespace Basta.Contracts.Faults {
    [DataContract]
    [Serializable()]
    public class UsernameRegisteredAlreadyFault {
        private string message;

        public UsernameRegisteredAlreadyFault() {
        }

        public UsernameRegisteredAlreadyFault( string message ) {
            this.message = message;
        }

        [DataMember]
        public string Message { get { return message; } set { message = value; } }
    }
}
