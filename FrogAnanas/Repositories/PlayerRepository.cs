using FrogAnanas.Constants;
using FrogAnanas.Context;
using FrogAnanas.DTOs;
using FrogAnanas.Events;
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
            lock (locker)
                return context.Players.FirstOrDefault(x => x.UserId == userId);
        }
        public List<DropInfoDto> GetPlayerResources(long userId)
        {
            var resourcesPlayer = context.ResourcesPlayers.Where(x => x.UserId == userId).ToList();
            var resources = context.Resources.Where(x => resourcesPlayer.Select(x => x.ResourceId).Contains(x.Id)).ToList();
            var info = new List<DropInfoDto>();
            foreach (var res in resourcesPlayer)
            {
                var resource = resources.FirstOrDefault(x => x.Id == res.ResourceId);
                info.Add(new DropInfoDto
                {
                    Amount = res.Amount,
                    Name = resource.Name,
                });
            }
            return info;
        }
        public List<Item> GetPlayerItems(long userId)
        {
            var itemsPlayer = context.ItemsPlayers.Where(x => x.UserId == userId).ToList();
            var items = context.Items.Where(x => itemsPlayer.Select(x => x.ItemId).Contains(x.Id)).ToList();
            var info = new List<Item>();
            foreach (var itm in itemsPlayer)
            {
                var item = items.FirstOrDefault(x => x.Id == itm.ItemId);
                info.Add(item);
            }
            return info;
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

        public void ReduceHP(long userId, int amountHP)
        {
            var player = GetPlayer(userId);
            player.CurrentHP -= amountHP;
            context.SaveChanges();
        }
        public bool InreaseXP(long userId, int amountXP)
        {
            bool isLvlUp = false;
            var player = GetPlayer(userId);
            var mastery = context.MasteryPlayers.Include(x=>x.CurrentLevelCLASS).FirstOrDefault(x => x.UserId == userId && x.MasteryId == player.MasteryId);
            mastery!.CurrentXP += amountXP;

            if (mastery.CurrentXP >= mastery.CurrentLevelCLASS.RequrimentXP)
            {
                IncreaseLvl(player, mastery);
                isLvlUp = true;
            }

            context.SaveChanges();
            return isLvlUp;
        }
        public CurrentMasteryLevelDto GetCurrentMasteryLevel(long userId)
        {
            var player = GetPlayer(userId);
            var level = context.MasteryPlayers.Include(x=>x.CurrentLevelCLASS).FirstOrDefault(x => x.UserId == userId && x.MasteryId == player.MasteryId);
            return new CurrentMasteryLevelDto
            {
                Level = level.CurrentLvl,
                CurrentXP = level.CurrentXP,
                RequiredXP = level.CurrentLevelCLASS.RequrimentXP
            };
        }
        public void AddResource(long userId, ResourcesPlayer resource)
        {
            if (resource.Amount < 1 || GetFreeSlots(userId)<1)
                return;

            var resourceInInventory = context.ResourcesPlayers.FirstOrDefault(x => x.UserId == userId && x.ResourceId == resource.ResourceId);

            if(resourceInInventory is not null)
                resourceInInventory.Amount+=resource.Amount;
            else
                context.ResourcesPlayers.Add(resource);

            context.SaveChanges();
        }
        public void AddItem(long userId, ItemsPlayer item)
        {
            if (item is null || GetFreeSlots(userId) < 1)
                return;

            context.ItemsPlayers.Add(item);

            context.SaveChanges();
        }
        public int GetFreeSlots(long userId)
        {
            var slots = GetPlayer(userId).ResourcesSlotAmount;
            var takenSlotsAmount = context.ResourcesPlayers.Where(x => x.UserId == userId).ToList().Count;
            takenSlotsAmount += context.ItemsPlayers.Where(x => x.UserId == userId).ToList().Count;
            return slots - takenSlotsAmount;
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
                Amount = 10,
                UserId = userId,
            });

        }
        private void IncreaseLvl(Player player, MasteryPlayer mastery)
        {
            player.CurrentHP = player.HP;
            
            mastery.CurrentXP -= mastery.CurrentLevelCLASS.RequrimentXP;
            mastery.CurrentLvl++;
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
                RequrimentXP = 150
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 2,
                RequrimentXP = 500
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 3,
                RequrimentXP = 1000
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 4,
                RequrimentXP = 1800
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 5,
                RequrimentXP = 3000
            });
            await context.Levels.AddAsync(new Level
            {
                Lvl = 6,
                RequrimentXP = 5000
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


            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
            await context.Resources.AddAsync(new Resource
            {
                Id = 2,
                Name = "Клык гоблина"
            });
            await context.Resources.AddAsync(new Resource
            {
                Id = 3,
                Name = "Сало вкуснячее"
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
                Description = "Уебан редкостный",
                Initiative = 0.7,
                GivenXP = 30,
                Stages = context.Stages.Where(x=>x.Id <6).ToList(),
            });
            await context.Enemies.AddAsync(new Enemy
            {
                Id = 2,
                Name = "Хохлуша украинская",
                Accuracy = 0.8,
                CritChance = 0.05,
                MultipleCrit = 1.5,
                Damage = 4,
                Defence = 2,
                Evation = 0.1,
                HP = 15,
                Description = "Уебан редкостный",
                Initiative = 0.7,
                GivenXP = 50,
                Stages = context.Stages.Where(x => x.Id < 6).ToList(),
            });

            await context.ResourcesEnemies.AddAsync(new ResourcesEnemy
            {
                EnemyId = 1,
                ResourceId = 2,
                DropChance = 0.7,
                MaxAmount = 3
            });
            await context.ResourcesEnemies.AddAsync(new ResourcesEnemy
            {
                EnemyId = 2,
                ResourceId = 3,
                DropChance = 0.5,
                MaxAmount = 4
            });

            await context.ItemsEnemies.AddAsync(new ItemsEnemy
            {
                EnemyId = 1,
                ItemId = 1,
                DropChance = 0.7
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
