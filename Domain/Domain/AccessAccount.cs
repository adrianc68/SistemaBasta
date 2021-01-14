using System.Runtime.Serialization;

namespace Domain.Domain {
    [DataContract]
    public class AccessAccount {
        [DataMember]

        public string Email { get; set; }
        [DataMember]

        public string Username { get; set; }
        [DataMember]

        public string Password { get; set; }
        [DataMember]

        public string Recovery_code { get; set; }
        [DataMember]

        public AccountState Account_state { get; set; }

        public Player Player { get; set; }

    }
}
