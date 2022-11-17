using FrogAnanas.Context;
using FrogAnanas.Models;

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

        public Enemy GetEnemy(int id)
        {
            throw new NotImplementedException();
        }
        public Enemy SpawnRandomEnemy(int stage)
        {
            var enemies = context.Enemies.Where(x => x.Stages.Select(x => x.Id).Contains(stage)).ToList();
            return Enemy.Clone(enemies[random.Next(enemies.Count)]);
        }
        public ResourcesPlayer GenerateResourceFromEnemy(long userId, int enemyId)
        {
            var dropList = context.ResourcesEnemies.Where(x => x.EnemyId == enemyId).ToList();
            var resource = dropList[random.Next(dropList.Count)];
            var drop = new ResourcesPlayer
            {
                ResourceId = resource.ResourceId,
                UserId = userId,
                Amount = CalculateDrop(resource.MaxAmount, resource.DropChance)
            };

            return drop;
        }
        public ItemsPlayer GenerateItemFromEnemyOrDefault(long userId, int enemyId)
        {
            var dropList = context.ItemsEnemies.Where(x => x.EnemyId == enemyId).ToList();
            if (dropList.Count == 0)
                return null;
            var item = dropList[random.Next(dropList.Count)];
            if (CalculateDrop(1, item.DropChance) > 0)
                return new ItemsPlayer
                {
                    ItemId = item.ItemId,
                    UserId = userId,
                };
            return null;
        }

        private int CalculateDrop(int dropMaxAmount, double dropChance)
        {
            int amount = 0;
            for (int i = 0; i < dropMaxAmount; i++)
            {
                double randomNum = random.Next(100) / 100d;
                amount = dropChance > randomNum ? amount + 1 : amount;
            }
            return amount;
        }
    }
}
