using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = "Какое-то описание скила";
        public int LvlRequirement { get; set; }
        public int MasteryId { get; set; }
        public Mastery Mastery { get; set; }

    }
}
