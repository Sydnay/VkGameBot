
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
using Microsoft.Extensions.Logging;
using Serilog;

namespace FrogAnanas
{
    public class AppStart
    {
        static AppSettingsSection config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings;
        public static readonly VkBot bot = new VkBot(config.Settings["groupToken"].Value, config.Settings["groupUri"].Value);

        private readonly RegistrationHandler registrationHandler;
        private readonly PlayerInfoHandler playerInfoHandler;
        private readonly AdventureHandler adventureHandler;
        private readonly IPlayerRepository playerRepository;
        public AppStart(IPlayerRepository playerRepository, RegistrationHandler registrationHandler, PlayerInfoHandler playerInfoHandler, AdventureHandler adventureHandler)
        {
            this.registrationHandler = registrationHandler;
            this.playerInfoHandler = playerInfoHandler;
            this.adventureHandler = adventureHandler;
            this.playerRepository = playerRepository;
        }
        public async void Start()
        {
            bot.OnMessageReceived += HandleMessage;

            Log.Information("StartReveiving");
            Console.WriteLine("SstartReceiveng");
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
        }
    }
}
