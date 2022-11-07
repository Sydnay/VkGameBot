using FrogAnanas.Models;
using FrogAnanas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkBotFramework.Models;

namespace FrogAnanas.Handlers.MiddleLevelHandlers
{
    public class RegistrationHandler
    {
        private readonly IUserRepository userRepository;
        private readonly IPlayerRepository playerRepository;
        public RegistrationHandler(IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            this.userRepository = userRepository;
            this.playerRepository = playerRepository;
        }
        public async void HandleRegistration(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;

            var userId = e.Message.FromId ?? -1;
            var user = await userRepository.GetUserAsync(userId);
            switch (msg)
            {
                case ConstPhrase.start:
                    if (user is not null) break;
                    HandleStart1(sender, e);
                    break;

                case ConstPhrase.createHero:
                    HandleGender2(sender, e);
                    break;

                case ConstPhrase.male or ConstPhrase.female:
                    HandleCreation3(sender, e);
                    break;

                default:
                    throw new ArgumentException(msg);
                    break;
            }
        }
        async void HandleStart1(object? sender, MessageReceivedEventArgs e)
        {

            var keyboard = new KeyboardBuilder();
            keyboard.AddButton(ConstPhrase.createHero, "", KeyboardButtonColor.Positive);

            await userRepository.AddUser(new User
            {
                Id = e.Message.FromId ?? -1,
                UserEventId = (int)EventType.HandleStart
            });

            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = "Добро пожаловать узбек \nДавайте создадим персоонажа",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = keyboard.Build()
            });

            Console.WriteLine($"HandleStart");
        }

        async void HandleGender2(object? sender, MessageReceivedEventArgs e)
        {
            var userId = e.Message.FromId ?? -1;
            var user = await userRepository.GetUserAsync(userId);

            if (user.UserEventId != (int)EventType.HandleStart)
                return;

            var keyboard = new KeyboardBuilder();
            keyboard.AddButton(ConstPhrase.male, "", KeyboardButtonColor.Positive)
                    .AddButton(ConstPhrase.female, "", KeyboardButtonColor.Negative);

            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = "Выберите пол",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = keyboard.Build()
            });

            await userRepository.SetCurrentEvent(userId, EventType.HandleGender);

            Console.WriteLine($"HandleGender");
        }

        async void HandleCreation3(object? sender, MessageReceivedEventArgs e)
        {
            var userId = e.Message.FromId ?? -1;
            var user = await userRepository.GetUserAsync(userId);

            if (user.UserEventId != (int)EventType.HandleGender)
                return;

            var playerId = await playerRepository.AddPlayer(new Player
            {
                Name = (await AppStart.bot.Api.Users.GetAsync(new List<long> { e.Message.FromId ?? (long)-1 })).FirstOrDefault()!.FirstName,
                Gender = e.Message.Text == ConstPhrase.female ? Gender.Female : Gender.Male,
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

            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = $"Персоонаж успешно создан!",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = keyboard.Build(),
            });

            await userRepository.SetCurrentEvent(userId, EventType.HandleCreation);

            Console.WriteLine($"HandleCreation");
        }

    }
}
