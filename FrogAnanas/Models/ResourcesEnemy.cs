using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public class ResourcesEnemy
    {
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
        public int EnemyId { get; set; }
        public Enemy Enemy { get; set; }
        public double DropChance { get; set; }
        public int MaxAmount { get; set; }
    }
}
