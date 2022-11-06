namespace FrogAnanas.Models
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int DPS { get; set; }
        public double MultipleCrit { get; set; }
        public double CritChance { get; set; }
        public double Accuracy { get; set; }
        public double Evation { get; set; }
        public int Defence { get; set; }
        public int HP { get; set; }
        public double Initiative { get; set; }
        public int Perception { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Unspecified
    }
}