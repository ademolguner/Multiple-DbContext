using MediatR;
using MultipleContext.Api.Order.Application.Models.Dto;

namespace MultipleContext.Api.Order.Application.Features.Order.Query.GetByIdOrder;

public class GetByIdOrderQuery:IRequest<OrderDto>
{
    public GetByIdOrderQuery(string id)
    {
        Id = id;
    }
    public  string Id { get; set; }
}