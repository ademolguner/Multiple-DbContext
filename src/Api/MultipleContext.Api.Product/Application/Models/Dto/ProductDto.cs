namespace MultipleContext.Api.Product.Application.Models.Dto;

public class ProductDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
}