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
        User GetUserAsync (long id);
        User GetUserWithPlayerAsync(long userId);
        Task AddUser (User user);
        void SetCurrentEvent(long userId, EventType currEvent);
        Task SetPlayerId(long userId, long playerId);
        Task ABOBA();
    }
}
