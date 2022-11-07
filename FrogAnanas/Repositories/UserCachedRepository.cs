using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public class UserCachedRepository
    {
        private readonly IUserRepository userRepository;
        List<User> users = new List<User>();
        public UserCachedRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User GetUser(long userId) => users.FirstOrDefault(x => x.Id == userId);
        public List<User> GetUsers() => users;
        public void UpdateCache()
        {
            users.Clear();
            users.AddRange(userRepository.GetAllUsers());
        }
    }
}
