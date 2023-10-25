using MediatR;
using MultipleContext.Api.Product.Application.Models.Dto;
using MultipleContext.Api.Product.Infrastructure.Repositories.Interfaces.Executable;

namespace MultipleContext.Api.Product.Application.Features.Product.Command.CreateProduct;

public class CreateProductCommandHandler:IRequestHandler<CreateProductCommand,ProductDto>
{
    private readonly IExecutableProductRepository _executableProductRepository;
    public CreateProductCommandHandler(IExecutableProductRepository executableProductRepository)
    {
        _executableProductRepository = executableProductRepository ?? throw new ArgumentNullException(nameof(executableProductRepository));
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var model = new Domain.Entities.Product
        {
            CreatedAt = DateTime.Now,
            Name = request.Name
        };
        
        var isCreated=await _executableProductRepository.AddAsync(model);
        if (!isCreated)
            throw new Exception("Yeni bir Product ekleme işlemi sırasında hata oluştu");

        return new ProductDto
        {
            CreatedAt = model.CreatedAt,
            IsDeleted = false,
            Id = model.Id,
            Name = model.Name
        };
    }
}