using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrogAnanas.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Damage { get; set; }
        public double MultipleCrit { get; set; }
        public double CritChance { get; set; }
        public double Accuracy { get; set; }
        public double Evation { get; set; }
        public int Defence { get; set; }
        public int CurrentHP { get; set; }
        public int HP { get; set; }
        public double Initiative { get; set; }
        public int Perception { get; set; }

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