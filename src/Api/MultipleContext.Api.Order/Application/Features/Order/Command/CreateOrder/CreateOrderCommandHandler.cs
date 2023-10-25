using MediatR;
using MultipleContext.Api.Order.Application.Models.Dto;
using MultipleContext.Api.Order.Infrastructure.Repositories.Interfaces.Executable;

namespace MultipleContext.Api.Order.Application.Features.Order.Command.CreateOrder;

public class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommand,OrderDto>
{
    private readonly IExecutableOrderRepository _executableOrderRepository;
    public CreateOrderCommandHandler(IExecutableOrderRepository executableOrderRepository)
    {
        _executableOrderRepository = executableOrderRepository ?? throw new ArgumentNullException(nameof(executableOrderRepository));
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var model = new Domain.Entities.Order
        {
            CreatedAt = DateTime.Now,
            Name = request.Name
        };
        
        var isCreated=await _executableOrderRepository.AddAsync(model);
        if (!isCreated)
            throw new Exception("Yeni bir Order ekleme işlemi sırasında hata oluştu");

        return new OrderDto
        {
            CreatedAt = model.CreatedAt,
            IsDeleted = false,
            Id = model.Id,
            Name = model.Name
        };
    }
}