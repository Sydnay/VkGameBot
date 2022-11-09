using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public class MasteryPlayer
    {
        public int MasteryId { get; set; }
        public Mastery Mastery { get; set; }
        public long UserId { get; set; }
        public Player Player { get; set; }
        public int CurrentXP { get; set; } = 0;
        public int CurrentLvl { get; set; } = 1;
        public Level CurrentLevelCLASS { get; set; }
    }
}
