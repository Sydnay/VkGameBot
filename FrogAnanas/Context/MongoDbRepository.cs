using FrogAnanas.Events;
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
        public List<FightEnemy> FightEvent = new List<FightEnemy>();

        //public MongoDbRepository(IMongoClient client)
        //{
        //    IMongoDatabase db = client.GetDatabase("VkGamedb");
        //    playerAttribute = db.GetCollection<Player>("playerAttribute");
        //}
        public Player GetListAttribute(long userId)
        {
            return null;
        }
        public void AddEvent(Player player, Enemy enemy)
        {
            FightEvent.Add(new FightEnemy { Player = player, Enemy = enemy });
        }
        public void CloseEvent(long userId)
        {
            FightEvent.Remove(FightEvent.FirstOrDefault(x => x.Player.UserId == userId)!);
        }
        public Player GetPlayer(long userId)
        {
            return FightEvent.FirstOrDefault(x=> x.Player.UserId == userId)?.Player!;
        }
        public Enemy GetEnemy(long userId)
        {
            return FightEvent.FirstOrDefault(x => x.Player.UserId == userId)?.Enemy!;
        }
    }
}
