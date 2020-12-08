using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basta.Contracts.Faults {
    [DataContract]
    public class UserKickedFromRoomFault {
        [DataMember]
        public string Message { get; set; }

        public UserKickedFromRoomFault( string message ) {
            Message = message;
        }
    }
}

