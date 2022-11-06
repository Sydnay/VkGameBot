using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public class UserEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
    public enum EventType
    {
        HandleStart = 1,
        HandleGender = 2,
        HandleCreation = 3,
    }
}
