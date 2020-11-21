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
        public int PlayerLimit { get; set; }
        public RoomState RoomState { get; set; }
        public string Code { get; set; }

        public Room Room { get; set; }

    }
}
