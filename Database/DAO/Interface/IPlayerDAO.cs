
using Domain.Domain;
using System.Collections.Generic;

namespace Database.DAO.Interface {
    interface IPlayerDAO {
        // Add a Player in Database.
        /// <summary>
        /// It adds a specified Player in database.
        /// </summary>
        /// <returns>
        /// returns true if callableStatement was perfomed otherwise false.
        /// </returns>
        bool AddPlayerAccount( Player player );
        // Get a Player from Database.
        /// <summary>
        /// It gets a Player by a email and password. It is used by a Login system.
        /// </summary>
        /// <returns>
        /// returns Player if exist Player otherwise null.
        /// </returns>
        Player GetPlayerAccount( string email, string password );

        // Set a score to a player
        /// <summary>
        /// It's used to show in top table the ranking.
        /// </summary>
        void SetScoreToPlayer(string email, double score);

        // Get score from database
        /// <summary>
        /// It gets top 10 player's score.
        /// </summary>
        /// <returns>
        /// return List<Player> with 10 elements
        /// </returns>
        Queue<Player> GetTopTenPlayers();

    }
}
