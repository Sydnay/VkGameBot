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
        public PlayerInfoHandler(LowPlayerHandler handler, IUserRepository userRepository)
        {
            this.handler = handler;
            this.userRepository = userRepository;
        }
        public async void HandlePlayerInfo(User user, object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;

            switch (msg, user.UserEventId)
            {
                case (ConstPhrase.player, >0) :
                    handler.HandlePlayer4(sender, e);
                    userRepository.SetCurrentEvent(user.Id, EventType.HandlePlayer);
                    break;

                case (ConstPhrase.playerInfo, (int)EventType.HandlePlayer):
                    handler.HandlePlayerInfo5(user, sender, e);
                    userRepository.SetCurrentEvent(user.Id, EventType.HandlePlayerInfo);
                    break;

                case (ConstPhrase.playerInventory, (int)EventType.HandlePlayer):
                    handler.HandlePlayerInventory6(sender, e);
                    userRepository.SetCurrentEvent(user.Id, EventType.HandlePlayerInventory);
                    break;

                default:
                    break;
            }
        }
    }
}
