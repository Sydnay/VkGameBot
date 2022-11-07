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
    internal class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationContext context;
        public PlayerRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<long> AddPlayer(Player player)
        {
            try
            {
                var playerEntiry = await context.Players.AddAsync(player);
                await context.SaveChangesAsync();

                return playerEntiry.Entity.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public  Player GetPlayer(long? playerId)
        {
            return context.Players.FirstOrDefault(x => x.Id == playerId);
        }
    }
}
