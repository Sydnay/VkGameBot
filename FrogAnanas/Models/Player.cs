using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrogAnanas.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public int Damage { get; set; } = 6;
        public double MultipleCrit { get; set; } = 1.1;
        public double CritChance { get; set; } = 0.1;
        public double Accuracy { get; set; } = 0.8;
        public double Evation { get; set; } = 0.2;
        public int Defence { get; set; } = 3;
        public int CurrentHP { get; set; } = 50;
        public int HP { get; set; } = 50;
        public double Initiative { get; set; } = 0.8;
        public int Perception { get; set; } = 2;
        public int ResourcesSlotAmount { get; set; } = 15;
        public int? MasteryId { get; set; }
        public Mastery Mastery { get; set; }
        public int UserEventId { get; set; }
        public UserEvent UserEvent { get; set; }
        public int MaxStage { get; set; }
        public Stage Stage { get; set; }
        public List<MasteryPlayer> MasteryPlayers { get; set; }
        public List<ItemsPlayer> ItemsPlayers { get; set; }
        public List<ResourcesPlayer> ResourcesPlayers { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public enum Gender
    {
        Male,
        Female,
        Unspecified
    }
}