using System;

namespace Basta.GUI.Login.Main {
    public class RoomServiceCallBack: Proxy.IRoomServiceCallback {

        public void ReciveMessageRoom( Domain.Domain.Player player, string message ) {
            Console.WriteLine( player.AccessAccount.Username + ": " + message );
        }
    }

}
