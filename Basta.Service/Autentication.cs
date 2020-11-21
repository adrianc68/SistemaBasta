using Basta.Contracts.Faults;
using Database.DAO;
using Domain.Domain;
using System;
using System.ServiceModel;

namespace Basta.Service {

    public class Autentication {
        private Player player;

        public Player Player { get => player; set => player = value; }

        public bool LogIn( string email, string password, string macAddress ) {
            bool isLogged = false;
            try {
                CheckAttemptsLimit( macAddress );
                SendMacAddress( macAddress );
                Player aux = GetCurrentPlayer( email, password );
                CheckPlayerState( email );
                player = aux;
                ResetAttempts( macAddress );
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

        private Player GetCurrentPlayer( string email, string password ) {
            Player player = null;
            try {
                player = new PlayerDAO().GetPlayerAccount( email, password );
            } catch ( Exception e ) {
                throw new FaultException( e.Message + e.StackTrace );
            }
            if ( player == null ) {
                AccessAccountNotFoundFault accountNotFoundFault = new AccessAccountNotFoundFault();
                accountNotFoundFault.Message = "Account not found";
                throw new FaultException<AccessAccountNotFoundFault>( accountNotFoundFault, "Account not found" );
            }
            return player;
        }

        private void CheckPlayerState( string email ) {
            AccountState accountState;
            try {
                accountState = new AccessAccountDAO().CheckAccountState( email );
            } catch ( Exception e ) {
                throw new FaultException( e.Message + e.StackTrace );
            }
            if ( accountState == AccountState.BANNED ) {
                BannedAccountFault bannedAccountFault = new BannedAccountFault();
                bannedAccountFault.Message = "Banned";
                throw new FaultException<BannedAccountFault>( bannedAccountFault, "Banned" );
            }

        }

        private void CheckAttemptsLimit( string macAddress ) {
            bool isAttempsLimitReached = false;
            int ATTEMPS_LIMIT = 5;
            try {
                isAttempsLimitReached = new HostDAO().getAttemptsByMacAddress( macAddress ) == ATTEMPS_LIMIT;
            } catch ( Exception e ) {
                throw new FaultException( e.Message + e.StackTrace );
            }
            if ( isAttempsLimitReached ) {
                LimitReachedFault limitReachedFault = new LimitReachedFault();
                limitReachedFault.Message = "Attempts limit reached!";
                throw new FaultException<LimitReachedFault>( limitReachedFault, "Limit reached!" );
            }
        }

        private bool SendMacAddress( string macAddress ) {
            bool isMacAddressSent = false;
            try {
                isMacAddressSent = new HostDAO().sendActualMacAddress( macAddress );
            } catch ( Exception e ) {
                throw new FaultException( e.Message + e.StackTrace );
            }
            return isMacAddressSent;
        }

        private bool ResetAttempts( string macAddress ) {
            bool isAttemptsReset = false;
            try {
                isAttemptsReset = new HostDAO().resetAttempts( macAddress );
            } catch ( Exception e ) {
                throw new FaultException( e.Message + e.StackTrace );
            }
            return isAttemptsReset;
        }

    }

}

