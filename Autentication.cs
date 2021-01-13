using Basta.Contracts.Faults;
using Domain.Domain;
using System;
using System.ServiceModel;
using Utils;

namespace Basta {
    public class Autentication {
        private static Autentication instance;

        public Proxy.LoginServiceClient LoginServer { get; set; }

        public Proxy.RoomServiceClient RoomServer { get; set; }

        public Proxy.GameServiceClient GameServer { get; set; }

        public RoomServiceCallBack RoomServiceCallBack { get; set; }

        public GameServiceCallBack GameServiceCallBack { get; set; }

        public Player Player { get; set; }

        public static Autentication GetInstance() {
            if ( instance == null ) {
                instance = new Autentication();
            }
            return instance;
        }

        public void LogOut() {
            Player = null;
        }

        public bool LogIn( string email, string password ) {
            bool isLogged = false;
            try {
                LoginServer = new Proxy.LoginServiceClient();
                Player = LoginServer.Login( Cryptography.SHA256_Hash( NetworkAddress.GetMacAddress() ), email, Cryptography.SHA256_Hash( password ) );
                isLogged = true;
            } catch ( FaultException<AccessAccountNotFoundFault> aac ) {
                throw aac;
            } catch ( FaultException<BannedAccountFault> bac ) {
                throw bac;
            } catch ( FaultException<LimitReachedFault> lre ) {
                throw lre;
            } catch ( FaultException<AccountAlreadyLoggedFault> acg ) {
                throw acg;
            } catch ( FaultException ) {
                throw;
            } catch ( EndpointNotFoundException ) {
                throw;
            }
            return isLogged;
        }
    }

}
