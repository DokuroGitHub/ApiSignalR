using System.Reflection;
using Domain.Common;
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
        // print seeds folder path
        Console.WriteLine($"seedsFolderPath: {seedsFolderPath}");
        if (Directory.Exists(seedsFolderPath))
        {
            try
            {
                modelBuilder.SeedFromJsonFiles(seedsFolderPath);
                // modelBuilder.GenericSeed<Conversation>(Path.Combine(seedsFolderPath, $"{nameof(Conversation)}.json"));
                // modelBuilder.GenericSeed<ConversationBlock>(Path.Combine(seedsFolderPath, $"{nameof(ConversationBlock)}.json"));
                // modelBuilder.GenericSeed<ConversationInvitation>(Path.Combine(seedsFolderPath, $"{nameof(ConversationInvitation)}.json"));
                // modelBuilder.GenericSeed<DeletedMessage>(Path.Combine(seedsFolderPath, $"{nameof(DeletedMessage)}.json"));
                // modelBuilder.GenericSeed<Message>(Path.Combine(seedsFolderPath, $"{nameof(Message)}.json"));
                // modelBuilder.GenericSeed<MessageAttachment>(Path.Combine(seedsFolderPath, $"{nameof(MessageAttachment)}.json"));
                // modelBuilder.GenericSeed<MessageEmote>(Path.Combine(seedsFolderPath, $"{nameof(MessageEmote)}.json"));
                // modelBuilder.GenericSeed<Participant>(Path.Combine(seedsFolderPath, $"{nameof(Participant)}.json"));
                // modelBuilder.GenericSeed<User>(Path.Combine(seedsFolderPath, $"{nameof(User)}.json"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static void SeedFromJsonFiles(this ModelBuilder modelBuilder, string seedsFolderPath)
    {
        // get all file by folder path
        var files = Directory.GetFiles(seedsFolderPath, "*.json", SearchOption.TopDirectoryOnly);
        // get all file name
        // var fileNames = files.Select(Path.GetFileNameWithoutExtension).ToList();
        // print file names
        var assemblyQualifiedName = typeof(BaseEntity).Assembly;
        // print assembly qualified name
        Console.WriteLine($"assemblyQualifiedName: {assemblyQualifiedName}");
        // loop all files to add data
        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var filePath = Path.Combine(seedsFolderPath, $"{fileName}.json");
            // print file name
            Console.WriteLine($"fileName: {fileName}");
            // print file path
            Console.WriteLine($"filePath: {filePath}");
            if (fileName == null) continue;
            // get type by file name
            var type = Type.GetType($"Domain.Entities.{fileName}, {assemblyQualifiedName}");
            Console.WriteLine($"type: {type}");
            if (type is not null)
            {
                // get method by type
                var method = typeof(ModelBuilderExtensions).GetMethod(nameof(ModelBuilderExtensions.GenericSeedV2));
                // make method generic by type
                var generic = method?.MakeGenericMethod(type);
                // invoke method
                // generic?.Invoke(modelBuilder, new object[] { filePath });
                generic?.Invoke(null, new object[] { modelBuilder, filePath });
            }
        }
    }

    public static void GenericSeedV2<T>(ModelBuilder modelBuilder, string path) where T : class
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
