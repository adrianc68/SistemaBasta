using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Domain {
    [DataContract]
    public class Room {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public RoomConfiguration RoomConfiguration { get; set; }

        public class EqualityComparer: IEqualityComparer<Room> {

            public bool Equals( Room x, Room y ) {
                return x.Code == y.Code;
            }

            public int GetHashCode( Room obj ) {
                //return 0;
                string combined = obj.Code;
                return combined.GetHashCode();
            }
        }

    }
}
