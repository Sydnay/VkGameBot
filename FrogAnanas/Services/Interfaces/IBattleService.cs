using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Services
{
    public interface IBattleService
    {
        void Attack(Player player, Enemy enemy);
        Enemy SpawnRandomEnemy(int stage);
    }
}
