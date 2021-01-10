using Basta.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basta.Service {
    public class PlayerChannels {
        public IRoomClient IRoomClient { get; set; }
        public IGameClient IGameClient { get; set; }

    }
}
