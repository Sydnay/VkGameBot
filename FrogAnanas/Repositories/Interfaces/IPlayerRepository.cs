using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public interface IPlayerRepository
    {
        Player GetPlayer (long userId);
        List<Player> GetAllPlayers();
        void AddPlayer (Player player);
        void SetCurrentEvent(long userId, EventType currEvent);
        void SetDefaultStats(long userId, Gender gender, string name);
        Task ABOBA();
    }
}
