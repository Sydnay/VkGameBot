using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrogAnanas.Models
{
    public record Level
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lvl { get; set; }
        public int RequrimentXP { get; set; }
        public List<MasteryPlayer> MasteryPlayers { get; set; }
    }
}