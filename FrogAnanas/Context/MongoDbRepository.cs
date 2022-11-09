using FrogAnanas.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Context
{
    public class MongoDbRepository
    {
        private readonly IMongoCollection<Player> playerAttribute;
        public MongoDbRepository(IMongoClient client)
        {
            IMongoDatabase db = client.GetDatabase("VkGamedb");
            playerAttribute = db.GetCollection<Player>("playerAttribute");
        }
        public Player GetListAttribute(long userId)
        {
            return null;
        }
    }
}
