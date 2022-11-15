using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public class Enemy
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
        public double MultipleCrit { get; set; }
        public double CritChance { get; set; }
        public double Accuracy { get; set; }
        public double Evation { get; set; }
        public int Defence { get; set; }
        public int HP { get; set; }
        public double Initiative { get; set; }
        public int GivenXP { get; set; }

        public List<ItemsEnemy> ItemsEnemies { get; set; }
        public List<ResourcesEnemy> ResourcesEnemies { get; set; }
        public List<Stage> Stages { get; set; }
    }
}
