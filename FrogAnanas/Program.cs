using FrogAnanas;
using FrogAnanas.Context;
using FrogAnanas.Handlers.JuniorLevelHandlers;
using FrogAnanas.Handlers.MiddleLevelHandlers;
using FrogAnanas.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var app = BuildConfig();
app.Start();

static AppStart BuildConfig()
{
    var services = new ServiceCollection()
    .AddDbContextFactory<ApplicationContext>(lifetime:ServiceLifetime.Singleton);

    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IPlayerRepository, PlayerRepository>();
    services.AddSingleton<UserCachedRepository>();

    services.AddTransient<LowPlayerHandler>();

    services.AddTransient<RegistrationHandler>();
    services.AddTransient<PlayerInfoHandler>();

    services.AddTransient<AppStart>();

    var serviceProvider = services.BuildServiceProvider();

    var userRepo = serviceProvider.GetService<IUserRepository>();
    var userCacheRepo = serviceProvider.GetService<UserCachedRepository>();

    userRepo.ABOBA();
    Thread.Sleep(500);
    userCacheRepo.UpdateCache();

    //logger.LogInformation("Closing Application");
    AppStart app = serviceProvider.GetService<AppStart>();
    return app;
}
