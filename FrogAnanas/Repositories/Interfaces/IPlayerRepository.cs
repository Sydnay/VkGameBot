using FrogAnanas.Models;

namespace FrogAnanas.Repositories
{
    public interface IPlayerRepository
    {
        Player GetPlayer(long? id);
        Task<long> AddPlayer(Player player);
    }
}