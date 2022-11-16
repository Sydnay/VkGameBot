using FrogAnanas.Context;
using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public class EnemyRepository : IEnemyRepository
    {
        private Random random = new Random(); 
        private readonly ApplicationContext context;
        public EnemyRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Enemy SpawnRandomEnemy(int stage)
        {
            var enemies = context.Enemies.Where(x=>x.Stages.Select(x=>x.Id).Contains(stage)).ToList();
            return Enemy.Clone(enemies[random.Next(enemies.Count)]);
        }
    }
}
