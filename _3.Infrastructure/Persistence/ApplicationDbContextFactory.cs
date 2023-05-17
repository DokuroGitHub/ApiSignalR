using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    ///<summary> to generate migrations </summary>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // get connection string
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var appsettings = configuration.Get<Appsettings>() ?? new Appsettings();

        var connectionString = appsettings.ConnectionStrings.DefaultConnectionV2;
        Console.WriteLine(connectionString);

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}