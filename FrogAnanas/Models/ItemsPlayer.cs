using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public class ItemsPlayer
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public long UserId { get; set; }
        public Player Player { get; set; }
    }
}
