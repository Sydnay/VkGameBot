using FrogAnanas.Context;
using FrogAnanas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> IsExist(long id)
        {
            var user = await GetUser(id);

            if(user is null)
                return false;
            return true;
        }
        
        public async Task<List<User>> GetAllUsers()
        {
            var Users = context.Users.ToList();
            return Users;
        }
    }
}
