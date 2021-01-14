using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain {
    [DataContract]
    public enum RoomState: int {
        [EnumMember]
        PUBLIC = 0,
        [EnumMember]
        PRIVATE = 1
    }
}
