using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain {
    public class Player {
        private int age;
        private string name;
        private string country;
        private AccessAccount accessAccount;

        public int Age { get => age; set => age = value; }
        public string Name { get => name; set => name = value; }
        public string Country { get => country; set => country = value; }
     
        public AccessAccount AccessAccount { get => accessAccount; set => accessAccount = value; }

    }

}
