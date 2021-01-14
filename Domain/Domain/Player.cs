using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Domain {
    [DataContract]
    public class Player {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public short Age { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public Location Location { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Score { get; set; }
        [DataMember]
        public AccessAccount AccessAccount { get; set; }

        public class EqualityComparer: IEqualityComparer<Player> {

            public bool Equals( Player x, Player y ) {
                return x.Email == y.Email;
            }

            public int GetHashCode( Player obj ) {
                string combined = obj.Email;
                return combined.GetHashCode();
            }
        }


    }

}
