using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public class ItemsEnemy
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int EnemyId { get; set; }
        public Enemy Enemy { get; set; }
        public double DropChance { get; set; }
    }
}
