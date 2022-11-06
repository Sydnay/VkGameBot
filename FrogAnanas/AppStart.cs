using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrogAnanas.Context;
using FrogAnanas.Models;
using FrogAnanas.Repositories;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace FrogAnanas
{
    public class AppStart
    {
        const string token = "vk1.a.7KyPdYKqp5ANTIBuBFlNKW3wXvLJFE3AsVoc8zm-mmU8KcInZ6-EgrhXxbdp17HSt6Q52gllyfFp2JynQ6ZGBWWYhDyfsE9S2ir9APQNNtL_dglHec77iUB2fSFBd7cRmRhpxeoVnQvTbdIa225E8PSPb6YNytUNiybnK9aIyjN6Q-oHT_F3bopuEOE6_81t5x82gbgr3tlkmYbkoPHvlA";
        const string groupUri = "https://vk.com/club216986922";
        private readonly VkBot bot = new VkBot(token, groupUri);
        private readonly IUserRepository userRepository;
        private readonly IPlayerRepository playerRepository;
        public AppStart(IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            this.userRepository = userRepository;
            this.playerRepository = playerRepository;
        }
        public async void cacheUsers()
        {
            var allUsers = userRepository.GetAllUsers().Result;

            Console.WriteLine(allUsers);
        }
        public async void Start()
        {
            bot.OnMessageReceived += HandleStart;
            bot.OnMessageReceived += HandleMessage;
            bot.OnMessageReceived += HandleMessage1;

            cacheUsers();
            Console.WriteLine("SstartReceiveng");
            bot.Start();
            Console.ReadLine();
            
        }

       
        async void HandleStart(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;
            if (msg != "/start")
            {
                var keyboard = new KeyboardBuilder(true);
                keyboard.AddButton("/start", "", KeyboardButtonColor.Default);
                await bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = "Нажмите /start чтобы начать",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                    Keyboard = keyboard.Build()
                });
                Console.WriteLine($"Сообщение отправлено");
            }
            else
            {
                var userId = e.Message.FromId ?? -1;
                var isExist = await userRepository.IsExist(userId);

                if (isExist)
                {
                    Console.WriteLine(isExist);
                    return;
                }

                var id = await playerRepository.AddPlayer(new Player
                {
                    Name = (await bot.Api.Users.GetAsync(new List<long> { e.Message.FromId ?? (long)-1 })).FirstOrDefault()!.FirstName,
                    Gender = Gender.Female
                });

                await userRepository.AddUser(new User
                {
                    Id = userId,
                    PlayerId = id,
                });

                await bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = "Добро пожаловать узбек",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                });

                Console.WriteLine($"Сообщение отправлено");
            }

        }

        void HandleMessage(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;
            Console.WriteLine($"{msg}");
            if (msg == "ping")
            {
                bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = $"Message: {e.Message.Text} \nChatId: {e.Message.ChatId} \n UserId:{e.Message.UserId} \n FromId:{e.Message.FromId} \n PeerId:{e.Message.PeerId}",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                });
            }

            Console.WriteLine($"Сообщение {msg} отправлено");
        }

        void HandleMessage1(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;
            Console.WriteLine($"{msg}");

            if (msg == "Добить босса" /*&& UserEvent == "айди события битвы с боссом"*/)
            {
                bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = $"второй хендлер",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                });
            }

            else
            {
                bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = "иди нахуй",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                });
            }

            Console.WriteLine($"Сообщение {msg} отправлено");
        }
    }
}
