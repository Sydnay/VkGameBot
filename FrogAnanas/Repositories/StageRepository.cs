using FrogAnanas.Context;
using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public class StageRepository : IStageRepository
    {
        private readonly ApplicationContext context;
        public StageRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Stage GetStage(int id)
        {
            return context.Stages.FirstOrDefault(x => x.Id == id);
        }
    }
}
