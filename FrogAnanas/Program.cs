using FrogAnanas;
using FrogAnanas.Context;
using FrogAnanas.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var app = BuildConfig();
app.Start();

static AppStart BuildConfig()
{
    //var serviceProvider = new ServiceCollection()
    //        .AddLogging()
    //        .AddSingleton<IFooService, FooService>()
    //        .AddSingleton<IBarService, BarService>()
    //        .BuildServiceProvider();

    ////configure console logging
    //serviceProvider
    //    .GetService<ILoggerFactory>()
    //    .AddConsole(LogLevel.Debug);

    //var logger = serviceProvider.GetService<ILoggerFactory>()
    //    .CreateLogger<Program>();
    //logger.LogDebug("Starting application");

    ////do the actual work here
    //var bar = serviceProvider.GetService<IBarService>();
    //bar.DoSomeRealWork();

    //logger.LogDebug("All done!");

    var services = new ServiceCollection()
    .AddDbContextFactory<ApplicationContext>(lifetime:ServiceLifetime.Transient);

    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IPlayerRepository, PlayerRepository>();
    services.AddTransient<AppStart>();

    var serviceProvider = services.BuildServiceProvider();

    var userRepo = serviceProvider.GetService<IUserRepository>();
    var playerRepo = serviceProvider.GetService<IPlayerRepository>();

    var logger = serviceProvider.GetService<ILogger<Program>>();

    //logger.LogInformation("Closing Application");
    AppStart app = serviceProvider.GetService<AppStart>();
    return app;
}
