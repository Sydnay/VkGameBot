using FrogAnanas.Constants;
using FrogAnanas.Helpers;
using FrogAnanas.Models;
using FrogAnanas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Handlers.JuniorLevelHandlers
{
    public class LowPlayerHandler
    {
        private readonly IUserRepository userRepository;
        public LowPlayerHandler(IUserRepository repository)
        {
            this.userRepository = repository;
        }
        public async void HandlePlayer4(object? sender, MessageReceivedEventArgs e)
        {
            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = "Выберите",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, ConstPhrase.playerInfo, ConstPhrase.playerInventory)
            });
        }

        public async void HandlePlayerInfo5(User user, object? sender, MessageReceivedEventArgs e)
        {
            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = $"Имя: {user.Player.Name}\n Атака:{user.Player.DPS}\n Защита:{user.Player.Defence}\n Здоровье: {user.Player.HP}\n Sex:{user.Player.Gender}",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, ConstPhrase.player)
            });
        }

        public async void HandlePlayerInventory6(object? sender, MessageReceivedEventArgs e)
        {
            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = $"Здесь будет инвентарь и не будет кнопки персонажа",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, ConstPhrase.player)
            });
        }
    }
}
