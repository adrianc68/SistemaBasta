using Basta.Contracts;
using Basta.Contracts.Faults;
using Database.DAO;
using Database.Entity;
using EmailSender;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Basta.Service {
    [ServiceBehavior( ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single )]
    public class BastaService: IChatService, ILoginService {
        Dictionary<IChatClient, Player> users = new Dictionary<IChatClient, Player>();

        /*
         * IChatService implement.
         */

        public void Connect( Player player ) {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            users[connection] = player;
        }

        public List<Player> GetConnectedUsers() {
            throw new NotImplementedException();
        }

        public void SendMessage( Domain.Domain.Message message ) {
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

        /*
         * ILoginService implement.
         */

        public bool SignUp( Player player ) {
            bool accountRegistered = false;
            PlayerDAO playerDAO = new PlayerDAO();
            AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
            if ( accessAccountDAO.verifyExistingEmail( player.AccessAccount.email ) ) {
                EmailAlreadyRegisteredFault emailAlreadyRegisteredFault = new EmailAlreadyRegisteredFault();
                emailAlreadyRegisteredFault.Message = "Email is already registered";
                throw new FaultException<EmailAlreadyRegisteredFault>( emailAlreadyRegisteredFault, "email" );
            } else if ( accessAccountDAO.verifyExistingUsername( player.AccessAccount.username ) ) {
                UsernameRegisteredAlreadyFault usernameRegisteredAlreadyFault = new UsernameRegisteredAlreadyFault();
                usernameRegisteredAlreadyFault.Message = "Username is already used!";
                throw new FaultException<UsernameRegisteredAlreadyFault>( usernameRegisteredAlreadyFault, "USERNAME" );
            } else {
                playerDAO.AddPlayerAccount( player );
                accountRegistered = true;
            }
            return accountRegistered;
        }

        public bool ChangePassword( string email, string password ) {
            bool isPasswordChanged = false;
            AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
            isPasswordChanged = accessAccountDAO.ChangePasswordByEmail( email, password );
            return isPasswordChanged;
        }

        public string RecoverPassword( string email, string password ) {
            throw new NotImplementedException();
        }

        Player ILoginService.Login( string macAddress, string email, string password ) {
            Player player = null;
            try {
                Autentication autentication = new Autentication();
                if ( autentication.LogIn( email, password, macAddress ) ) {
                    player = autentication.Player;
                }
            } catch ( FaultException<AccessAccountNotFoundFault> aac ) {
                throw aac;
            } catch ( FaultException<BannedAccountFault> bac ) {
                throw bac;
            } catch ( FaultException<LimitReachedFault> lre ) {
                throw lre;
            } catch ( FaultException ) {
                throw;
            }
            return player;
        }

        public string GenerateCode( string email ) {
            string code;
            AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
            if ( accessAccountDAO.verifyExistingEmail( email ) ) {
                code = accessAccountDAO.GenerateRecoveryCodeByEmail( email );
            } else {
                EmailNotRegisteredYetFault emailNotRegisteredFault = new EmailNotRegisteredYetFault();
                emailNotRegisteredFault.Message = "Username is already used!";
                throw new FaultException<EmailNotRegisteredYetFault>( emailNotRegisteredFault, "Email no registered yet" );
            }
            return code;
        }

        public bool SendMessageByEmail( string email, string content ) {
            bool isMessageSent = false;
            try {
                Email.SendMessage( email, content );
                isMessageSent = true;
            } catch ( Exception ex ) {
                EmailSenderFault emailFault = new EmailSenderFault();
                emailFault.Message = "Email error";
                throw new FaultException<EmailSenderFault>( emailFault, ex.Message );
            }
            return isMessageSent;
        }
    }
}
