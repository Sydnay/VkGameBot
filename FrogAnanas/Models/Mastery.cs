using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public record Mastery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = "Какое-то описание мастерки";
        public List<Skill> Skills { get; set; }
    }
}
