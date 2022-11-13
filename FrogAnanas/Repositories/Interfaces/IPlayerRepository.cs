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
        List<Resource> GetPlayerResources(long userId);
        List<Item> GetPlayerItems(long userId);
        List<Skill> GetPlayerSkills(long userId);
        void AddPlayer (Player player);
        void SetEvent(long userId, EventType currEvent);
        void SetDefaultStats(long userId, Gender gender, string name);
        void SetMastery(long userId, int masteryId);
        Task ABOBA();
    }
}
