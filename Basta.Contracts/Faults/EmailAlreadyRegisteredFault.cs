using System;
using System.Runtime.Serialization;

namespace Basta.Contracts.Faults {
    [DataContract]
    [Serializable()]
    public class EmailAlreadyRegisteredFault {
        private string message;

        public EmailAlreadyRegisteredFault() {
        }

        public EmailAlreadyRegisteredFault( string message ) {
            this.message = message;
        }


        [DataMember]
        public string Message { get { return message; } set { message = value; } }

    }

}
