using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Interfaces;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Application.UseCases
{
    public class WineUseCases : IWineUseCases
    {
        private readonly IRepository<Wine> _wineRepository;
        private readonly IRepository<Winery> _wineryRepository;
        private readonly IRepository<Grape> _grapeRepository;
        private readonly IRepository<Region> _regionRepository;

        public WineUseCases(
            IRepository<Wine> wineRepository,
            IRepository<Winery> wineryRepository,
            IRepository<Grape> grapeRepository,
            IRepository<Region> regionRepository)
        {
            _wineRepository = wineRepository;
            _wineryRepository = wineryRepository;
            _grapeRepository = grapeRepository;
            _regionRepository = regionRepository;
        }

        public async Task<Wine> CreateWineAsync(IWineCreationPayload winePayload, CancellationToken cancellationToken = default)
        {
            var winery = await _wineryRepository.GetAsync(new GetWineryByNameSpecification(winePayload.WineryName), cancellationToken);
            var region = await _regionRepository.GetAsync(new GetRegionByNameSpecification(winePayload.RegionName), cancellationToken);
            var grapes = await _grapeRepository.FetchAsync(new FetchGrapesByNameSpecification(winePayload.GrapeNames.Split(',')), cancellationToken);

            var wine = new Wine(winery, winePayload.Label, region, grapes);
            var createdWine = await _wineRepository.CreateAsync(wine, cancellationToken);
            await _wineRepository.SaveChangesAsync(cancellationToken);
            return createdWine;
        }

        public async Task<Wine> GetWineAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _wineRepository.GetAsync(new GetWineByIdSpecification(id), cancellationToken);
        }

        public async Task<object?> GetWinesAsync(IWineFilter filter, CancellationToken cancellationToken = default)
        {
            var skip = filter.Page * filter.Size;
            var spec = new FetchWinesByFilterSpecification(
                filter.WineryName,
                filter.Label,
                filter.GrapeName,
                filter.RegionName,
                filter.CountryName,
                filter.Sort,
                skip,
                filter.Size);
            return await _wineRepository.FetchAsync(spec, cancellationToken);
        }
    }
}
