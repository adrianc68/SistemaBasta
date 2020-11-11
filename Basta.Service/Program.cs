using Basta.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Basta.Service {
    class Program {
        static void Main( string[] args ) {

            using (ServiceHost host = new ServiceHost(typeof(BastaService) ) ) {
                host.Open();
                Console.WriteLine( $"{host.Description.Name} is up and listening on the URI given below. Press <enter> to exit." );
                PrintServiceInfo( host.Description );
                Console.ReadLine();
                host.Close();
            }
            
            //var uris = new Uri[1];
            //string address = "net.tcp://localhost:4345/BastaService";
            //uris[0] = new Uri( address );
            //IChatService chatService = new BastaService();
            //ServiceHost host = new ServiceHost( chatService, uris );
            //var binding = new NetTcpBinding( SecurityMode.None );
            //host.AddServiceEndpoint( typeof( IChatService ), binding, String.Empty );
            //host.Opened += HostOnOpened;

            //host.Open();
            //Console.ReadLine();
        }

        private static void PrintServiceInfo( ServiceDescription desc ) {
            foreach ( ServiceEndpoint nextEndpoint in desc.Endpoints ) {
                Console.WriteLine( nextEndpoint.Address );
            }

        }

        private static void HostOnOpened( object sender, EventArgs e ) {
            Console.WriteLine( "TCP Service started" );
        }
    }

}
