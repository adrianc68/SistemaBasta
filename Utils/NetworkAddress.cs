using System;
using System.Net.NetworkInformation;

namespace Utils {
    /*
    The NetWorkAddress class
    Contains all methods for get MacAddress
    */
    public class NetworkAddress {
        // Get MacAddress from actual network
        /// <summary>
        /// It obtains the actual PC's macaddress
        /// </summary>
        /// <exception cref="Exception"> Thrown when an exception occurs.</exception>
        /// <returns>
        /// returns a macaddress string.
        /// </returns>
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
