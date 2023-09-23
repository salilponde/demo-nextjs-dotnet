using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WebApi.Helpers;
using WebApi.Repositories;
using WebApi.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var dbSettings = new DbSettings
        {
            Server = Environment.GetEnvironmentVariable("SqlServer"),
            UserId = Environment.GetEnvironmentVariable("SqlUserId"),
            Password = Environment.GetEnvironmentVariable("SqlPassword"),
            Database = Environment.GetEnvironmentVariable("SqlDatabase")
        };

        services.AddSingleton<DbSettings>(dbSettings);
        services.AddSingleton<DataContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
    })
    .Build();

host.Run();
