using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Events
{
    public class FightEnemy
    {
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }
    }
}
