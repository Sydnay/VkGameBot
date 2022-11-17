using FrogAnanas.Context;
using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationContext context;
        public ResourceRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public Resource GetResource(int id)
        {
            return context.Resources.FirstOrDefault(x => x.Id == id);
        }
    }
}
