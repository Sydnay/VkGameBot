using FrogAnanas.Constants;
using FrogAnanas.Helpers;
using FrogAnanas.Repositories;

namespace FrogAnanas.Handlers.JuniorLevelHandlers
{
    public class LowAdventureHandler
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IStageRepository stageRepository;
        public LowAdventureHandler(IPlayerRepository repository, IStageRepository stageRepository)
        {
            this.playerRepository = repository;
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
                Keyboard = KeyboardHelper.CreateOneColumns(KeyboardButtonColor.Default, AdventurePhrase.COME_TOWER, AdventurePhrase.TOWER_INFO, AdventurePhrase.GO_BACK_TOWN)
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
                Keyboard = KeyboardHelper.CreateOneColumns(KeyboardButtonColor.Default, AdventurePhrase.COME_TOWER, AdventurePhrase.TOWER_INFO, AdventurePhrase.GO_BACK_TOWN)
            });
        }
        public async void HandleCloseTower4(int playerStage, object? sender, MessageReceivedEventArgs e)
        {
            var keyboard = new KeyboardBuilder();
            keyboard.AddButton("Войти в башню", "", KeyboardButtonColor.Primary);
            //11,21,31
            while (playerStage % 10 != 1)
                playerStage--;

            for (int i = playerStage; i > 10; i -= 10)
            {
                keyboard.AddLine().AddButton($"Войти на {i} этаж", "", i < 50 ? KeyboardButtonColor.Positive : KeyboardButtonColor.Negative);
            }

            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = "Какое-то сообщение",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = keyboard.Build()
            });
        }
        public async void HandleEnterTower5(object? sender, MessageReceivedEventArgs e)
        {

            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.ENTER_TOWER,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateOneColumns(KeyboardButtonColor.Default, TowerPhrase.GO_FORWARD)
            });
        }
        public async void HandleEnterStageTower5(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.ENTER_STAGE_TOWER,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateOneColumns(KeyboardButtonColor.Default, TowerPhrase.GO_FORWARD)
            });
        }
    }
}
