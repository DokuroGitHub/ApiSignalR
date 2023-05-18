using Application.Common.Interfaces;
using Application.Common.Interfaces.IRepositories;
using Domain.Common;
using Infrastructure;
using Infrastructure.Files;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        Appsettings appsettings)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (appsettings.UseInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CleanArchitectureDb"));
        }
        else
        {
            var connectionString = appsettings.ConnectionStrings.DefaultConnection;
            Console.WriteLine($"connectionString: {connectionString}");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        // repositories
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<ITodoListRepository, TodoListRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // services
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        return services;
    }
}
