using FrogAnanas;
using FrogAnanas.Context;
using FrogAnanas.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

BuildConfig();


static void BuildConfig()
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
    .AddDbContext<ApplicationContext>(ServiceLifetime.Transient);

    // register `Worker` in the service collection
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IPlayerRepository, PlayerRepository>();

    // build the service provider
    var serviceProvider = services.BuildServiceProvider();

    // resolve a `Worker` from the service provider
    var userRepo = serviceProvider.GetService<IUserRepository>();
    var playerRepo = serviceProvider.GetService<IPlayerRepository>();

    var logger = serviceProvider.GetService<ILogger<Program>>();

    //logger.LogInformation("Closing Application");
    AppStart app = new AppStart(userRepo, playerRepo);
    app.Start();
}
