using MediatR;
using MultipleContext.Api.Order.Application.Models.Dto;
using MultipleContext.Api.Order.Infrastructure.Repositories.Interfaces.Queryable;

namespace MultipleContext.Api.Order.Application.Features.Order.Query.GetByIdOrder;

public class GetByIdOrderQueryHandler:IRequestHandler<GetByIdOrderQuery,OrderDto>
{
     
    private readonly IQueryableOrderRepository _queryableOrderRepository;
    public GetByIdOrderQueryHandler(IQueryableOrderRepository queryableOrderRepository)
    {
        _queryableOrderRepository = queryableOrderRepository ?? throw new ArgumentNullException(nameof(queryableOrderRepository));
    }

    public async Task<OrderDto> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
    {
        var model=await _queryableOrderRepository.GetByIdAsync(id:request.Id,master:false);
        if (model is null)
            ArgumentException.ThrowIfNullOrEmpty(nameof(Domain.Entities.Order.Id));

        return new OrderDto
        {
            Id = model.Id,
            Name = model.Name,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            IsDeleted = false
        };
    }
}