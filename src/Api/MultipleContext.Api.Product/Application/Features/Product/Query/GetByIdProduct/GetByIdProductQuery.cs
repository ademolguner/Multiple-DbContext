using MediatR;
using MultipleContext.Api.Product.Application.Models.Dto;

namespace MultipleContext.Api.Product.Application.Features.Product.Query.GetByIdProduct;

public class GetByIdProductQuery:IRequest<ProductDto>
{
    public GetByIdProductQuery(string id)
    {
        Id = id;
    }
    public  string Id { get; set; }
}