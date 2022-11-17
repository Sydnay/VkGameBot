using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public interface IEnemyRepository
    {
        Enemy GetEnemy(int id);
        Enemy SpawnRandomEnemy(int stage);
        ResourcesPlayer GenerateResourceFromEnemy(long userId, int enemyId);
        ItemsPlayer GenerateItemFromEnemyOrDefault(long userId, int enemyId);
    }
}
