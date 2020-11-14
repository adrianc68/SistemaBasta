using Basta.Properties;
using Database.DAO;
using Database.Entity;
using System;
using Utils;

namespace Basta {
    public class Autentication {
        //private static Autentication instance;
        //private Player player;
        //private string macAddress;

        //public Player GetPlayer() {
        //    Player playerAux = player;
        //    return playerAux;
        //}

        //public static Autentication GetInstance() {
        //    if ( instance == null ) {
        //        instance = new Autentication();
        //    }
        //    return instance;
        //}

        //public bool LogIn( string email, string password ) {
        //    bool isLogged = false;
        //    try {
        //        macAddress = GetMacAddress();
        //        CheckAttemptsLimit();
        //        SendMacAddress();
        //        Player auxiliar = GetCurrentPlayer( email, password );
        //        ResetAttempts();
        //        CheckPlayerState( email );
        //        player = auxiliar;
        //        isLogged = true;

        //    } catch ( AccessAccountNotFoundException aac ) {
        //        throw aac;
        //    } catch ( BannedAccountException bac ) {
        //        throw bac;
        //    } catch ( LimitReachedException lre ) {
        //        throw lre;
        //    } catch ( Exception ) {
        //        throw;
        //    }
        //    return isLogged;
        //}

        //private Player GetCurrentPlayer( string email, string password ) {
        //    PlayerDAO playerDAO = new PlayerDAO();
        //    Player player = playerDAO.GetPlayerAccount( email, password );
        //    if ( player == null ) {
        //        throw new AccessAccountNotFoundException( Resource.SystemLoginError );
        //    }
        //    return null;
        //}

        //private void CheckPlayerState( string email ) {
        //    AccountState accountState;
        //    AccessAccountDAO accessAccountDAO = new AccessAccountDAO();
        //    accountState = accessAccountDAO.CheckAccountState( email );
        //    if ( accountState == AccountState.BANNED ) {
        //        throw new BannedAccountException( Resource.SystemLoginAccountBanned );
        //    }
        //}

        //private void CheckAttemptsLimit() {
        //    int ATTEMPS_LIMIT = 5;
        //    bool isAttempsLimitReached = new HostDAO().getAttemptsByMacAddress( macAddress ) == ATTEMPS_LIMIT;
        //    if ( isAttempsLimitReached ) {
        //        throw new LimitReachedException( Resource.SystemAttemptsLimitReached );
        //    }
        //}

        //private string GetMacAddress() {
        //    string macAddressEncrypted;
        //    try {
        //        macAddressEncrypted = Cryptography.SHA256_Hash( NetworkAddress.GetMacAddress() );
        //    } catch ( Exception ) {
        //        throw;
        //    }
        //    return macAddressEncrypted;
        //}

        //private bool SendMacAddress() {
        //    bool isMacAddressSent;
        //    isMacAddressSent = new HostDAO().sendActualMacAddress( macAddress );
        //    return isMacAddressSent;
        //}

        //private bool ResetAttempts() {
        //    bool isAttemptsReset;
        //    isAttemptsReset = new HostDAO().resetAttempts( macAddress );
        //    return isAttemptsReset;
        //}

        //private Autentication() { }


    }

}
