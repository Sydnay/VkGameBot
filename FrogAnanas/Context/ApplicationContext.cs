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
        public DbSet<Player> Players { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<Mastery> Masteries { get; set; }
        public DbSet<MasteryPlayer> MasteryPlayers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Level> Levels { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-ET4OTK6\SQLEXPRESS;Database=VkGamedb;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasteryPlayer>()
            .HasKey(sc => new { sc.UserId, sc.MasteryId });

            modelBuilder.Entity<MasteryPlayer>()
            .HasOne(p => p.Player)
            .WithMany(mp => mp.MasteryPlayers)
            .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<MasteryPlayer>()
            .HasOne(p => p.CurrentLevelCLASS)
            .WithMany(l => l.MasteryPlayers)
            .HasForeignKey(p => p.CurrentLvl); 
            
            modelBuilder.Entity<MasteryPlayer>()
             .HasOne(x=>x.Player).WithMany(x=>x.MasteryPlayers).OnDelete(DeleteBehavior.NoAction);
        }
    }

}
