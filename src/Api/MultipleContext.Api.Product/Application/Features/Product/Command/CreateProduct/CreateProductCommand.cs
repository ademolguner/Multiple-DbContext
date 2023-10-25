using MediatR;
using MultipleContext.Api.Product.Application.Models.Dto;

namespace MultipleContext.Api.Product.Application.Features.Product.Command.CreateProduct;

public class CreateProductCommand:IRequest<ProductDto>
{
    public CreateProductCommand(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}