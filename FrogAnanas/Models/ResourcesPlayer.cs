using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public class ResourcesPlayer
    {
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
        public long UserId { get; set; }
        public Player Player { get; set; }
        public int Amount { get; set; }
    }
}
