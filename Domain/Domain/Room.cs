using System.Runtime.Serialization;

namespace Domain.Domain {
    [DataContract]
    public class Room {
        public string Code { get; set; }
        public RoomConfiguration RoomConfiguration { get; set; }


    }
}
