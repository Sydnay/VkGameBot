using FrogAnanas;
using FrogAnanas.Context;
using FrogAnanas.Handlers.JuniorLevelHandlers;
using FrogAnanas.Handlers.MiddleLevelHandlers;
using FrogAnanas.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var app = BuildConfig();
app.Start();

static AppStart BuildConfig()
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();

    var services = new ServiceCollection()
    .AddDbContextFactory<ApplicationContext>(lifetime: ServiceLifetime.Singleton);

    services.AddTransient<IPlayerRepository, PlayerRepository>();
    services.AddTransient<IMasteryRepository, MasteryRepository>();
    services.AddTransient<IStageRepository, StageRepository>();

    services.AddTransient<LowPlayerHandler>();
    services.AddTransient<LowAdventureHandler>();

    services.AddTransient<RegistrationHandler>();
    services.AddTransient<PlayerInfoHandler>();
    services.AddTransient<AdventureHandler>();

    services.AddTransient<AppStart>();

    var serviceProvider = services.BuildServiceProvider();

    var userRepo = serviceProvider.GetService<IPlayerRepository>();

    userRepo.ABOBA();

    //logger.LogInformation("Closing Application");
    AppStart app = serviceProvider.GetService<AppStart>();
    return app;
}