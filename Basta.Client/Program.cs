using Basta.Client.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Basta.Client {
    public class MyCallBack: Proxy.IChatServiceCallback {
        public void ReciveMessage( Player player, Message message ) {
            Console.WriteLine( player.AccessAccount.Username + ": " + message.MessageSent);
        }
    }
    class Program {
        static void Main() {
            InstanceContext context = new InstanceContext( new MyCallBack() );
            Proxy.ChatServiceClient server = new Proxy.ChatServiceClient( context );
            Console.WriteLine( "Enter username" );
            var username = Console.ReadLine();
            server.Connect( new Player { AccessAccount = new AccessAccount { Username = username } } );
            Console.WriteLine();
            Console.WriteLine( "Enter message" );
            Console.WriteLine("Press Q To Exit");
            var message = Console.ReadLine();
            
            while(message != "Q") {
                if( !string.IsNullOrEmpty(message)) {
                    server.SendMessage(new Message { MessageSent = message} );
                }
                message = Console.ReadLine();
            }

        }
    }
}
