using Basta.Contracts;
using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Basta.Service {
    [ServiceBehavior( ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single )]
    public class BastaService: IChatService {
        Dictionary<IChatClient, Player> users = new Dictionary<IChatClient, Player>();

        public void Connect( Player player ) {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            users[connection] = player;
        }

        public List<Player> GetConnectedUsers() {
            throw new NotImplementedException();
        }

        public void SendMessage( Message message ) {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            Player player;
            if ( !users.TryGetValue( connection, out player ) )
                return;
            foreach ( var other in users.Keys ) {
                if ( other == connection )
                    continue;
                other.ReciveMessage( player, message );
            }
        }
    }
}
