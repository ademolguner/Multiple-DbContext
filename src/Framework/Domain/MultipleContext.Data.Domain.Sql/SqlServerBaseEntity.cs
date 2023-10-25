namespace MultipleContext.Data.Domain.Sql;

public class SqlServerBaseEntity:ISqlServerEntity<int>
{
    public int Id { get; }// type dikkat attiribute olacvak primary key
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}