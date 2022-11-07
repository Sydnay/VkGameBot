using FrogAnanas.Context;
using FrogAnanas.Models;
using Microsoft.EntityFrameworkCore;

namespace FrogAnanas.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;
        object locker = new object();
        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task AddUser(User user)
        {
            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserAsync(long userId)
        {
            lock (locker)
            {
                return context.Users.FirstOrDefault(x => x.Id == userId);
            }
        }
        public User GetUserWithPlayerAsync(long userId)
        {
            lock(locker)
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                var player = context.Players.FirstOrDefault(x => x.Id == (user != null ? user.PlayerId : -1));
                return user;
            }
        }

        public void SetCurrentEvent(long userId, EventType currEvent)
        {
            var user = GetUserAsync(userId);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UserEventId = (int)currEvent;
            context.SaveChanges();
        }
        public async Task SetPlayerId(long userId, long playerId)
        {
            var user = GetUserAsync(userId);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PlayerId = playerId;
            context.SaveChanges();
        }

        public async Task ABOBA()
        {
            if (context.UserEvents.FirstOrDefault() is not null)
                return;

            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandleStart,
                Name = EventType.HandleStart.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandleGender,
                Name = EventType.HandleGender.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandleCreation,
                Name = EventType.HandleCreation.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandlePlayer,
                Name = EventType.HandlePlayer.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandlePlayerInfo,
                Name = EventType.HandlePlayerInfo.ToString()
            });
            await context.UserEvents.AddAsync(new UserEvent
            {
                Id = (int)EventType.HandlePlayerInventory,
                Name = EventType.HandlePlayerInventory.ToString()
            });
            await context.SaveChangesAsync();
        }
    }
}
