using FrogAnanas.Constants;
using FrogAnanas.Helpers;
using FrogAnanas.Models;
using FrogAnanas.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkBotFramework.Models;
using VkNet.Model.Attachments;

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
                case RegistrationPhrase.start:
                    if (player is null)
                    {
                        player = new Player();
                        HandleStart1(sender, e);
                    }
                    break;

                case RegistrationPhrase.createHero:
                    if (player is not null && player.UserEventId == (int)EventType.HandleStart)
                    {
                        playerRepository.SetEvent(userId, EventType.HandleGender);
                        HandleGender2(sender, e);
                    }
                    break;

                case RegistrationPhrase.male or RegistrationPhrase.female:
                    if (player is not null && player.UserEventId == (int)EventType.HandleGender)
                    {
                        playerRepository.SetEvent(userId, EventType.HandleCreation);
                        HandleCreation3(userId,sender, e);
                    }
                    break;
                case RegistrationPhrase.masterySword or RegistrationPhrase.masteryDagger or RegistrationPhrase.masteryDoubleSword or RegistrationPhrase.masteryAxe:
                    if (player is not null && player.UserEventId == (int)EventType.HandleCreation)
                    {
                        playerRepository.SetEvent(userId, EventType.HandleFirstRole);
                        HandleFirstRole4(userId, sender, e);
                    }
                    break;
                case RegistrationPhrase.accept:
                    if (player is not null && player.UserEventId == (int)EventType.HandleFirstRole)
                    {
                        playerRepository.SetEvent(userId, EventType.HandleAcceptFirstRole);
                        HandleAcceptFristRole5(userId, sender, e);
                    }
                    break;
                case RegistrationPhrase.goBack:
                    if (player is not null && player.UserEventId == (int)EventType.HandleFirstRole)
                    {
                        playerRepository.SetEvent(userId, EventType.HandleCreation);
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
                Name = "Заблудший путник",
                MaxStage = 1
            });
            Log.Information($"Игрок {e.Message.FromId} зарегался");

            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = "Добро пожаловать узбек \nДавайте создадим персоонажа",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Positive, RegistrationPhrase.createHero)
            });
            Log.Information($"Отправили сообщение");
        }

        async void HandleGender2(object? sender, MessageReceivedEventArgs e)
        {
            Log.Information($"Игрок {e.Message.FromId} начал создание персонажа");

            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = "Выберите пол",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Positive, RegistrationPhrase.male, RegistrationPhrase.female)
            });

            Log.Information($"Отправили сообщение");
        }

        async void HandleCreation3(long userId, object? sender, MessageReceivedEventArgs e)
        {
            playerRepository.SetDefaultStats(userId, e.Message.Text == RegistrationPhrase.female ? Gender.Female : Gender.Male,
                (await AppStart.bot.Api.Users.GetAsync(new List<long> { e.Message.FromId ?? -1 })).FirstOrDefault()!.FirstName);

            Log.Information($"Игрок {e.Message.FromId} выбрал пол");
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = $"Персоонаж успешно создан! \nДавайте выберем первую роль",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateOneColumns(KeyboardButtonColor.Positive, RegistrationPhrase.masterySword, RegistrationPhrase.masteryDoubleSword, RegistrationPhrase.masteryAxe, RegistrationPhrase.masteryDagger )
            });

            Log.Information($"Отправили сообщение ");
        }

        async void HandleFirstRole4(long userId, object? sender, MessageReceivedEventArgs e)
        {
            var mastery = masteryRepository.GetMastery(e.Message.Text);
            playerRepository.SetMastery(userId, mastery.Id);

            Log.Information($"Игрок {e.Message.FromId} выбрал роль");
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = $"{e.Message.Text}: {mastery.Description}",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Default, RegistrationPhrase.accept, RegistrationPhrase.goBack)
            });

            Log.Information($"Отправили сообщение ");
        }
        async void HandleAcceptFristRole5(long userId, object? sender, MessageReceivedEventArgs e)
        {
            Log.Information($"Игрок {e.Message.FromId} подтвердил выбранную роль"); 
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = $"Персоонаж успешно создан!",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateBuilder(KeyboardButtonColor.Positive, AdventurePhrase.START_ADVENTURE)
            });

            Log.Information($"Отправили сообщение ");
        }
        async void HandleGoBackFristRole5(object? sender, MessageReceivedEventArgs e)
        {
            Log.Information($"Игрок {e.Message.FromId} отменил выбранную роль");
            AppStart.bot.Api.Messages.Send(new MessagesSendParams
            {
                Message = $"(сменить мастерство можно будет чуть позже в любое время)",
                PeerId = e.Message.PeerId,
                RandomId = Math.Abs(Environment.TickCount),
                Keyboard = KeyboardHelper.CreateOneColumns(KeyboardButtonColor.Positive, RegistrationPhrase.masterySword, RegistrationPhrase.masteryDoubleSword, RegistrationPhrase.masteryAxe, RegistrationPhrase.masteryDagger)
            });

            Log.Information($"Отправили сообщение ");
        }
    }
}
