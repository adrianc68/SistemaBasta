using Domain.Domain;
using System.Collections.Generic;
using System.ServiceModel;

namespace Basta.Contracts {
    [ServiceContract( CallbackContract = typeof( IChatClient ) )]
    public interface IChatService {
        [OperationContract( IsOneWay = true )]
        void Connect( Player player );

        [OperationContract( IsOneWay = true )]
        void SendMessage( string message );

        [OperationContract( IsOneWay = false )]
        List<Player> GetConnectedUsers();
    }

    [ServiceContract]
    public interface IChatClient {
        [OperationContract( IsOneWay = true )]
        void ReciveMessage( Player player, string message );
    }
}
