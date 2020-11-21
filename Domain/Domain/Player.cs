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

        public AccessAccount AccessAccount { get; set; }

    }

}
