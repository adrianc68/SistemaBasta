using Basta.Contracts;
using Basta.Contracts.Faults;
using Database.DAO;
using Domain.Domain;
using EmailSender;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows.Threading;

namespace Basta.Service {
    [ServiceBehavior( ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false )]
    public class BastaService: ILoginService, IRoomService, IGameService {
        Dictionary<Room, Dictionary<Player, PlayerChannels>> usersRoom = new Dictionary<Room, Dictionary<Player, PlayerChannels>>( new Room.EqualityComparer() );
        HashSet<Player> playersConnected = new HashSet<Player>( new Player.EqualityComparer() );
        Dictionary<string, List<string>> userKickedFromRoom = new Dictionary<string, List<string>>();
        Dictionary<Room, GameProperties> roomsInGame = new Dictionary<Room, GameProperties>( new Room.EqualityComparer() );

        /*
        * 
        * 
        * 
        * IRoomService implementation.
        * 
        * 
        */

        public List<Room> GetRooms() {
            List<Room> rooms = null;
            try {
                rooms = new RoomDAO().GetRoomAvailable();
            } catch ( FaultException ) {
                throw;
            }
            return rooms;
        }

        public Room GetRoomByCode( string code ) {
            Room room = null;
            try {
                room = new RoomDAO().GetRoomByCode( code );
            } catch ( FaultException ) {
                throw;
            }
            return room;
        }

        public void JoinRoom( Player player, Room room ) {
            PlayerChannels playerChannels = new PlayerChannels();
            playerChannels.IRoomClient = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            var connection = playerChannels.IRoomClient;

            if ( !isPlayerKickedFromRoom( player, room ) ) {
                if ( usersRoom[room].Count != room.RoomConfiguration.PlayerLimit ) {
                    usersRoom[room][player] = playerChannels;
                    foreach ( var other in usersRoom[room].Values ) {
                        if ( other.IRoomClient == connection )
                            connection.Join();
                        other.IRoomClient.PlayerConnected( player );
                    }
                } else {
                    connection.GameIsFull();
                }
            } else {
                connection.PlayerKicked();
            }
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
                Dictionary<Player, PlayerChannels> users = new Dictionary<Player, PlayerChannels>( new Player.EqualityComparer() );
                PlayerChannels playerChannels = new PlayerChannels();
                playerChannels.IRoomClient = OperationContext.Current.GetCallbackChannel<IRoomClient>();
                users[player] = playerChannels;
                usersRoom[room] = users;
            }
            return roomCode;
        }

        public void SendMessageRoomChat( Player player, Room room, string message ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            foreach ( var other in usersRoom[room].Values ) {
                if ( other.IRoomClient == connection )
                    continue;
                other.IRoomClient.ReciveMessageRoom( player, message );
            }
        }

        public void SetUpRoom() {
            throw new NotImplementedException();
        }

        public void DeleteRoom( Room room ) {
            if ( usersRoom.ContainsKey( room ) ) {
                if ( new RoomDAO().DeleteRoom( room ) ) {
                    Console.WriteLine( "Room Eliminated: " + room.Code );
                    foreach ( var other in usersRoom[room].Values ) {
                        other.IRoomClient.RoomDelected( room );
                    }
                    //BUG HERE 
                    //usersRoom.Remove( room );
                }
            }
        }

        public void UserDisconnectedFromRoom( Player player, Room room ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            foreach ( var other in usersRoom[room].Values ) {
                if ( connection == other.IRoomClient )
                    connection.YouHaveDisconnected();
                other.IRoomClient.PlayerDisconnected( player );
            }

            usersRoom[room].Remove( player );


            //foreach ( var playern in usersRoom[room].Keys ) {
            //    if ( playern.Email == player.Email ) {
            //        usersRoom[room].Remove( playern );
            //        break;
            //    }
            //}

        }

        public List<Player> GetConnectedUsersFromRoom( Room room ) {
            var connection = OperationContext.Current.GetCallbackChannel<IRoomClient>();
            List<Player> usersConnectedToSpecifiedRoom = new List<Player>();
            foreach ( var dictionary in usersRoom[room] ) {
                if ( connection == dictionary.Value.IRoomClient )
                    continue;
                usersConnectedToSpecifiedRoom.Add( dictionary.Key );
            }
            return usersConnectedToSpecifiedRoom;
        }

        public void SendMessageRoomChatToPlayer( Player player, Room room, string message, Player toPlayer ) {
            foreach ( var other in usersRoom[room] ) {
                if ( other.Key.Email == toPlayer.Email && player.Email != toPlayer.Email ) {
                    other.Value.IRoomClient.ReciveMessageFromPlayer( player, message );
                    break;
                }
            }
        }

        public void KickPlayer( Player player, Room room ) {
            foreach ( var other in usersRoom[room] ) {
                if ( other.Key.Email == player.Email ) {
                    other.Value.IRoomClient.PlayerKicked();
                    if ( !userKickedFromRoom.ContainsKey( room.Code ) ) {
                        userKickedFromRoom.Add( room.Code, new List<String>() );
                    }
                    userKickedFromRoom[room.Code].Add( player.Email );
                } else {
                    other.Value.IRoomClient.PlayerDisconnected( player );
                }
            }
            foreach ( var other in usersRoom[room] ) {
                if ( other.Key.Email == player.Email ) {
                    usersRoom[room].Remove( other.Key );
                    break;
                }
            }
        }

        public void StartGamePressed( Room room, GameConfiguration gameConfiguration ) {
            gameConfiguration.ActualLetter = AssignRandomLetterToGameConfiguration();
            GameProperties gameProperties = new GameProperties();
            gameProperties.GameConfiguration = gameConfiguration;
            roomsInGame.Add( room, gameProperties );
            foreach ( var other in usersRoom[room].Values ) {
                other.IRoomClient.OpenGameWindow( gameConfiguration );
            }
        }

        private char AssignRandomLetterToGameConfiguration() {
            char randomLetter;
            char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'W', 'X', 'Y', 'Z' };
            Random random = new Random();
            int randomIndex = random.Next( 0, letters.Length );
            randomLetter = letters[randomIndex];
            return randomLetter;
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

        /*
        * 
        * 
        * 
        * IGameService implementation.
        * 
        * 
        */
        public void StopButtonPressed( Room room, Player player ) {
            foreach ( var other in usersRoom[room].Values ) {
                other.IGameClient.PlayerHasPressedStopButton( player );
            }
        }

        public void OpenChannel( Room room, Player player ) {
            var connection = OperationContext.Current.GetCallbackChannel<IGameClient>();
            Console.WriteLine( "Hello" );

            usersRoom[room][player].IGameClient = connection;

            //Console.WriteLine( "Hola de nuevo " );
            ////Console.WriteLine( "Existe usuario: " + usersRoom[room][player].IGameClient != null );
            connection.GameHasStarted();
            //roomsInGame[room].StartRound(connection);
        }

        public void RemoveChannel( Room room, Player player ) {
            throw new NotImplementedException();
        }

        //private DispatcherTimer gameTimer;
        //private TimeSpan gameTime;

        //private static TimeSpan time;
        //private static DispatcherTimer timer;

        //private void StartRoundRoom(Room room) {

        //}

        //private void ShowWinnerRoom() {

        //}

        //private void ShowResultsRoom() {

        //}



    }

}
