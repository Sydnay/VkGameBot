using FrogAnanas.Constants;
using FrogAnanas.Helpers;
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
        private readonly UserCachedRepository userCachedRepository;
        public RegistrationHandler(IUserRepository userRepository, IPlayerRepository playerRepository, UserCachedRepository userCachedRepository)
        {
            this.userRepository = userRepository;
            this.playerRepository = playerRepository;
            this.userCachedRepository = userCachedRepository;
        }
        public async void HandleRegistration(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;

            var userId = e.Message.FromId ?? -1;
            var user = userCachedRepository.GetUser(userId);
            switch (msg)
            {
                case ConstPhrase.start:
                    if (user is not null) break;
                    HandleStart1(sender, e);
                    break;

                case ConstPhrase.createHero:
                    if (user.UserEventId == (int)EventType.HandleStart)
                    {
                        userRepository.SetCurrentEvent(userId, EventType.HandleGender);
                        HandleGender2(sender, e);
                    }
                    break;

                case ConstPhrase.male or ConstPhrase.female:
                    if (user.PlayerId is null && user.UserEventId == (int)EventType.HandleGender)
                    {
                        userRepository.SetCurrentEvent(userId, EventType.HandleCreation);
                        HandleCreation3(userId,sender, e);
                    }
                    break;

                default:
                    Console.WriteLine($"default {nameof(RegistrationHandler)}");
                    break;
            }
        }
        async void HandleStart1(object? sender, MessageReceivedEventArgs e)
        {
            await userRepository.AddUser(new User
            {
                Id = e.Message.FromId ?? -1,
                UserEventId = (int)EventType.HandleStart
            });

            userCachedRepository.UpdateCache();

            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = "Добро пожаловать узбек \nДавайте создадим персоонажа",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Positive, ConstPhrase.createHero)
            });

            Console.WriteLine($"HandleStart");
        }

        async void HandleGender2(object? sender, MessageReceivedEventArgs e)
        {
            var userId = e.Message.FromId ?? -1;
            var user = userRepository.GetUserAsync(userId);

            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = "Выберите пол",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Positive, ConstPhrase.male, ConstPhrase.female)
            });


            Console.WriteLine($"HandleGender");
        }

        async void HandleCreation3(long userId, object? sender, MessageReceivedEventArgs e)
        {
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
            userCachedRepository.UpdateCache();

            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = $"Персоонаж успешно создан!",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, ConstPhrase.player)
            });


            Console.WriteLine($"HandleCreation");
        }

    }
}
