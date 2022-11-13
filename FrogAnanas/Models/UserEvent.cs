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
        public List<Player> Players { get; set; }
    }
    public enum EventType
    {
        HandleStart = 1,
        HandleGender,
        HandleCreation,
        HandleFirstRole,
        HandleAcceptFirstRole,

        HandlePlayer,
        HandlePlayerInfo,
        HandlePlayerInventory,

        HandleStartAdventure,
        HandleCity,
        HandleTower,
        HandleEnterTower,
    }
}
