namespace MultipleContext.Api.Catalog.Application.Models.Dto;

public class CatalogDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
}