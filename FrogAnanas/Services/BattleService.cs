using FrogAnanas.Models;
using FrogAnanas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Services
{
    public class BattleService : IBattleService
    {
        private Random random = new Random();
        private readonly IEnemyRepository enemyRepository;
        private readonly IPlayerRepository playerRepository;
        public BattleService(IEnemyRepository enemyRepository, IPlayerRepository playerRepository)
        {
            this.enemyRepository = enemyRepository;
            this.playerRepository = playerRepository;
        }
        public string Attack(Player player, Enemy enemy)
        {
            string msg;
            string damagePlayer;
            string damageEnemy;

            if (IsDodge(player.Accuracy, enemy.Evation))
                damagePlayer = "Противник уклонился от вашей атаки\n";
            else
            {
                int dmg = player.Damage - enemy.Defence;
                damagePlayer = $"⚔Вы нанесли {dmg} урона\n";
                enemyRepository.ReduceHP(enemy, dmg);
            }

            if (IsDodge(player.Accuracy, enemy.Evation))
                damageEnemy = "Вы уклонились от атаки противника\n";
            else
            {
                int dmg = player.Damage - enemy.Defence;
                playerRepository.ReduceHP(player, dmg);
                damageEnemy = $"⚔Противник нанес {dmg} урона\n";
            }
            msg = damagePlayer + damageEnemy + "\n\n" + $"Ваше здоровье:{player.CurrentHP}/{player.HP} \nЗдоровье {enemy.Name}: {enemy.HP}";
            return msg;
        }

        public Enemy SpawnRandomEnemy(int stage)
        {
            var enemies = enemyRepository.GetEnemiesByStages(stage);
            return enemies[random.Next(enemies.Count)];
        }
        private bool IsDodge(double accuracy, double evation)
        {
            var chance = accuracy - evation;
            double randomNum = random.Next(100)/100;
            return chance>randomNum?true:false;
        }

        void IBattleService.Attack(Player player, Enemy enemy)
        {
            throw new NotImplementedException();
        }
    }
}
