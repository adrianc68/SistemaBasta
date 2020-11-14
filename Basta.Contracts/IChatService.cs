using Database.Entity;
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
        void SendMessage( Domain.Domain.Message message );

        [OperationContract( IsOneWay = false )]
        List<Player> GetConnectedUsers();
    }

    [ServiceContract]
    public interface IChatClient {
        [OperationContract( IsOneWay = true )]
        void ReciveMessage( Player player, Domain.Domain.Message message );
    }
}
