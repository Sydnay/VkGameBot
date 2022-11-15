using FrogAnanas.Constants;
using FrogAnanas.Helpers;
using FrogAnanas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Handlers.JuniorLevelHandlers
{
    public class LowTowerHandler
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IStageRepository stageRepository;
        public LowTowerHandler(IPlayerRepository repository, IStageRepository stageRepository)
        {
            this.playerRepository = repository;
        }
        public async void HandleForward1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.GO_FORWARD,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, TowerPhrase.GO_FORWARD)
            });
        }
        public async void HandleForwardBattle1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.BATTLE,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, TowerPhrase.BATTLE)
            });
        }
        public async void HandleForwardHBattle1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.HARD_BATTLE,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, TowerPhrase.BATTLE)
            });
        }
        public async void HandleForwardBoss1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.BOSS,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, TowerPhrase.BATTLE)
            });
        }
        public async void HandleForwardEscape1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = Message.ESCAPE,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, TowerPhrase.ESCAPE)
            });
        }
        public async void HandleBattle1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = "Типа тут драка какая-то прошла",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, TowerPhrase.GO_FORWARD)
            });
        }
        public async void HandleHardBattle1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = "Типа тут ЖЕСКАЯ драка какая-то прошла",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, TowerPhrase.GO_FORWARD)
            });
        }
        public async void HandleBoss1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = "Типа тут драка с боссом прошла",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, TowerPhrase.ESCAPE)
            });
        }
        public async void HandleEscape1(object? sender, MessageReceivedEventArgs e)
        {
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = "Типа тут по съебам",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, AdventurePhrase.GREHOVKA)
            });
        }
    }
}