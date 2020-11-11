using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Basta.Contracts {
    [ServiceContract( CallbackContract = typeof( IChatClient ) )]
    public interface IChatService {
        [OperationContract( IsOneWay = true )]
        void Connect( Player player );

        [OperationContract( IsOneWay = true )]
        void SendMessage( Message message );

        [OperationContract( IsOneWay = false )]
        List<Player> GetConnectedUsers();
    }

    [ServiceContract]
    public interface IChatClient {
        [OperationContract( IsOneWay = true )]
        void ReciveMessage( Player player, Message message );
    }
}
