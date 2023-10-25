namespace MultipleDbContext.Data.Mongo;

// public class MongoDbOptions
// {
//     public string ConnectionString { get; set; }
//     public string Database { get; set; }
// }

// veya


public class MongoDbSettings
{
    private const int DefaultMaxPoolSize = 100;

    public required  string DatabaseName { get; set; }
    public required  string UserName { get; set; }
    public required  string Password { get; set; }
    public required  int MaxPoolSize { get; set; }
    public required  MongoDbSettingsReplicaModel ReplicaSet { get; set; }

    public string GetConnectionString(string dbName)
    {
        if (MaxPoolSize == 0)
            MaxPoolSize = DefaultMaxPoolSize;
            
        return $"mongodb://{UserName}:{Password}@{string.Join(',', ReplicaSet.Endpoints.Select(s => $"{s.Name}:{s.Port}").ToList())}/?authSource={dbName}&maxPoolSize={MaxPoolSize}";
    }
}   

public class MongoDbSettingsReplicaModel
{
    public required string Name { get; set; }
    public   List<MongoDbSettingsReplicaEndpointModel> Endpoints { get; set; }
}

public class MongoDbSettingsReplicaEndpointModel
{
    public required  string Name { get; set; }
    public required  int Port { get; set; }
}
