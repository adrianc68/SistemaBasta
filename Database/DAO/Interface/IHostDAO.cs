using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAO.Interface {
    interface IHostDAO {
        int getAttemptsByMacAddress( string address );

        bool sendActualMacAddress( string address );

        bool resetAttempts( string address );
    }
}
