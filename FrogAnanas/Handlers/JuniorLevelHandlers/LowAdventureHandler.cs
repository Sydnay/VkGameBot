using FrogAnanas.Constants;
using FrogAnanas.Helpers;
using FrogAnanas.Repositories;

namespace FrogAnanas.Handlers.JuniorLevelHandlers
{
    public class LowAdventureHandler
    {
        private readonly IPlayerRepository userRepository;
        public LowAdventureHandler(IPlayerRepository repository)
        {
            this.userRepository = repository;
        }
        public async void HandleStartAdventure1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.START_ADVENTURE,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, AdventurePhrase.GREHOVKA)
            });
        }

        public async void HandleCity2(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.GREHOVKA,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateOneColumns(KeyboardButtonColor.Default, PlayerInfoPhrase.player, AdventurePhrase.TOWER, AdventurePhrase.BAR, AdventurePhrase.MARKET)
            });
        }
        public async void HandleBar3(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.BAR,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateTwoColumns(KeyboardButtonColor.Default, PlayerInfoPhrase.player, AdventurePhrase.TOWER, AdventurePhrase.BAR, AdventurePhrase.MARKET)
            });
        }
        public async void HandleMarket3(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.MARKET,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateTwoColumns(KeyboardButtonColor.Default, PlayerInfoPhrase.player, AdventurePhrase.TOWER, AdventurePhrase.BAR, AdventurePhrase.MARKET)
            });
        }

        public async void HandleTower3(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.TOWER,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateOneColumns(KeyboardButtonColor.Default, AdventurePhrase.ENTER_TOWER, AdventurePhrase.TOWER_INFO, AdventurePhrase.GO_BACK_TOWN)
            });
        }
        public async void HandleGoBackTown4(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.GO_BACK_TOWN,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateTwoColumns(KeyboardButtonColor.Default, PlayerInfoPhrase.player, AdventurePhrase.TOWER, AdventurePhrase.BAR, AdventurePhrase.MARKET)
            });
        }
        public async void HandleTowerInfo4(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = "Здесь будет инфа по пройденным этажам",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateOneColumns(KeyboardButtonColor.Default, AdventurePhrase.ENTER_TOWER, AdventurePhrase.TOWER_INFO, AdventurePhrase.GO_BACK_TOWN)
            });
        }
        public async void HandleEnterTower4(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.ENTER_TOWER,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, "В разработке")
            });
        }
    }
}
