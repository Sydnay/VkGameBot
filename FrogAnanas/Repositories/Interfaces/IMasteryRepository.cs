using FrogAnanas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Repositories
{
    public interface IMasteryRepository
    {
        Mastery GetMasteryByName(string name);
        Mastery GetMastery(int id);

    }
}
