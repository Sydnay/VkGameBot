﻿using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUser (long id);
        Task AddUser (User user);
        Task SetCurrentEvent(long userId, EventType currEvent);
        Task SetPlayerId(long userId, long playerId);
    }
}
