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
        private readonly IUserRepository userRepository;
        private readonly IPlayerRepository playerRepository;
        public PlayerInfoHandler(LowPlayerHandler handler, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            this.handler = handler;
            this.userRepository = userRepository;
            this.playerRepository = playerRepository;
        }
        public async void HandlePlayerInfo(User user, object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;
            var player = playerRepository.GetPlayer(user.PlayerId);

            switch (msg, user.UserEventId)
            {
                case (ConstPhrase.player, not (int)EventType.HandlePlayer):
                    userRepository.SetCurrentEvent(user.Id, EventType.HandlePlayer);
                    handler.HandlePlayer4(sender, e);
                    break;

                case (ConstPhrase.playerInfo, (int)EventType.HandlePlayer):
                    userRepository.SetCurrentEvent(user.Id, EventType.HandlePlayerInfo);
                    handler.HandlePlayerInfo5(player, sender, e);
                    break;

                case (ConstPhrase.playerInventory, (int)EventType.HandlePlayer):
                    userRepository.SetCurrentEvent(user.Id, EventType.HandlePlayerInventory);
                    handler.HandlePlayerInventory6(sender, e);
                    break;

                default:
                    break;
            }
        }
    }
}
