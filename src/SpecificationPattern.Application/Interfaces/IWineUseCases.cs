using SpecificationPattern.Application.Commands.WineCommands;
using SpecificationPattern.Application.Queries;
using SpecificationPattern.Application.UseCases;
using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Interfaces
{
    public interface IWineUseCases
    {
        Task<WineOutput> CreateWineAsync(CreateWineCommand request, CancellationToken cancellationToken = default);
        Task<WineOutput> GetWineAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<WineOutput>> GetWinesAsync(WinesQuery filter, CancellationToken cancellationToken = default);
    }
}
