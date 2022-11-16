using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Models
{
    public record UserEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
        public static int GenerateRandomEvent(int turn)
        {
            var random = new Random();
            var v = Enum.GetValues(typeof(EventType));
            if (turn <=3)
            {
                return (int)v.GetValue(random.Next((int)EventType.HandleForward, (int)EventType.HandleForwardBattle));
            }
            else if (turn>3&&turn<=6)
            {
                return (int)v.GetValue(random.Next((int)EventType.HandleForward, (int)EventType.HandleForwardHardBattle));
            }
            else
            {
                return (int)v.GetValue(random.Next((int)EventType.HandleForward, (int)EventType.HandleForwardBoss));
            }
        }
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
        HandleCloseTower,
        HandleEnterTower,

        HandleForward,
        HandleForwardBattle,
        HandleForwardHardBattle,
        HandleForwardBoss,
        HandleForwardEscape,
        HandleBattle,
        HandleHardBattle,
        HandleBoss,
        HandleEscape,
        HandleEndBattle,
    }
}
