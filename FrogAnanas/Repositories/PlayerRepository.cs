using FrogAnanas.Constants;
using FrogAnanas.Context;
using FrogAnanas.Models;

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
            lock (locker)
                return context.Players.FirstOrDefault(x => x.UserId == userId);
        }
        public List<Resource> GetPlayerResources(long userId)
        {
            var resourcesPlayerId = context.ResourcesPlayers.Where(x => x.UserId == userId).Select(x => x.ResourceId).ToList();
            var resources = context.Resources.Where(x => resourcesPlayerId.Contains(x.Id)).ToList();
            return resources;
        }
        public List<Item> GetPlayerItems(long userId)
        {
            var itemsPlayerId = context.ItemsPlayers.Where(x => x.UserId == userId).Select(x => x.ItemId).ToList();
            var items = context.Items.Where(x => itemsPlayerId.Contains(x.Id)).ToList();
            return items;
        }
        public List<Skill> GetPlayerSkills(long userId)
        {
            var skillPlayers = context.MasteryPlayers.Where(x => x.UserId == userId).ToList();
            var skills = new List<Skill>();
            foreach (var sp in skillPlayers)
            {
                var skill = context.Skills.FirstOrDefault(x => x.MasteryId == sp.MasteryId && x.LvlRequirement < sp.CurrentLvl);
                if (skill is not null)
                    skills.Add(skill);
            }
            return skills;
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
            context.ItemsPlayers.Add(new ItemsPlayer
            {
                ItemId = 1,
                UserId = userId,
            });
            context.ResourcesPlayers.Add(new ResourcesPlayer
            {
                ResourceId = 1,
                UserId = userId,
            });

        }

        public void SetEvent(long userId, EventType currEvent)
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
            player.DPS = 3;
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

            foreach (EventType @event in Enum.GetValues(typeof(EventType)))
                await context.UserEvents.AddAsync(new UserEvent
                {
                    Id = (int)@event,
                    Name = @event.ToString()
                });

            var masterySwordId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 1,
                Name = RegistrationPhrase.masterySword
            })).Entity.Id;
            var masteryDaggerId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 2,
                Name = RegistrationPhrase.masteryDagger
            })).Entity.Id;
            var masteryAxeId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 3,
                Name = RegistrationPhrase.masteryAxe
            })).Entity.Id;
            var masteryHummerId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 4,
                Name = RegistrationPhrase.masteryHummer
            })).Entity.Id;
            var masteryPeakId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 5,
                Name = RegistrationPhrase.masteryPeak
            })).Entity.Id;
            var masteryDoubleSwordId = (await context.Masteries.AddAsync(new Mastery
            {
                Id = 6,
                Name = RegistrationPhrase.masteryDoubleSword
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

            await context.Stages.AddAsync(new Stage
            {
                Id = 1,
                Description = "1 этаж"
            });
            await context.Stages.AddAsync(new Stage
            {
                Id = 2,
                Description = "2 этаж"
            });
            await context.Stages.AddAsync(new Stage
            {
                Id = 3,
                Description = "3 этаж"
            });
            await context.Stages.AddAsync(new Stage
            {
                Id = 4,
                Description = "4 этаж"
            });
            await context.Stages.AddAsync(new Stage
            {
                Id = 5,
                Description = "5 этаж"
            });

            await context.Items.AddAsync(new Item
            {
                Id = 1,
                Name = "Маленькая бутылка зелья исцеления"
            });

            await context.Resources.AddAsync(new Resource
            {
                Id = 1,
                Name = "Ветка дерева"
            });

            await context.Enemies.AddAsync(new Enemy
            {
                Id = 1,
                Name = "Хоб гоблин",
                Accuracy = 0.8,
                CritChance = 0.05,
                MultipleCrit = 1.5,
                Damage = 2,
                Defence = 2,
                Evation = 0.1,
                HP = 20,
                Description = "Уебано редкостный",
                Initiative = 0.7
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
