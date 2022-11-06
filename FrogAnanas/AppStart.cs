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
            userRepository.ABOBA();
            bot.OnMessageReceived += HandleStart1;
            bot.OnMessageReceived += HandleGender2;
            bot.OnMessageReceived += HandleCreation3;
            bot.OnMessageReceived += HandlePlayer4;
            bot.OnMessageReceived += HandlePlayerInfo5;
            bot.OnMessageReceived += HandlePlayerInventory6;

            Console.WriteLine("SstartReceiveng");
            bot.Start();
            Console.ReadLine();


        }
        async void HandleStart1(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;
            if (msg == ConstPhrase.start)
            {
                var userId = e.Message.FromId ?? -1;
                var user = await userRepository.GetUserAsync(userId);

                if (user is not null)
                    return;

                var keyboard = new KeyboardBuilder();
                keyboard.AddButton(ConstPhrase.createHero, "", KeyboardButtonColor.Positive);

                await userRepository.AddUser(new User
                {
                    Id = userId,
                    UserEventId = (int)EventType.HandleStart
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

            var msg = e.Message.Text;
            Console.WriteLine($"{msg}");

            if (msg == ConstPhrase.createHero)
            {
                var userId = e.Message.FromId ?? -1;
                var user = await userRepository.GetUserAsync(userId);

                if (user is null)
                    return;
                if (user.UserEventId != (int)EventType.HandleStart)
                    return;

                var keyboard = new KeyboardBuilder();
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
            var msg = e.Message.Text;
            Console.WriteLine($"{msg}");

            if (msg == ConstPhrase.male||msg == ConstPhrase.female)
            {
                var userId = e.Message.FromId ?? -1;
                var user = await userRepository.GetUserAsync(userId);

                if (user is null)
                    return;
                if (user.UserEventId != (int)EventType.HandleGender)
                    return;

                var playerId = await playerRepository.AddPlayer(new Player
                {
                    Name = (await bot.Api.Users.GetAsync(new List<long> { e.Message.FromId ?? (long)-1 })).FirstOrDefault()!.FirstName,
                    Gender = msg == ConstPhrase.female ? Gender.Female : Gender.Male,
                    DPS = 1,
                    Defence = 3,
                    Accuracy = 0.8,
                    Evation = 0.2,
                    CritChance = 0.1,
                    MultipleCrit = 1.1,
                    HP = 50,
                    Initiative = 0.8,
                    Perception = 2
                });

                await userRepository.SetPlayerId(userId, playerId);

                var keyboard = new KeyboardBuilder();
                keyboard.AddButton(ConstPhrase.player, "", KeyboardButtonColor.Default);

                await bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = $"Персоонаж успешно создан!",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                    Keyboard = keyboard.Build(),
                });

                await userRepository.SetCurrentEvent(userId, EventType.HandleCreation);
                Console.WriteLine($"Сообщение {msg} отправлено");
            }

        }
        async void HandlePlayer4(object? sender, MessageReceivedEventArgs e)
        {

            var msg = e.Message.Text;
            Console.WriteLine($"{msg}");

            if (msg == ConstPhrase.player)
            {
                var userId = e.Message.FromId ?? -1;
                var user = await userRepository.GetUserWithPlayerAsync(userId);

                if (user is null || user.Player is null)
                    return;

                var keyboard = new KeyboardBuilder();
                keyboard.AddButton(ConstPhrase.playerInfo, "", KeyboardButtonColor.Positive)
                        .AddButton(ConstPhrase.playerInventory, "", KeyboardButtonColor.Negative);

                await bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = "Выберите",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                    Keyboard = keyboard.Build()
                });

                await userRepository.SetCurrentEvent(userId, EventType.HandlePlayer);
                Console.WriteLine($"Сообщение {msg} отправлено");
            }
        }
        async void HandlePlayerInfo5(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;
            Console.WriteLine($"{msg}");

            if (msg == ConstPhrase.playerInfo)
            {
                var userId = e.Message.FromId ?? -1;
                var user = await userRepository.GetUserWithPlayerAsync(userId);

                if (user is null || user.Player is null)
                    return;
                if (user.UserEventId != (int)EventType.HandlePlayer)
                    return;

                var keyboard = new KeyboardBuilder();
                keyboard.AddButton(ConstPhrase.player, "", KeyboardButtonColor.Default);

                await bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = $"Имя: {user.Player.Name}\n Атака:{user.Player.DPS}\n Защита:{user.Player.Defence}\n Здоровье: {user.Player.HP}\n Sex:{user.Player.Gender}",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                    Keyboard = keyboard.Build()
                });

                await userRepository.SetCurrentEvent(userId, EventType.HandlePlayerInfo);
                Console.WriteLine($"Сообщение {msg} отправлено");
            }
        }
        async void HandlePlayerInventory6(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;
            Console.WriteLine($"{msg}");

            if (msg == ConstPhrase.playerInventory)
            {
                var userId = e.Message.FromId ?? -1;
                var user = await userRepository.GetUserWithPlayerAsync(userId);

                if (user is null || user.Player is null)
                    return;

                if (user.UserEventId != (int)EventType.HandlePlayer)
                    return;

                await bot.Api.Messages.SendAsync(new MessagesSendParams
                {
                    Message = $"Здесь будет инвентарь и не будет кнопки персонажа",
                    PeerId = e.Message.PeerId,
                    RandomId = Math.Abs(Environment.TickCount),
                });

                await userRepository.SetCurrentEvent(userId, EventType.HandlePlayerInventory);
                Console.WriteLine($"Сообщение {msg} отправлено");
            }
        }
    }
}
