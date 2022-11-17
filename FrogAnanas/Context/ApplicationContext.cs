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
        public DbSet<ItemsPlayer> ItemsPlayers { get; set; }
        public DbSet<ResourcesPlayer> ResourcesPlayers { get; set; }
        public DbSet<ItemsEnemy> ItemsEnemies { get; set; }
        public DbSet<ResourcesEnemy> ResourcesEnemies { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Enemy> Enemies { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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


            modelBuilder.Entity<ResourcesPlayer>()
            .HasOne(p => p.Player)
            .WithMany(mp => mp.ResourcesPlayers)
            .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<ItemsPlayer>()
            .HasOne(p => p.Player)
            .WithMany(mp => mp.ItemsPlayers)
            .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<ResourcesEnemy>()
            .HasKey(sc => new { sc.EnemyId, sc.ResourceId });

            modelBuilder.Entity<ResourcesEnemy>()
            .HasOne(p => p.Enemy)
            .WithMany(mp => mp.ResourcesEnemies)
            .HasForeignKey(p => p.EnemyId);

            modelBuilder.Entity<ItemsEnemy>()
            .HasKey(sc => new { sc.EnemyId, sc.ItemId });

            modelBuilder.Entity<ItemsEnemy>()
            .HasOne(p => p.Enemy)
            .WithMany(mp => mp.ItemsEnemies)
            .HasForeignKey(p => p.EnemyId);

            modelBuilder.Entity<Player>()
            .HasOne(p => p.Stage)
            .WithMany(mp => mp.Players)
            .HasForeignKey(p => p.MaxStage);
        }
    }

}
