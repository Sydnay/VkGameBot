using FrogAnanas.Context;
using FrogAnanas.Models;
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
                await context.Players.AddAsync(player);
                await context.SaveChangesAsync();

                var id = context.Set<Player>().FirstOrDefault()!.Id;
                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<Player> GetPlayer(long id)
        {
            throw new NotImplementedException();
        }
    }
}
