using System.Runtime.Serialization;


namespace Domain.Domain {
    [DataContract]
    public enum Location: int {
        [EnumMember]
        NORTH = 0,
        [EnumMember]
        SOUTH = 1,
        [EnumMember]
        EUROPE = 2
    }
}
