using FrogAnanas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogAnanas.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
            if(UserEvents.Local.FirstOrDefault() is null)
            {
                UserEvents.Add(new UserEvent
                {
                    Id = (int)EventType.HandleStart,
                    Name = EventType.HandleStart.ToString()
                });
                UserEvents.Add(new UserEvent
                {
                    Id = (int)EventType.HandleGender,
                    Name = EventType.HandleGender.ToString()
                });
                UserEvents.Add(new UserEvent
                {
                    Id = (int)EventType.HandleCreation,
                    Name = EventType.HandleCreation.ToString()
                });
                SaveChangesAsync();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-ET4OTK6\SQLEXPRESS;Database=VkGamedb;Trusted_Connection=True;");
        }
    }

}
