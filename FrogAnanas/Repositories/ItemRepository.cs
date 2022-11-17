using FrogAnanas.Context;
using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationContext context;
        public ItemRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public Item GetItem(int id)
        {
            return context.Items.FirstOrDefault(x => x.Id == id);
        }
    }
}
