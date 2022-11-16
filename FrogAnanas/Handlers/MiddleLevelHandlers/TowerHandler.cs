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
    public class TowerHandler
    {
        private readonly LowTowerHandler handler;
        private readonly IPlayerRepository playerRepository;
        public TowerHandler(LowTowerHandler handler, IPlayerRepository playerRepository)
        {
            this.handler = handler;
            this.playerRepository = playerRepository;
        }
        public async void HandleTower(Player player, object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;

            //TODO: Не реализованы этапы прохода по этажу
            switch (msg, player.UserEventId)
            {
                case (TowerPhrase.GO_FORWARD, (int)EventType.HandleEnterTower):
                    playerRepository.SetEvent(player.UserId, (EventType)UserEvent.GenerateRandomEvent(1));
                    handler.HandleForward1(sender, e);
                    break;
                case (TowerPhrase.GO_FORWARD, (int)EventType.HandleForward):
                    playerRepository.SetEvent(player.UserId, (EventType)UserEvent.GenerateRandomEvent(10));
                    handler.HandleForward1(sender, e);
                    break;
                case (TowerPhrase.GO_FORWARD, (int)EventType.HandleForwardBattle):
                    playerRepository.SetEvent(player.UserId, EventType.HandleBattle);
                    handler.HandleForwardBattle1(sender, e);
                    break;
                case (TowerPhrase.GO_FORWARD, (int)EventType.HandleForwardHardBattle):
                    playerRepository.SetEvent(player.UserId, EventType.HandleHardBattle);
                    handler.HandleForwardHBattle1(sender, e);
                    break;
                case (TowerPhrase.GO_FORWARD, (int)EventType.HandleForwardBoss):
                    playerRepository.SetEvent(player.UserId, EventType.HandleBoss);
                    handler.HandleForwardBoss1(sender, e);
                    break;
                case (TowerPhrase.GO_FORWARD, (int)EventType.HandleForwardEscape):
                    playerRepository.SetEvent(player.UserId, EventType.HandleEscape);
                    handler.HandleForwardEscape1(sender, e);
                    break;

                case (TowerPhrase.BATTLE, (int)EventType.HandleBattle):
                    handler.HandleBattle1(player, sender, e);
                    break;
                case (TowerPhrase.BATTLE, (int)EventType.HandleHardBattle):
                    playerRepository.SetEvent(player.UserId, EventType.HandleForward);
                    handler.HandleHardBattle1(sender, e);
                    break;
                case (TowerPhrase.BATTLE, (int)EventType.HandleBoss):
                    playerRepository.SetEvent(player.UserId, EventType.HandleEscape);
                    handler.HandleBoss1(sender, e);
                    break;
                case (TowerPhrase.ESCAPE, (int)EventType.HandleEscape):
                    playerRepository.SetEvent(player.UserId, EventType.HandleStartAdventure);
                    handler.HandleEscape1(sender, e);
                    break;

                default:
                    break;
            }
        }
    }
}
