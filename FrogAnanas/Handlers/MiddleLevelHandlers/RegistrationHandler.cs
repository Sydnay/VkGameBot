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
        private readonly IPlayerRepository playerRepository;
        private readonly IMasteryRepository masteryRepository;
        public RegistrationHandler(IPlayerRepository playerRepository, IMasteryRepository masteryRepository)
        {
            this.playerRepository = playerRepository;
            this.masteryRepository = masteryRepository;
        }
        public void HandleRegistration(object? sender, MessageReceivedEventArgs e)
        {
            var msg = e.Message.Text;

            var userId = e.Message.FromId ?? -1;
            var player = playerRepository.GetPlayer(userId);

            switch (msg)
            {
                case ConstPhrase.start:
                    if (player is null)
                    {
                        player = new Player();
                        HandleStart1(sender, e);
                    }
                    break;

                case ConstPhrase.createHero:
                    if (player is not null && player.UserEventId == (int)EventType.HandleStart)
                    {
                        playerRepository.SetCurrentEvent(userId, EventType.HandleGender);
                        HandleGender2(sender, e);
                    }
                    break;

                case ConstPhrase.male or ConstPhrase.female:
                    if (player is not null && player.UserEventId == (int)EventType.HandleGender)
                    {
                        playerRepository.SetCurrentEvent(userId, EventType.HandleCreation);
                        HandleCreation3(userId,sender, e);
                    }
                    break;
                case ConstPhrase.masterySword or ConstPhrase.masteryDagger or ConstPhrase.masteryDoubleSword or ConstPhrase.masteryAxe:
                    if (player is not null && player.UserEventId == (int)EventType.HandleCreation)
                    {
                        playerRepository.SetCurrentEvent(userId, EventType.HandleFirstRole);
                        HandleFirstRole4(userId, sender, e);
                    }
                    break;
                case ConstPhrase.accept:
                    if (player is not null && player.UserEventId == (int)EventType.HandleFirstRole)
                    {
                        playerRepository.SetCurrentEvent(userId, EventType.HandleAcceptFirstRole);
                        HandleAcceptFristRole5(userId, sender, e);
                    }
                    break;
                case ConstPhrase.goBack:
                    if (player is not null && player.UserEventId == (int)EventType.HandleFirstRole)
                    {
                        playerRepository.SetCurrentEvent(userId, EventType.HandleCreation);
                        HandleGoBackFristRole5(sender, e);
                    }
                    break;

                default:
                    Console.WriteLine($"default {nameof(RegistrationHandler)}");
                    break;
            }
        }
        async void HandleStart1(object? sender, MessageReceivedEventArgs e)
        {
            playerRepository.AddPlayer(new Player
            {
                UserId = e.Message.FromId ?? -1,
                UserEventId = (int)EventType.HandleStart,
                Name = "Заблудший путник"
            });

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
            playerRepository.SetDefaultStats(userId, e.Message.Text == ConstPhrase.female ? Gender.Female : Gender.Male,
                (await AppStart.bot.Api.Users.GetAsync(new List<long> { e.Message.FromId ?? -1 })).FirstOrDefault()!.FirstName);

            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = $"Персоонаж успешно создан! \nДавайте выберем первую роль",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateMasteryKeyboard(ConstPhrase.masterySword, ConstPhrase.masteryDoubleSword, ConstPhrase.masteryAxe, ConstPhrase.masteryDagger )
            });

            Console.WriteLine($"HandleCreation");
        }

        async void HandleFirstRole4(long userId, object? sender, MessageReceivedEventArgs e)
        {
            var mastery = masteryRepository.GetMastery(e.Message.Text);
            playerRepository.SetMastery(userId, mastery.Id);

            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = $"{e.Message.Text}: {mastery.Description}",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, ConstPhrase.accept, ConstPhrase.goBack)
            });

            Console.WriteLine($"HandleCreation");
        }
        async void HandleAcceptFristRole5(long userId, object? sender, MessageReceivedEventArgs e)
        {
            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = $"Персоонаж успешно создан!",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, ConstPhrase.player)
            });

            Console.WriteLine($"HandleCreation");
        }
        async void HandleGoBackFristRole5(object? sender, MessageReceivedEventArgs e)
        {
            await AppStart.bot.Api.Messages.SendAsync(new MessagesSendParams
            {
                Message = $"(сменить мастерство можно будет чуть позже в любое время)",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateMasteryKeyboard(ConstPhrase.masterySword, ConstPhrase.masteryDoubleSword, ConstPhrase.masteryAxe, ConstPhrase.masteryDagger)
            });

            Console.WriteLine($"HandleCreation");
        }
    }
}
