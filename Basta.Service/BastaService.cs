using Basta.Contracts;
using Basta.Contracts.Faults;
using Database.DAO;
using Domain.Domain;
using EmailSender;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Basta.Service {
    [ServiceBehavior( ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single )]
    public class BastaService: ILoginService, IRoomService {
        Dictionary<Room, Dictionary<IRoomClient, Player>> usersRoom = new Dictionary<Room, Dictionary<IRoomClient, Player>>( new Room.EqualityComparer() );
        HashSet<Player> playersConnected = new HashSet<Player>( new Player.EqualityComparer() );
        Dictionary<string, List<string>> userKickedFromRoom = new Dictionary<string, List<string>>();



        /*
        * 
        * 
        * 
        * IRoomService implementation.
        * 
        * 
        */
        public void GetRooms() {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            List<Room> rooms = new RoomDAO().GetRoomAvailable();
            connection.ReciveRoomsAvailable( rooms );
        }

        public void SetUpRoom() {
            throw new NotImplementedException();
        }

        public string CreateRoom( Player player, Room room ) {
            string roomCode = null;
            try {
                roomCode = new RoomDAO().CreateRoom( room );
                room.Code = roomCode;
                room.RoomConfiguration.Code = room.Code;
            } catch ( FaultException ) {
                throw;
            }
            if ( roomCode != null ) {
                var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
                Dictionary<IRoomClient, Player> users = new Dictionary<IRoomClient, Player>();
                users.Add( connection, player );
                usersRoom[room] = users;
            }
            return roomCode;
        }

        public void JoinRoom( Player player, Room room ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            if ( !isPlayerKickedFromRoom( player, room ) ) {


                if ( usersRoom[room].Count != room.RoomConfiguration.PlayerLimit ) {
                    List<Player> connectedUsers = GetConnectedUsersFromRoom( room );
                    usersRoom[room][connection] = player;
                    foreach ( var other in usersRoom[room].Keys ) {
                        if ( other == connection ) {
                            other.PlayerHasJoinToRoom( connectedUsers, room );
                        } else {
                            other.PlayerConnected( player );
                        }
                    }


                } else {
                    connection.GameIsFull();
                }
            } else {
                connection.PlayersIsKicked();
            }
        }

        public void DeleteRoom( Room room ) {
            if ( usersRoom.ContainsKey( room ) ) {
                if ( new RoomDAO().DeleteRoom( room ) ) {
                    foreach ( var other in usersRoom[room].Keys ) {
                        other.RoomDelected( room );
                    }
                    //BUG HERE 
                    //usersRoom.Remove( room );
                }
            }
        }

        public void UserDisconnectedFromRoom( Player player, Room room ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            foreach ( var other in usersRoom[room].Keys ) {
                other.PlayerDisconnected( player );
            }
            usersRoom[room].Remove( connection );
        }

        public void KickPlayer( Player player, Room room ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            foreach ( var other in usersRoom[room] ) {
                if ( other.Key == connection ) {
                    other.Key.PlayerKicked();
                    if ( !userKickedFromRoom.ContainsKey( room.Code ) ) {
                        userKickedFromRoom.Add( room.Code, new List<String>() );
                    }
                    userKickedFromRoom[room.Code].Add( player.Email );
                } else {
                    other.Key.PlayerDisconnected( player );
                }
            }

            usersRoom[room].Remove( connection );


            GetConnectedUsers().ForEach( e => Console.WriteLine( e.Name ) );

            foreach ( var a in usersRoom ) {
                Console.WriteLine( "Salas: " + a.Key );
                foreach ( var b in usersRoom[a.Key] ) {
                    Console.WriteLine( "Connection: " + b.Key );
                    Console.WriteLine( "Jugador: " + b.Value.Email );
                }
            }

            foreach ( var a in userKickedFromRoom ) {
                Console.WriteLine( "Sala: " + a.Key );
            }



        }

        public void SendMessageRoomChat( Room room, string message ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            Player player;
            if ( !usersRoom[room].TryGetValue( connection, out player ) )
                return;
            foreach ( var other in usersRoom[room] ) {
                Console.WriteLine( "Mensaje enviado a : " + other.Value.Email );
                //if ( other == connection )
                //    continue;
                other.Key.ReciveMessageRoom( player, message );
            }

        }

        public void SendMessageRoomChatToPlayer( Room room, string message, Player toPlayer ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            Player player;
            if ( !usersRoom[room].TryGetValue( connection, out player ) ) {
                return;
            }
            foreach ( var other in usersRoom[room] ) {
                if ( other.Value.Email == toPlayer.Email && player.Email != toPlayer.Email ) {
                    other.Key.ReciveMessageFromPlayer( player, message );
                    break;
                }
            }
        }

        public List<Player> GetConnectedUsers() {
            throw new NotImplementedException();
        }

        /*
         * 
         * 
         * 
         * ILoginService implement.
         * 
         * 
         * 
         * 
         */

        public bool BanPlayer( Player player ) {
            throw new NotImplementedException();
        }

        public bool SignUp( Player player ) {
            bool accountRegistered = false;
            PlayerDAO playerDAO = new PlayerDAO();
            AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
            if ( accessAccountDAO.verifyExistingEmail( player.AccessAccount.Email ) ) {
                throw new FaultException<EmailAlreadyRegisteredFault>( new EmailAlreadyRegisteredFault() { Message = "Email is already registered!" }, "email" );
            } else if ( accessAccountDAO.verifyExistingUsername( player.AccessAccount.Username ) ) {
                throw new FaultException<UsernameRegisteredAlreadyFault>( new UsernameRegisteredAlreadyFault() { Message = "Username is already used!" }, "USERNAME" );
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
                if ( playersConnected.Contains( player ) ) {
                    throw new FaultException<AccountAlreadyLoggedFault>( new AccountAlreadyLoggedFault { Message = "Account already logged!" }, "Account logged" );
                }
                playersConnected.Add( player );
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

        void ILoginService.LogOut( Player player ) {
            playersConnected.Remove( player );
        }

        public string GenerateCode( string email ) {
            string code;
            AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
            if ( accessAccountDAO.verifyExistingEmail( email ) ) {
                code = accessAccountDAO.GenerateRecoveryCodeByEmail( email );
            } else {
                throw new FaultException<EmailNotRegisteredYetFault>( new EmailNotRegisteredYetFault() { Message = "Username is already used!" }, "Email no registered yet" );
            }
            return code;
        }

        public bool SendMessageByEmail( string email, string content ) {
            bool isMessageSent = false;
            try {
                Email.SendMessage( email, content );
                isMessageSent = true;
            } catch ( Exception ex ) {
                throw new FaultException<EmailSenderFault>( new EmailSenderFault() { Message = "Email error!" }, ex.Message );
            }
            return isMessageSent;
        }


        private bool isPlayerKickedFromRoom( Player player, Room room ) {
            bool isKicked = false;
            if ( userKickedFromRoom.ContainsKey( room.Code ) ) {
                foreach ( var playerInList in userKickedFromRoom[room.Code] ) {
                    if ( playerInList.Equals( player.Email ) ) {
                        isKicked = true;
                        break;
                    }
                }
            }
            return isKicked;
        }

        private List<Player> GetConnectedUsersFromRoom( Room room ) {
            List<Player> list = new List<Player>();
            foreach ( var dictionary in usersRoom[room] ) {
                list.Add( dictionary.Value );
            }
            return list;
        }


    }


}
