using Basta.Contracts;
using Basta.Contracts.Faults;
using Database.DAO;
using Domain.Domain;
using EmailSender;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Basta.Service {
    [ServiceBehavior( ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single )]
    public class BastaService: IChatService, ILoginService, IRoomService {


        Dictionary<IChatClient, Player> usersChat = new Dictionary<IChatClient, Player>();

        Dictionary<Room, Dictionary<IRoomClient, Player>> usersRoom = new Dictionary<Room, Dictionary<IRoomClient, Player>>();
        //Dictionary<string, Dictionary<IRoomClient, Player>> usersRoom = new Dictionary<string, Dictionary<IRoomClient, Player>>;


        /*
        * IRoomService implement.
        */
        public void CreateRoom( Player player, Room room ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            Dictionary<IRoomClient, Player> users = new Dictionary<IRoomClient, Player>();
            users[connection] = player;
            usersRoom[room] = users;
            Console.WriteLine( $"{room.Code} room created." );
        }

        public void JoinRoom( Player player, Room room ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            usersRoom[room][connection] = player;
        }

        public void DeleteRoom() {
            throw new NotImplementedException();
        }

        public void SetUpRoom() {
            throw new NotImplementedException();
        }

        public void ConnectToRoom( Player player ) {
            throw new NotImplementedException();
        }

        public void SendMessageRoomChat( string message ) {
            throw new NotImplementedException();
        }

        public List<Player> GetConnectedUsersFromRoom( string ccodeRoom ) {
            throw new NotImplementedException();
        }

        /*
         * IChatService implement.
         */

        public void Connect( Player player ) {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            usersChat[connection] = player;
        }

        public void SendMessage( string message ) {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            Player player;
            if ( !usersChat.TryGetValue( connection, out player ) )
                return;
            foreach ( var other in usersChat.Keys ) {
                if ( other == connection )
                    continue;
                other.ReciveMessage( player, message );
            }
        }

        public List<Player> GetConnectedUsers() {
            throw new NotImplementedException();
        }
        /*
         * ILoginService implement.
         */

        public bool SignUp( Player player ) {
            bool accountRegistered = false;
            PlayerDAO playerDAO = new PlayerDAO();
            AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
            if ( accessAccountDAO.verifyExistingEmail( player.AccessAccount.Email ) ) {
                EmailAlreadyRegisteredFault emailAlreadyRegisteredFault = new EmailAlreadyRegisteredFault();
                emailAlreadyRegisteredFault.Message = "Email is already registered";
                throw new FaultException<EmailAlreadyRegisteredFault>( emailAlreadyRegisteredFault, "email" );
            } else if ( accessAccountDAO.verifyExistingUsername( player.AccessAccount.Username ) ) {
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
