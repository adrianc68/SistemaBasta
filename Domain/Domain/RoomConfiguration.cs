using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain {
    [DataContract]
    public class RoomConfiguration {
        [DataMember]
        public int PlayerLimit { get; set; }
        [DataMember]
        public RoomState RoomState { get; set; }
        [DataMember]
        public string Code { get; set; }
        public Room Room { get; set; }

    }
}
