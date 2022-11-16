using FrogAnanas.Constants;
using FrogAnanas.Context;
using FrogAnanas.Helpers;
using FrogAnanas.Models;
using FrogAnanas.Repositories;
using FrogAnanas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Handlers.JuniorLevelHandlers
{
    public class LowFightingHandler
    {
        private readonly IPlayerRepository playerRepository;
        private readonly MongoDbRepository eventRepository;
        private readonly IBattleService battleService;
        public LowFightingHandler(IPlayerRepository repository, MongoDbRepository eventRepository, IBattleService battleService)
        {
            this.playerRepository = repository;
            this.eventRepository = eventRepository;
            this.battleService = battleService;
        }
        public async void HandleAttack1(long userId, object? sender, MessageReceivedEventArgs e)
        {
            var msg = battleService.Attack(userId);
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = msg,
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
            });
            if (battleService.isEnemyDead(userId))
                playerRepository.SetEvent(userId, EventType.HandleEndBattle);
        }
        public async void HandleEndBattle2(long userId, object? sender, MessageReceivedEventArgs e)
        {
            eventRepository.CloseEvent(userId);
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = "Награда и всякая хуйня",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, TowerPhrase.GO_FORWARD)
            });
        }
    }
}
