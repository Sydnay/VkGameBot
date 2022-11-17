using FrogAnanas.DTOs;
using FrogAnanas.Events;
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
        List<DropInfoDto> GetPlayerResources(long userId);
        List<Item> GetPlayerItems(long userId);
        List<Skill> GetPlayerSkills(long userId);
        void AddPlayer (Player player);
        void SetEvent(long userId, EventType currEvent);
        void SetDefaultStats(long userId, Gender gender, string name);
        void SetMastery(long userId, int masteryId);
        void ReduceHP(long userId, int amountHP);
        bool InreaseXP(long userId, int amountXP);
        void AddResource(long userId, ResourcesPlayer resource);
        void AddItem(long userId, ItemsPlayer item);
        int GetFreeSlots(long userId);
        CurrentMasteryLevelDto GetCurrentMasteryLevel(long userId);
        Task ABOBA();
    }
}
