using FrogAnanas.Context;
using FrogAnanas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext context;
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

        public async Task<User> GetUserAsync(long userId)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }
        public async Task<User> GetUserWithPlayerAsync(long userId)
        {
            return await context.Users.Include(x=>x.Player).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task SetCurrentEvent(long userId, EventType currEvent)
        {
            var user = await GetUserAsync(userId);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UserEventId = (int)currEvent;
            await context.SaveChangesAsync();
        }
        public async Task SetPlayerId(long userId, long playerId)
        {
            var user = await GetUserAsync(userId);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PlayerId = playerId;
            await context.SaveChangesAsync();
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
