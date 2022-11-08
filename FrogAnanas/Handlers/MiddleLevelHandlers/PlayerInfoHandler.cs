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
                case (ConstPhrase.player, not (int)EventType.HandlePlayer):
                    playerRepository.SetCurrentEvent(player.UserId, EventType.HandlePlayer);
                    handler.HandlePlayer4(sender, e);
                    break;

                case (ConstPhrase.playerInfo, (int)EventType.HandlePlayer):
                    playerRepository.SetCurrentEvent(player.UserId, EventType.HandlePlayerInfo);
                    handler.HandlePlayerInfo5(player, sender, e);
                    break;

                case (ConstPhrase.playerInventory, (int)EventType.HandlePlayer):
                    playerRepository.SetCurrentEvent(player.UserId, EventType.HandlePlayerInventory);
                    handler.HandlePlayerInventory6(sender, e);
                    break;

                default:
                    break;
            }
        }
    }
}
