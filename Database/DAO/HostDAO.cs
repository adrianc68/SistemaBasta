using Database.DAO.Interface;
using Database.Entity;
using System;
using System.Data;
using System.Linq;

namespace Database.DAO {

    /*
        The PlayerDAO class
        Contains all methods for getting and setting data from database.
    */
    public class HostDAO: IHostDAO {

        // Get attempts from database
        /// <summary>
        /// It get the actually attempts perfomed by a user who is tryng to login to App.
        /// </summary>
        /// <returns>
        /// returns int representing the attempts.
        /// </returns>
        public int getAttemptsByMacAddress( string address ) {
            int attempsByAddress = -1;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var macAddress = database.Hosts
                    .Where( b => b.mac_address == address )
                    .FirstOrDefault();
                if ( macAddress != null ) {
                    attempsByAddress = macAddress.attempts;
                }
            }

            return attempsByAddress;
        }

        // Set attempts to 0 in database
        /// <summary>
        /// It should be used to reset the attempts perfomed by a user.
        /// </summary>
        /// <returns>
        /// returns true if any row was affected otherwise false.
        /// </returns>
        public bool resetAttempts( string address ) {
            bool isAttemptsReset = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var macAddress = database.Hosts
                    .Where( b => b.mac_address == address )
                    .FirstOrDefault();
                if ( macAddress != null ) {
                    macAddress.attempts = 0;
                    isAttemptsReset = true;
                }
                database.SaveChanges();
            }
            return isAttemptsReset;
        }

        // Set an attempt to database
        /// <summary>
        /// Add an attempt to database to a specified macAddress.
        /// </summary>
        /// <returns>
        /// returns true if any row was affected otherwise false.
        /// </returns>
        public bool sendActualMacAddress( string address ) {
            bool isAddressSent = false;
            using ( BastaEntityModelContainer database = new BastaEntityModelContainer() ) {
                var macAddress = database.Hosts
                    .Where( b => b.mac_address == address )
                    .FirstOrDefault();
                if ( macAddress == null ) {
                    Host host = new Host();
                    host.mac_address = address;
                    host.attempts = 1;
                    database.Hosts.Add( host );
                    isAddressSent = true;
                } else {
                    macAddress.attempts++;
                }
                database.SaveChanges();
            }
            return isAddressSent;
        }

    }
}
