using FrogAnanas.Context;
using FrogAnanas.Repositories;

namespace FrogAnanas.Services
{
    public class BattleService : IBattleService
    {
        private Random random = new Random();
        private readonly IEnemyRepository enemyRepository;
        private readonly IPlayerRepository playerRepository;
        private readonly MongoDbRepository eventRepository;
        public BattleService(IEnemyRepository enemyRepository, IPlayerRepository playerRepository, MongoDbRepository eventRepository)
        {
            this.enemyRepository = enemyRepository;
            this.playerRepository = playerRepository;
            this.eventRepository = eventRepository;
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
                int dmg = player.Damage - enemy.Defence;
                enemy.HP -= dmg;
                damagePlayer = $"⚔Вы нанесли {dmg} урона\n";
            }
            if (enemy.HP <= 0)
            {
                playerRepository.InreaseXP(userId, enemy.GivenXP);
                var level = playerRepository.GetCurrentMasteryLevel(userId);
                return damagePlayer + $"\n\n VICTORY\nПолучено опыта:{enemy.GivenXP}\nТекущий прогресс: {level.Level} уровень\n{level.CurrentXP}/{level.RequiredXP}";
            }

            if (IsDodge(enemy.Accuracy, player.Evation))
                damageEnemy = "Вы уклонились от атаки противника\n";
            else
            {
                int dmg = enemy.Damage - player.Defence;
                playerRepository.ReduceHP(userId, dmg);
                damageEnemy = $"⚔Противник нанес {dmg} урона\n";
            }
            msg = damagePlayer + damageEnemy + "\n\n" + $"Ваше здоровье:{player.CurrentHP}/{player.HP} \nЗдоровье {enemy.Name}: {enemy.HP}";
            return msg;
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
