namespace FrogAnanas.Models
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Unspecified
    }
}