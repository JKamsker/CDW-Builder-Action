using CDW_Builder_Action.Dal;
using CDW_Builder_Action.Models.Configuration;
using CDW_Builder_Action.Models.Database;
using CDW_Builder_Action.Models.Mapping;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using System;
using System.Security.Authentication;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<EventDao>();

        services.AddHostedService<CommandRunner>();
        services.Configure<GitConfiguration>(Configuration.GetSection("Git"));
        services.AddAutoMapper(typeof(WorkshopEventProfile));

        ConfigureMongoDb(services);
    }

    private void ConfigureMongoDb(IServiceCollection services)
    {
        var databaseName = Configuration["Database:Name"]?.ToString();
        //Console.WriteLine($"Database Name: {databaseName}");

        var mongoClient = CreateMongoClient();
        var db = mongoClient.GetDatabase(databaseName);
        var events = db.GetCollection<WorkshopEvent>("events");

       

        var coll = events.Find(x => true).ToList();


        services
            .AddSingleton(db)
            .AddSingleton(events);
    }

    private MongoClient CreateMongoClient()
    {
        var connectionString = Configuration["Database:ConnectionString"]?.ToString();
        Console.WriteLine($"connectionString: {connectionString}");

        var settings = MongoClientSettings.FromUrl(
            new MongoUrl(connectionString)
        );

        settings.SslSettings = new() { EnabledSslProtocols = SslProtocols.Tls12 };

        return new MongoClient(settings);
    }
}