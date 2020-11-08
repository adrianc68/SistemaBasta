using Domain.Domain;

namespace Database.DAO.Interface {
    interface IPlayerDAO {
        bool AddPlayerAccount(Player player);

        Player GetPlayerAccount(string email, string password);
    }
}
