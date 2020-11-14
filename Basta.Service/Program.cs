
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

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
        }

        private static void PrintServiceInfo( ServiceDescription desc ) {
            foreach ( ServiceEndpoint nextEndpoint in desc.Endpoints ) {
                Console.WriteLine( nextEndpoint.Address );
            }
        }

        private static void HostOnOpened( object sender, EventArgs e ) {
            Console.WriteLine( "Service started" );
        }
    }

}
