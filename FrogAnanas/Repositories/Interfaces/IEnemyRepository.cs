﻿using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public interface IEnemyRepository
    {
        Enemy SpawnRandomEnemy(int stage);
    }
}