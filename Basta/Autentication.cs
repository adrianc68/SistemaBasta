using Basta.Contracts.Faults;
using Domain.Domain;
using System;
using System.ServiceModel;
using Utils;

namespace Basta {
    public class Autentication {
        private static Autentication instance;

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
                Proxy.LoginServiceClient server = new Proxy.LoginServiceClient();
                Player = server.Login( Cryptography.SHA256_Hash( NetworkAddress.GetMacAddress() ), email, Cryptography.SHA256_Hash( password ) );
                isLogged = true;
            } catch ( FaultException<AccessAccountNotFoundFault> aac ) {
                throw aac;
            } catch ( FaultException<BannedAccountFault> bac ) {
                throw bac;
            } catch ( FaultException<LimitReachedFault> lre ) {
                throw lre;
            } catch ( FaultException ) {
                throw;
            }
            return isLogged;
        }
    }

}
