using FrogAnanas.Context;
using FrogAnanas.Events;
using FrogAnanas.Models;
using FrogAnanas.Repositories;

namespace FrogAnanas.Services
{
    public class BattleService : IBattleService
    {
        private Random random = new Random();
        private readonly IEnemyRepository enemyRepository;
        private readonly IResourceRepository resourceRepository;
        private readonly IItemRepository itemRepository;
        private readonly IPlayerRepository playerRepository;
        private readonly IMasteryRepository masteryRepository;
        private readonly MongoDbRepository eventRepository;
        public BattleService(IEnemyRepository enemyRepository,
            IPlayerRepository playerRepository,
            IMasteryRepository masteryRepository,
            IResourceRepository resourceRepository,
            IItemRepository itemRepository,
            MongoDbRepository eventRepository)
        {
            this.enemyRepository = enemyRepository;
            this.resourceRepository = resourceRepository;
            this.itemRepository = itemRepository;
            this.playerRepository = playerRepository;
            this.eventRepository = eventRepository;
            this.masteryRepository = masteryRepository;
        }
        public string Attack(long userId)
        {
            var player = eventRepository.GetPlayer(userId);
            var enemy = eventRepository.GetEnemy(userId);

            string msg;
            string damagePlayer;
            string damageEnemy;

            if (IsDodge(player.Accuracy, enemy.Evation))
                damagePlayer = "Противник уклонился от вашей атаки\n";
            else
            {
                int dmg = player.Damage - enemy.Defence < 1 ? 1 : player.Damage - enemy.Defence;
                enemy.HP -= dmg;
                damagePlayer = $"⚔Вы нанесли {dmg} урона\n";
            }
            if (enemy.HP <= 0)
            {
                var islvlUp = playerRepository.InreaseXP(userId, enemy.GivenXP);
                var msgLvlUp = islvlUp ? $"\nУровень владения {masteryRepository.GetMastery(player.MasteryId ?? 0)?.Name} повышен!\n" : String.Empty;
                var level = playerRepository.GetCurrentMasteryLevel(userId);
                return damagePlayer + $"\n\n VICTORY\n{msgLvlUp}Получено опыта:{enemy.GivenXP}\nТекущий прогресс: {level.Level} уровень\n{level.CurrentXP}/{level.RequiredXP}";
            }

            if (IsDodge(enemy.Accuracy, player.Evation))
                damageEnemy = "Вы уклонились от атаки противника\n";
            else
            {
                int dmg = enemy.Damage - player.Defence < 1 ? 1 : enemy.Damage - player.Defence;
                playerRepository.ReduceHP(userId, dmg);
                damageEnemy = $"⚔Противник нанес {dmg} урона\n";
            }
            msg = damagePlayer + damageEnemy + "\n\n" + $"Ваше здоровье:{player.CurrentHP}/{player.HP} \nЗдоровье {enemy.Name}: {enemy.HP}";
            return msg;
        }
        public DropInfoDto DropResource(long userId)
        {
            var enemy = eventRepository.GetEnemy(userId);
            var drop = enemyRepository.GenerateResourceFromEnemy(userId, enemy.Id);
            var resource = resourceRepository.GetResource(drop.ResourceId);
            playerRepository.AddResource(userId, drop);
            var freeslots = playerRepository.GetFreeSlots(userId);
            return new DropInfoDto { Amount = drop.Amount, Name = resource.Name, FreeSlotAmount = freeslots };
        }
        public DropInfoDto DropItem(long userId)
        {
            var enemy = eventRepository.GetEnemy(userId);
            var drop = enemyRepository.GenerateItemFromEnemyOrDefault(userId, enemy.Id);
            if (drop is null)
                return null;
            var item = itemRepository.GetItem(drop.ItemId);
            playerRepository.AddItem(userId, drop);
            var freeslots = playerRepository.GetFreeSlots(userId);
            return new DropInfoDto { Amount = 1, Name = item.Name, FreeSlotAmount = freeslots };
        }
        public void EndBattle(long userId)
        {
            eventRepository.CloseEvent(userId);
        }
        private bool IsDodge(double accuracy, double evation)
        {
            var chance = accuracy - evation;
            double randomNum = random.Next(100) / 100d;
            return chance > randomNum ? false : true;
        }

        public bool isEnemyDead(long userId)
        {
            var enemy = eventRepository.GetEnemy(userId);
            return enemy.HP <= 0 ? true : false;
        }

        public bool isPlayerDead(long userId)
        {
            var player = eventRepository.GetPlayer(userId);
            return player.HP <= 0 ? true : false;
        }
    }
}
