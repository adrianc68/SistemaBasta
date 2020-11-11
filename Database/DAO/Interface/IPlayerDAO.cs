
using Database.Entity;

namespace Database.DAO.Interface {
    interface IPlayerDAO {
        // Add a Player in Database.
        /// <summary>
        /// It adds a specified Player in database.
        /// </summary>
        /// <returns>
        /// returns true if callableStatement was perfomed otherwise false.
        /// </returns>
        bool AddPlayerAccount(Player player);
        // Get a Player from Database.
        /// <summary>
        /// It gets a Player by a email and password. It is used by a Login system.
        /// </summary>
        /// <returns>
        /// returns Player if exist Player otherwise null.
        /// </returns>
        Player GetPlayerAccount(string email, string password);
    }
}
