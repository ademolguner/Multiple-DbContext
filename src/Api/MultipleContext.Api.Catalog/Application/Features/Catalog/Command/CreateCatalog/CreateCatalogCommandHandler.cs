using MediatR;
using MultipleContext.Api.Catalog.Application.Models.Dto;
using MultipleContext.Api.Catalog.Features.Catalog.Command.CreateCatalog;
using MultipleContext.Api.Catalog.Infrastructure.Repositories.Interfaces.Executable;

namespace MultipleContext.Api.Catalog.Application.Features.Catalog.Command.CreateCatalog;

public class CreateCatalogCommandHandler:IRequestHandler<CreateCatalogCommand,CatalogDto>
{
    private readonly IExecutableCatalogRepository _executableCatalogRepository;
    public CreateCatalogCommandHandler(IExecutableCatalogRepository executableCatalogRepository)
    {
        _executableCatalogRepository = executableCatalogRepository ?? throw new ArgumentNullException(nameof(executableCatalogRepository));
    }

    public async Task<CatalogDto> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
    {
        var model = new Domain.Entities.Catalog
        {
            CreatedAt = DateTime.Now,
            Name = request.Name
        };
        
        var isCreated=await _executableCatalogRepository.AddAsync(model);
        if (!isCreated)
            throw new Exception("Yeni bir Catalog ekleme işlemi sırasında hata oluştu");

        return new CatalogDto
        {
            CreatedAt = model.CreatedAt,
            IsDeleted = false,
            Id = model.Id,
            Name = model.Name
        };
    }
}