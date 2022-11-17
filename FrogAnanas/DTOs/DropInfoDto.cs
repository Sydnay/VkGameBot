using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.DTOs
{
    public class DropInfoDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int FreeSlotAmount { get; set; }
        public override string ToString()
        {
            return $"{Name} {Amount} шт";
        }
    }
}
