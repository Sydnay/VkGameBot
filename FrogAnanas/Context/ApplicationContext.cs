using FrogAnanas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace FrogAnanas.Context
{
    public class ApplicationContext : DbContext
    {
        private readonly StreamWriter _logStream = new StreamWriter("mylog.txt", append: true);
        public DbSet<Player> Players { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=vkgamedb;Username=vkgame_client;Password=vkgamepasswd")
                .EnableSensitiveDataLogging()
                .LogTo(_logStream.WriteLine,  
                    LogLevel.Information, 
                    DbContextLoggerOptions.DefaultWithUtcTime | DbContextLoggerOptions.SingleLine);
        }
        public override void Dispose()
        {
            base.Dispose();
            _logStream.Dispose();
        }
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _logStream.DisposeAsync();
        }

    }
}
