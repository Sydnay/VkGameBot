using FrogAnanas.Context;
using FrogAnanas.Models;
using Microsoft.EntityFrameworkCore;

namespace FrogAnanas.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationContext context;
        private readonly
        object locker = new object();
        public PlayerRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public Player GetPlayer(long userId)
        {
            lock(locker)
                return context.Players.FirstOrDefault(x=>x.UserId == userId);
        }

        public List<Player> GetAllPlayers()
        {
            lock (locker)
            {
                return context.Players.ToList();
            }
        }

        public void AddPlayer(Player player)
        {
            try
            {
                context.Players.Add(player);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetCurrentEvent(long userId, EventType currEvent)
        {
            var player = GetPlayer(userId);

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            player.UserEventId = (int)currEvent;
            context.SaveChanges();
        }
        public void SetDefaultStats(long userId, Gender gender, string name)
        {
            var player = GetPlayer(userId);

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            player.Gender = gender;
            player.Name = name;
            player.DPS = 1;
            player.Defence = 3;
            player.Accuracy = 0.8;
            player.Evation = 0.2;
            player.CritChance = 0.1;
            player.MultipleCrit = 1.1;
            player.HP = 50;
            player.Initiative = 0.8;
            player.Perception = 2;
            context.SaveChanges();
        }

        public async Task ABOBA()
        {
            if (context.UserEvents.FirstOrDefault() is not null)
                return;

            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandleStart,
                Name = EventType.HandleStart.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandleGender,
                Name = EventType.HandleGender.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandleCreation,
                Name = EventType.HandleCreation.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandlePlayer,
                Name = EventType.HandlePlayer.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandlePlayerInfo,
                Name = EventType.HandlePlayerInfo.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandlePlayerInventory,
                Name = EventType.HandlePlayerInventory.ToString()
            });
            await context.SaveChangesAsync();
        }
    }
}
