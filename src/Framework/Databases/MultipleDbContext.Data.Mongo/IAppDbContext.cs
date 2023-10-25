using MongoDB.Driver;

namespace MultipleDbContext.Data.Mongo;

public interface IAppDbContext
{
    IMongoCollection<T> GetCollection<T>();
    IMongoClient Client { get; }
}