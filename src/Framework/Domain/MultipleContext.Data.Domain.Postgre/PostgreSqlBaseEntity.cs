using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultipleContext.Data.Domain.Postgre;

public class PostgreSqlBaseEntity:IPostgreSqlEntity<Guid>
{
    [Key]
    [Column("id")]
    public Guid Id { get; }
    
    [Column("createdat")]
    public DateTime CreatedAt { get; set; }
    
    [Column("updatedat")]
    public DateTime? UpdatedAt { get; set; }
    
    [Column("isdeleted")]
    public bool IsDeleted { get; set; }
}