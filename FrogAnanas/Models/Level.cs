using System.ComponentModel.DataAnnotations.Schema;

namespace FrogAnanas.Models
{
    public class Level
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int ExpRequired { get; set; }
        public List<Player> Players { get; set; }
    }
}