
global using VkBotFramework;
global using VkBotFramework.Models;
global using VkNet.Enums.SafetyEnums;
global using VkNet.Model.Keyboard;
global using VkNet.Model.RequestParams;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FrogAnanas.Constants;
using FrogAnanas.Context;
using FrogAnanas.Handlers.MiddleLevelHandlers;
using FrogAnanas.Models;
using FrogAnanas.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FrogAnanas
{
    public class AppStart
    {

        public static VkBot bot;

        private readonly RegistrationHandler registrationHandler;
        private readonly PlayerInfoHandler playerInfoHandler;
        private readonly AdventureHandler adventureHandler;
        private readonly TowerHandler towerHandler;
        private readonly IPlayerRepository playerRepository;
        public AppStart(IPlayerRepository playerRepository, RegistrationHandler registrationHandler, PlayerInfoHandler playerInfoHandler, AdventureHandler adventureHandler, TowerHandler towerHandler)
        {
            this.registrationHandler = registrationHandler;
            this.playerInfoHandler = playerInfoHandler;
            this.adventureHandler = adventureHandler;
            this.playerRepository = playerRepository;
            this.towerHandler = towerHandler;
        }
        public async void Start(IConfiguration config)
        {
            bot = new VkBot(config.GetSection("VkBot:Token").Value, config.GetSection("VkBot:GroupUri").Value);

            bot.OnMessageReceived += HandleMessage;
            Log.Information("StartReveiving");
            bot.Start(); 
            
            Console.ReadLine();
        }
        async void HandleMessage(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;

            if (PhrasesType.registrationPhrases.Contains(msg))
            {
                registrationHandler.HandleRegistration(sender, e);
                return;
            }

            var userId = e.Message.FromId ?? -1;
            var player = playerRepository.GetPlayer(userId);

            if (player is null)
                return;

            if (PhrasesType.playerInfoPhrases.Contains(msg))
                playerInfoHandler.HandlePlayerInfo(player, sender, e);
            if (PhrasesType.adventurePhrases.Contains(msg))
                adventureHandler.HandleAdventure(player, sender, e);
            if (PhrasesType.towerPhrases.Contains(msg))
                towerHandler.HandleTower(player, sender, e);
        }
    }
}
