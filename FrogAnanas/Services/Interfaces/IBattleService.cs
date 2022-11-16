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
        string Attack(long userId);
        bool isEnemyDead(long userId);
        bool isPlayerDead(long userId);
    }
}
