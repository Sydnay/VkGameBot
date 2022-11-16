using FrogAnanas;
using FrogAnanas.Context;
using FrogAnanas.Handlers.JuniorLevelHandlers;
using FrogAnanas.Handlers.MiddleLevelHandlers;
using FrogAnanas.Repositories;
using FrogAnanas.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var app = Build(config);
app.Start(config);

static AppStart Build(IConfiguration config)
{

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(config)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

    var services = new ServiceCollection()
    .AddDbContextFactory<ApplicationContext>(options =>
    {
        options.UseSqlServer(config.GetConnectionString("DefaultContext"));
        options.EnableSensitiveDataLogging();
    }
    ,lifetime: ServiceLifetime.Singleton);

    services.AddTransient<IPlayerRepository, PlayerRepository>();
    services.AddTransient<IMasteryRepository, MasteryRepository>();
    services.AddTransient<IStageRepository, StageRepository>();
    services.AddTransient<IEnemyRepository, EnemyRepository>();
    services.AddTransient<IBattleService, BattleService>();
    services.AddSingleton<MongoDbRepository>();

    services.AddTransient<LowPlayerHandler>();
    services.AddTransient<LowAdventureHandler>();
    services.AddTransient<LowTowerHandler>();
    services.AddTransient<LowFightingHandler>();

    services.AddTransient<RegistrationHandler>();
    services.AddTransient<PlayerInfoHandler>();
    services.AddTransient<AdventureHandler>();
    services.AddTransient<TowerHandler>();
    services.AddTransient<FightingHandler>();

    services.AddTransient<AppStart>();

    var serviceProvider = services.BuildServiceProvider();

    var userRepo = serviceProvider.GetService<IPlayerRepository>();

    userRepo.ABOBA();

    //logger.LogInformation("Closing Application");
    AppStart app = serviceProvider.GetService<AppStart>();
    return app;
}