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
        public async void Start()
        {
            bot.OnMessageReceived += HandleStart1;
            bot.OnMessageReceived += HandleGender2;
            bot.OnMessageReceived += HandleCreation3;

            Console.WriteLine("SstartReceiveng");
            bot.Start();
            Console.ReadLine();
        }
        async void HandleStart1(object? sender, MessageReceivedEventArgs e)
        {
            var userId = e.Message.FromId ?? -1;
            var user = await userRepository.GetUser(userId);

            if (user is not null)
                return;

            var msg = e.Message.Text;
            if (msg != ConstPhrase.start)
            {
                var keyboard = new KeyboardBuilder(true);
                keyboard.AddButton(ConstPhrase.start, "", KeyboardButtonColor.Default);

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
                var keyboard = new KeyboardBuilder(true);
                keyboard.AddButton(ConstPhrase.createHero, "", KeyboardButtonColor.Positive);

                await userRepository.AddUser(new User
                {
                    Id = userId,
                    EventId = (int)EventType.HandleStart
                });

                await bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = "Добро пожаловать узбек \nДавайте создадим персоонажа",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                    Keyboard= keyboard.Build()
                });

                Console.WriteLine($"Сообщение отправлено");
            }

        }

        async void HandleGender2(object? sender, MessageReceivedEventArgs e)
        {
            var userId = e.Message.FromId ?? -1;
            var user = await userRepository.GetUser(userId);

            if (user is null)
                return;

            var msg = e.Message.Text;
            Console.WriteLine($"{msg}");

            if (msg == ConstPhrase.createHero && user.EventId == (int)EventType.HandleStart)
            {
                var keyboard = new KeyboardBuilder(true);
                keyboard.AddButton(ConstPhrase.male, "", KeyboardButtonColor.Positive)
                        .AddButton(ConstPhrase.female, "", KeyboardButtonColor.Negative);

                await bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = "Выберите пол",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                    Keyboard = keyboard.Build()
                });

                await userRepository.SetCurrentEvent(userId, EventType.HandleGender);
                Console.WriteLine($"Сообщение {msg} отправлено");
            }

        }

        async void HandleCreation3(object? sender, MessageReceivedEventArgs e)
        {
            var userId = e.Message.FromId ?? -1;
            var user = await userRepository.GetUser(userId);

            if (user is null)
                return;

            var msg = e.Message.Text;
            Console.WriteLine($"{msg}");

            if ((msg == ConstPhrase.male||msg == ConstPhrase.female) && user.Id == (int)EventType.HandleGender  /*&& UserEvent == "айди события битвы с боссом"*/)
            {
                var playerId = await playerRepository.AddPlayer(new Player
                {
                    Name = (await bot.Api.Users.GetAsync(new List<long> { e.Message.FromId ?? (long)-1 })).FirstOrDefault()!.FirstName,
                    Gender = msg == ConstPhrase.female ? Gender.Female : Gender.Male,
                });

                await userRepository.SetPlayerId(userId, playerId);

                await bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = "Персоонаж успешно создан",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                });

                await userRepository.SetCurrentEvent(userId, EventType.HandleCreation);
                Console.WriteLine($"Сообщение {msg} отправлено");
            }

        }
    }
}
