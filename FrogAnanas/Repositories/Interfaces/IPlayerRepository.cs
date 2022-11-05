using FrogAnanas.Models;

namespace FrogAnanas.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayer(long id);
        Task<long> AddPlayer(Player player);
    }
}