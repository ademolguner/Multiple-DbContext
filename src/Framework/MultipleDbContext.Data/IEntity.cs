namespace MultipleDbContext.Data;

public interface IEntity
{
    
}

public interface IEntity<out TKey> : IEntity where TKey : IEquatable<TKey>
{
    TKey Id { get; }

    DateTime CreatedAt { get; set; }

    DateTime? UpdatedAt { get; set; }
    bool IsDeleted { get; set; }
}