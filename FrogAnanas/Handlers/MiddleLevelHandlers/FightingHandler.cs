using FrogAnanas.Constants;
using FrogAnanas.Context;
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
    public class FightingHandler
    {
        private readonly LowFightingHandler handler;
        private readonly IPlayerRepository playerRepository;
        public FightingHandler(LowFightingHandler handler, IPlayerRepository playerRepository)
        {
            this.handler = handler;
            this.playerRepository = playerRepository;
        }
        public async void HandleFighting(Player player, object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;

            switch (msg, player.UserEventId)
            {
                case (FightPhrase.ATTACK, (int)EventType.HandleBattle):
                    handler.HandleAttack1(player.UserId, sender, e);
                    break;
                default:
                    break;
            }

            if(player.UserEventId == (int)EventType.HandleEndBattle)
            {
                playerRepository.SetEvent(player.UserId, EventType.HandleForward);
                handler.HandleEndBattle2(player.UserId, sender, e);
            }
        }
    }
}
