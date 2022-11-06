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

        public async Task<User> GetUser(long id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SetCurrentEvent(long userId, EventType currEvent)
        {
            var user = await GetUser(userId);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.UserEventId = (int)currEvent;
            await context.SaveChangesAsync();
        }
        public async Task SetPlayerId(long userId, long playerId)
        {
            var user = await GetUser(userId);

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PlayerId = playerId;
            await context.SaveChangesAsync();
        }
    }
}
