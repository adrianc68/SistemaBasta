using Basta.Contracts.Faults;
using Domain.Domain;
using System.ServiceModel;

namespace Basta.Contracts {
    [ServiceContract]
    public interface ILoginService {
        [OperationContract]
        [FaultContract( typeof( AccessAccountNotFoundFault ) )]
        [FaultContract( typeof( BannedAccountFault ) )]
        [FaultContract( typeof( LimitReachedFault ) )]
        [FaultContract( typeof( AccountAlreadyLoggedFault ) )]
        Player Login( string macAddress, string email, string password );

        [OperationContract]
        [FaultContract( typeof( EmailAlreadyRegisteredFault ) )]
        [FaultContract( typeof( UsernameRegisteredAlreadyFault ) )]
        bool SignUp( Player player );

        [OperationContract]
        bool ChangePassword( string email, string password );

        [OperationContract]
        [FaultContract( typeof( EmailNotRegisteredYetFault ) )]
        string GenerateCode( string email );

        [OperationContract]
        [FaultContract( typeof( EmailSenderFault ) )]
        bool SendMessageByEmail( string email, string content );

        [OperationContract]
        void LogOut( Player player );

        [OperationContract]
        bool BanPlayer(Player player);


    }

}
