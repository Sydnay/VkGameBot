
global using VkBotFramework;
global using VkBotFramework.Models;
global using VkNet.Enums.SafetyEnums;
global using VkNet.Model.Keyboard;
global using VkNet.Model.RequestParams;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogAnanas.Constants;
using FrogAnanas.Context;
using FrogAnanas.Handlers.MiddleLevelHandlers;
using FrogAnanas.Models;
using FrogAnanas.Repositories;

namespace FrogAnanas
{
    public class AppStart
    {
        const string token = "vk1.a.7KyPdYKqp5ANTIBuBFlNKW3wXvLJFE3AsVoc8zm-mmU8KcInZ6-EgrhXxbdp17HSt6Q52gllyfFp2JynQ6ZGBWWYhDyfsE9S2ir9APQNNtL_dglHec77iUB2fSFBd7cRmRhpxeoVnQvTbdIa225E8PSPb6YNytUNiybnK9aIyjN6Q-oHT_F3bopuEOE6_81t5x82gbgr3tlkmYbkoPHvlA";
        const string groupUri = "https://vk.com/club216986922";
        public static readonly VkBot bot = new VkBot(token, groupUri);
        private readonly IUserRepository userRepository;
        private readonly RegistrationHandler registrationHandler;
        private readonly PlayerInfoHandler playerInfoHandler;
        public AppStart(IUserRepository userRepository, IPlayerRepository playerRepository, RegistrationHandler registrationHandler, PlayerInfoHandler playerInfoHandler)
        {
            this.userRepository = userRepository;
            this.registrationHandler = registrationHandler;
            this.playerInfoHandler = playerInfoHandler;
        }
        public async void Start()
        {
            userRepository.ABOBA();
            bot.OnMessageReceived += HandleMessage;

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
            var user = userRepository.GetUserWithPlayerAsync(userId);

            if (user is null || user.PlayerId is null)
                return;

            if (PhrasesType.playerInfoPhrases.Contains(msg))
                playerInfoHandler.HandlePlayerInfo(user, sender, e);
        }
    }
}
