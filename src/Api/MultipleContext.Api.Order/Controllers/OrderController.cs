using Microsoft.AspNetCore.Mvc;

namespace MultipleContext.Api.Order.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
     
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "orders")]
    public IEnumerable<string> Get() => new List<string>{"Come to back"};
}