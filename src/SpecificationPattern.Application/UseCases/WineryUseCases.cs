using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Interfaces;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Application.UseCases
{
    public class WineryUseCases : IWineryUseCases
    {
        private readonly IRepository<Winery> _wineryRepository;

        public WineryUseCases(IRepository<Winery> wineryRepository)
        {
            _wineryRepository = wineryRepository;
        }

        public async Task<Winery> CreateWineAsync(IWineryCreationPayload payload, CancellationToken cancellationToken = default)
        {
            var winery = new Winery(payload.Name);
            return await _wineryRepository.CreateAsync(winery, cancellationToken);
        }

        public async Task<Winery> GetWineAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _wineryRepository.GetAsync(new GetWineryByIdSpecification(id), cancellationToken);
        }

        public async Task<object?> GetWinesAsync(IWineryFilter filter, CancellationToken cancellationToken = default)
        {
            var skip = filter.Page * filter.Size;
            var spec = new FetchWineryByFilterSpecification(
                filter.Name,
                filter.Sort,
                skip,
                filter.Size);
            return await _wineryRepository.FetchAsync(spec, cancellationToken);
        }
    }
}
