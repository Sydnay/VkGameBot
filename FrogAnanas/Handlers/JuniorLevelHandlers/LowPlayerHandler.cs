using FrogAnanas.Constants;
using FrogAnanas.Helpers;
using FrogAnanas.Models;
using FrogAnanas.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Handlers.JuniorLevelHandlers
{
    public class LowPlayerHandler
    {
        private readonly IPlayerRepository playerRepository;
        public LowPlayerHandler(IPlayerRepository repository)
        {
            this.playerRepository = repository;
        }
        public async void HandlePlayer4(Player player, object? sender, MessageReceivedEventArgs e)
        {
            Log.Information($"Игрок {e.Message.FromId} узнает инфу о персонаже");
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = $"Здесь вы можете узнать основную информацию по вашему персонажу\nИмя: {player.Name}\n Атака:{player.Damage}\n Защита:{player.Defence}\n Здоровье: {player.HP}\n Наивысший этаж:{player.MaxStage}\n Sex:{player.Gender}",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, PlayerInfoPhrase.playerInventory, PlayerInfoPhrase.playerEquip, PlayerInfoPhrase.playerSkills)
            });
            Log.Information($"Отправили сообщение ");
        }

        public async void HandlePlayerEquip2(object? sender, MessageReceivedEventArgs e)
        {
            Log.Information($"Игрок {e.Message.FromId} экипировка");
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = $"Здесь будет ваша экипировка",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateTwoColumns(KeyboardButtonColor.Default, PlayerInfoPhrase.player, AdventurePhrase.TOWER, AdventurePhrase.BAR, AdventurePhrase.MARKET)
            });
            Log.Information($"Отправили сообщение ");
        }
        public async void HandlePlayerSkills2(long userId, object? sender, MessageReceivedEventArgs e)
        {
            var skills = playerRepository.GetPlayerSkills(userId).Select(x=>x.ToString());

            string msgSkills = String.Join("\n ", skills.ToArray());

            Log.Information($"Игрок {e.Message.FromId} скиллы");
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = msgSkills,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateTwoColumns(KeyboardButtonColor.Default, PlayerInfoPhrase.player, AdventurePhrase.TOWER, AdventurePhrase.BAR, AdventurePhrase.MARKET)
            });
            Log.Information($"Отправили сообщение ");
        }
        public async void HandlePlayerInventory2(long userId, object? sender, MessageReceivedEventArgs e)
        {
            #region Image
            // Получить адрес сервера для загрузки.
            var uploadServer = AppStart.bot.Api.Photo.GetMessagesUploadServer(152276925);
            // Загрузить файл.
            var wc = new WebClient();
            var responseFile = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"C:\Users\warcr\Desktop\Rabota\BOTVKPHOTO\inventory.jpg"));
            var photo = AppStart.bot.Api.Photo.SaveMessagesPhoto(responseFile);
            #endregion
            var resources = playerRepository.GetPlayerResources(userId).Select(x => x.ToString());
            var images = playerRepository.GetPlayerItems(userId).Select(x => x.ToString());

            string msgResources = String.Join("\n ", resources.ToArray());
            string msgImages = String.Join("\n ", images.ToArray());

            Log.Information($"Игрок {e.Message.FromId} инвентарь");
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Attachments = photo,
                Message = $"Ресурсы:\n{msgResources}\nПредметы:\n{msgImages}",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateTwoColumns(KeyboardButtonColor.Default, PlayerInfoPhrase.player, AdventurePhrase.TOWER, AdventurePhrase.BAR, AdventurePhrase.MARKET)
            });
            Log.Information($"Отправили сообщение ");
        }
    }
}
