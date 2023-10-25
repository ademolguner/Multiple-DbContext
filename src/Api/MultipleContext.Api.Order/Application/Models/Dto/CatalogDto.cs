namespace MultipleContext.Api.Order.Application.Models.Dto;

public class OrderDto
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
}