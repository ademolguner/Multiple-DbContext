using MediatR;
using MultipleContext.Api.Order.Application.Models.Dto;

namespace MultipleContext.Api.Order.Application.Features.Order.Command.CreateOrder;

public class CreateOrderCommand:IRequest<OrderDto>
{
    public CreateOrderCommand(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}