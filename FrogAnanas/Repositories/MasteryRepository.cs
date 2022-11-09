using FrogAnanas.Context;
using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public class MasteryRepository : IMasteryRepository
    {
        private readonly ApplicationContext context;
        private readonly
        object locker = new object();
        public MasteryRepository(ApplicationContext context)
        {
            this.context = context;
        }
        public Mastery GetMastery(string name)
        {
            return context.Masteries.FirstOrDefault(x => x.Name == name);
        }
    }
}
