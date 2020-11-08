using System;
using System.Net.NetworkInformation;

namespace Utils {
    public class NetworkAddress {
        public static string GetMacAddress() {
            string mac = "";
            try {
                foreach ( NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces() ) {
                    if ( nic.OperationalStatus == OperationalStatus.Up && ( !nic.Description.Contains( "Virtual" ) && !nic.Description.Contains( "Pseudo" ) ) ) {
                        if ( nic.GetPhysicalAddress().ToString() != "" ) {
                            mac = nic.GetPhysicalAddress().ToString();
                        }
                    }
                }
            } catch ( Exception ) {
                throw;
            }
            return mac;
        }
    }

}
