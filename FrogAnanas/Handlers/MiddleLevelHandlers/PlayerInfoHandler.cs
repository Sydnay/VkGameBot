using FrogAnanas.Constants;
using FrogAnanas.Handlers.JuniorLevelHandlers;
using FrogAnanas.Models;
using FrogAnanas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Handlers.MiddleLevelHandlers
{
    public class PlayerInfoHandler
    {
        private readonly LowPlayerHandler handler;
        private readonly IPlayerRepository playerRepository;
        public PlayerInfoHandler(LowPlayerHandler handler, IPlayerRepository playerRepository)
        {
            this.handler = handler;
            this.playerRepository = playerRepository;
        }
        public async void HandlePlayerInfo(Player player, object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;

            switch (msg, player.UserEventId)
            {
                case (PlayerInfoPhrase.player, not (int)EventType.HandlePlayer):
                    playerRepository.SetEvent(player.UserId, EventType.HandlePlayer);
                    handler.HandlePlayer4(player, sender, e);
                    break;

                case (PlayerInfoPhrase.playerInventory, (int)EventType.HandlePlayer):
                    playerRepository.SetEvent(player.UserId, EventType.HandleCity);
                    handler.HandlePlayerInventory2(player.UserId, sender, e);
                    break;
                case (PlayerInfoPhrase.playerSkills, (int)EventType.HandlePlayer):
                    playerRepository.SetEvent(player.UserId, EventType.HandleCity);
                    handler.HandlePlayerSkills2(player.UserId, sender, e);
                    break;
                case (PlayerInfoPhrase.playerEquip, (int)EventType.HandlePlayer):
                    playerRepository.SetEvent(player.UserId, EventType.HandleCity);
                    handler.HandlePlayerEquip2(sender, e);
                    break;

                default:
                    break;
            }
        }
    }
}
