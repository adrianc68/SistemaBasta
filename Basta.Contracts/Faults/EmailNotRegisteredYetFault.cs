using System;
using System.Runtime.Serialization;

namespace Basta.Contracts.Faults {
    [DataContract]
    [Serializable()]
    public class EmailNotRegisteredYetFault {
        private string message;

        public EmailNotRegisteredYetFault() {
        }

        public EmailNotRegisteredYetFault( string message ) {
            this.message = message;
        }

        [DataMember]
        public string Message { get { return message; } set { message = value; } }

    }
}
