using System.Runtime.Serialization;

namespace Domain.Domain {
    [DataContract]
    public enum AccountState: int {
        [EnumMember]
        FREE = 0,
        [EnumMember]
        BANNED = 1
    }
}
