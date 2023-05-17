using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure.Common;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // from _4.Api to _1.Domain.Seeds
        var seedsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "../_1.Domain/Seeds/");
        if (Directory.Exists(seedsFolderPath))
        {
            modelBuilder.GenericSeed<User>(Path.Combine(seedsFolderPath, "User.json"));
        }
    }

    public static void GenericSeed<T>(this ModelBuilder modelBuilder, string path) where T : class
    {
        // check if path is valid
        if (File.Exists(path) is false)
        {
            Console.WriteLine($"File '{path}' not found.");
            return;
        }
        using var r = new StreamReader(path);
        string json = r.ReadToEnd();
        var items = JsonConvert.DeserializeObject<List<T>>(json);
        if (items is not null)
        {
            modelBuilder.Entity<T>().HasData(items);
        }
    }
}
