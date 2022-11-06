using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync (long id);
        Task<User> GetUserWithPlayerAsync(long userId);
        Task AddUser (User user);
        Task SetCurrentEvent(long userId, EventType currEvent);
        Task SetPlayerId(long userId, long playerId);
        Task ABOBA();
    }
}
