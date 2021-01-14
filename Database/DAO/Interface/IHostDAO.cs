using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAO.Interface {
    interface IHostDAO {
        // Get attempts from database
        /// <summary>
        /// It get the actually attempts perfomed by a user who is tryng to login to App.
        /// </summary>
        /// <returns>
        /// returns int representing the attempts.
        /// </returns>
        int getAttemptsByMacAddress( string address );
        // Set an attempt to database
        /// <summary>
        /// Add an attempt to database to a specified macAddress.
        /// </summary>
        /// <returns>
        /// returns true if any row was affected otherwise false.
        /// </returns>
        bool sendActualMacAddress( string address );
        // Set attempts to 0 in database
        /// <summary>
        /// It should be used to reset the attempts perfomed by a user.
        /// </summary>
        /// <returns>
        /// returns true if any row was affected otherwise false.
        /// </returns>
        bool resetAttempts( string address );
    }
}
