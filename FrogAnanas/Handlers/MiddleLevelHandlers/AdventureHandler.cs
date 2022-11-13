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
    public class AdventureHandler
    {
        private readonly LowAdventureHandler handler;
        private readonly IPlayerRepository playerRepository;
        public AdventureHandler(LowAdventureHandler handler, IPlayerRepository playerRepository)
        {
            this.handler = handler;
            this.playerRepository = playerRepository;
        }
        public async void HandleAdventure(Player player, object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;

            switch (msg, player.UserEventId)
            {
                case (AdventurePhrase.START_ADVENTURE, (int)EventType.HandleAcceptFirstRole):
                    playerRepository.SetEvent(player.UserId, EventType.HandleStartAdventure);
                    handler.HandleStartAdventure1(sender, e);
                    break;
                case (AdventurePhrase.GREHOVKA, (int)EventType.HandleStartAdventure):
                    playerRepository.SetEvent(player.UserId, EventType.HandleCity);
                    handler.HandleCity2(sender, e);
                    break;
                case (AdventurePhrase.BAR, (int)EventType.HandleCity):
                    handler.HandleBar3(sender, e);
                    break;
                case (AdventurePhrase.MARKET, (int)EventType.HandleCity):
                    handler.HandleMarket3(sender, e);
                    break;
                case (AdventurePhrase.TOWER, (int)EventType.HandleCity):
                    playerRepository.SetEvent(player.UserId, EventType.HandleTower);
                    handler.HandleTower3(sender, e);
                    break;
                case (AdventurePhrase.GO_BACK_TOWN, (int)EventType.HandleTower):
                    playerRepository.SetEvent(player.UserId, EventType.HandleCity);
                    handler.HandleGoBackTown4(sender, e);
                    break;
                case (AdventurePhrase.TOWER_INFO, (int)EventType.HandleTower):
                    handler.HandleTowerInfo4(sender, e);
                    break;
                case (AdventurePhrase.COME_TOWER, (int)EventType.HandleTower):
                    playerRepository.SetEvent(player.UserId, EventType.HandleCloseTower);
                    handler.HandleCloseTower4(player.MaxStage, sender, e);
                    break;
                case (AdventurePhrase.ENTER_TOWER, (int)EventType.HandleCloseTower):
                    playerRepository.SetEvent(player.UserId, EventType.HandleEnterTower);
                    handler.HandleEnterTower5(sender, e);
                    break;
                case (AdventurePhrase.ENTER_STAGE_TOWER11 or AdventurePhrase.ENTER_STAGE_TOWER21 or AdventurePhrase.ENTER_STAGE_TOWER31 or AdventurePhrase.ENTER_STAGE_TOWER41 or AdventurePhrase.ENTER_STAGE_TOWER51 or AdventurePhrase.ENTER_STAGE_TOWER61 or AdventurePhrase.ENTER_STAGE_TOWER71 or AdventurePhrase.ENTER_STAGE_TOWER81 or AdventurePhrase.ENTER_STAGE_TOWER91
                , (int)EventType.HandleCloseTower):
                    playerRepository.SetEvent(player.UserId, EventType.HandleEnterTower);
                    handler.HandleEnterStageTower5(sender, e);
                    break;

                default:
                    break;
            }
        }
    }
}
