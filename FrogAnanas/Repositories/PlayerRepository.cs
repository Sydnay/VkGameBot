using FrogAnanas.Constants;
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
                HandleAddPlayer(player.UserId);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void HandleAddPlayer(long userId)
        {
            var masteries = context.Masteries.ToList();
            foreach (var mastery in masteries)
            {
                context.MasteryPlayers.Add(new MasteryPlayer
                {
                    MasteryId = mastery.Id,
                    UserId = userId,
                });
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
        public void SetMastery(long userId, int masteryId)
        {
            try
            {
                var player = GetPlayer(userId);
                player.MasteryId = masteryId;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandleAcceptFirstRole,
                Name = EventType.HandleAcceptFirstRole.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandleFirstRole,
                Name = EventType.HandleFirstRole.ToString()
            });


            var masterySwordId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 1,
                Name = ConstPhrase.masterySword
            })).Entity.Id;
            var masteryDaggerId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 2,
                Name = ConstPhrase.masteryDagger
            })).Entity.Id;
            var masteryAxeId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 3,
                Name = ConstPhrase.masteryAxe
            })).Entity.Id;
            var masteryHummerId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 4,
                Name = ConstPhrase.masteryHummer
            })).Entity.Id;
            var masteryPeakId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 5,
                Name = ConstPhrase.masteryPeak
            })).Entity.Id;
            var masteryDoubleSwordId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 6,
                Name = ConstPhrase.masteryDoubleSword
            })).Entity.Id;


            await context.Levels.AddAsync(new Level
            {
                Lvl = 1,
                RequrimentXP = 0
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 2,
                RequrimentXP = 100
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 3,
                RequrimentXP = 500
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 4,
                RequrimentXP = 1000
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 5,
                RequrimentXP = 1800
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 6,
                RequrimentXP = 3000
            });


            await context.Skills.AddAsync(new Skill
            {
                MasteryId = masterySwordId,
                LvlRequirement = 0,
                Name = "Удар мечом",
            });
            await context.Skills.AddAsync(new Skill
            {
                MasteryId = masteryAxeId,
                LvlRequirement = 0,
                Name = "Удар топором",
            });
            await context.Skills.AddAsync(new Skill
            {
                MasteryId = masteryDaggerId,
                LvlRequirement = 0,
                Name = "Удар кинажлом",
            });
            await context.Skills.AddAsync(new Skill
            {
                MasteryId = masteryDoubleSwordId,
                LvlRequirement = 0,
                Name = "Удар двуручным мечом",
            });
            await context.Skills.AddAsync(new Skill
            {
                MasteryId = masteryHummerId,
                LvlRequirement = 0,
                Name = "Удар молотом",
            });
            await context.Skills.AddAsync(new Skill
            {
                MasteryId = masteryPeakId,
                LvlRequirement = 0,
                Name = "Удар копьем",
            });
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
