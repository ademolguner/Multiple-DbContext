using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultipleContext.Api.Order.Application.Features.Order.Command.CreateOrder;
using MultipleContext.Api.Order.Application.Features.Order.Query.GetByIdOrder;

namespace MultipleContext.Api.Order.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
     
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var item = await _mediator.Send(new GetByIdOrderQuery(id));
        return Ok(item);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderCommand command)
    {
        var created = await _mediator.Send(command);
        return Ok(created);
    }
}