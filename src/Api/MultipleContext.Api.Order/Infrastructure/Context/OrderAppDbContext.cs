using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MultipleDbContext.Data.Mongo;

namespace MultipleContext.Api.Order.Infrastructure.Context;

public class OrderAppDbContext: IAppDbContext
{
    private readonly IMongoDatabase _db;
    private readonly IMongoClient _client;

    public OrderAppDbContext(IOptionsMonitor<MongoDbSettings> mongoDbSettings)
    {
        var settings = mongoDbSettings.CurrentValue;

        _client = new MongoClient(settings.GetConnectionString(settings.DatabaseName));
        _db = _client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>() => _db.GetCollection<T>(_collections[typeof(T)]);
    private readonly Dictionary<Type, string> _collections = new()
    {
        {typeof(Domain.Entities.Order), "Orders"}
    };
    
    public IMongoClient Client => _client;
}