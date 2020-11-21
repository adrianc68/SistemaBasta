using Domain.Domain;
using System;
using System.ServiceModel;

namespace Basta.Client {
    //public class MyCallBack: Proxy.IChatServiceCallback {
 
    //    public void ReciveMessage( Player player, string message ) {
    //        Console.WriteLine( player + message );
    //    }
    //}
    //class Program {
    //    static void Main() {
    //        InstanceContext context = new InstanceContext( new MyCallBack() );
    //        Proxy.ChatServiceClient server = new Proxy.ChatServiceClient( context );
    //        Console.WriteLine( "Enter username" );
    //        var username = Console.ReadLine();
    //        server.Connect( new Player() { AccessAccount = new AccessAccount { Username = username} } );
    //        Console.WriteLine();
    //        Console.WriteLine( "Enter message" );
    //        Console.WriteLine( "Press Q To Exit" );
    //        var message = Console.ReadLine();

    //        while ( message != "Q" ) {
    //            if ( !string.IsNullOrEmpty( message ) ) {
    //                server.SendMessage( message );
    //            }
    //            message = Console.ReadLine();
    //        }

    //    }
    //}
}
