using SpecificationPattern.Application.Commands.WineCommands;
using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Application.Queries;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Application.UseCases
{
    

    public class WineUseCases : IWineUseCases
    {
        private readonly IRepository<Wine> _wineRepository;
        private readonly IRepository<Winery> _wineryRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Grape> _grapeRepository;
        private readonly IRepository<Region> _regionRepository;

        public WineUseCases(
            IRepository<Wine> wineRepository,
            IRepository<Winery> wineryRepository,
            IRepository<Country> countryRepository,
            IRepository<Grape> grapeRepository,
            IRepository<Region> regionRepository)
        {
            _wineRepository = wineRepository;
            _wineryRepository = wineryRepository;
            _countryRepository = countryRepository;
            _grapeRepository = grapeRepository;
            _regionRepository = regionRepository;
        }

        public async Task<WineOutput> CreateWineAsync(CreateWineCommand createWineCommand, CancellationToken cancellationToken = default)
        {
            var winery = new Winery(createWineCommand.WineryName);
            try
            {
                winery = await _wineryRepository.GetAsync(new GetWineryByNameSpecification(createWineCommand.WineryName), cancellationToken);
            }
            catch (Exception) { }

            var region = new Region(createWineCommand.RegionName, new Country(createWineCommand.CountryName));
            try
            {
                region = await _regionRepository.GetAsync(new GetRegionByNameSpecification(createWineCommand.RegionName), cancellationToken);
            }
            catch (Exception) { }

            var grapes = createWineCommand.GrapeNames.Select(grapeName => new Grape(grapeName, GrapeColor.Red)).ToList();
            try
            {
                var foundGrapes = await _grapeRepository.FetchAsync(new FetchGrapesByNameSpecification(createWineCommand.GrapeNames), cancellationToken);
                if (foundGrapes.Any()) grapes = foundGrapes.ToList();
            }
            catch (Exception) { }

            var wine = new Wine(winery, createWineCommand.Label, region, grapes);
            var createdWine = await _wineRepository.CreateAsync(wine, cancellationToken);
            await _wineRepository.SaveChangesAsync(cancellationToken);
            return WineOutput.FromEntity(createdWine);
        }

        public async Task<WineOutput> GetWineAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _wineRepository.GetAsync(new GetWineByIdSpecification(id), cancellationToken);
        }

        public async Task<IEnumerable<WineOutput>> GetWinesAsync(WinesQuery query, CancellationToken cancellationToken = default)
        {
            var skip = query.Page * query.Size;
            var spec = new FetchWinesByFilterSpecification(
                query.WineryName,
                query.Label,
                query.GrapeName,
                query.RegionName,
                query.CountryName,
                query.Sort,
                skip,
                query.Size);
            return await _wineRepository.FetchAsync(spec, cancellationToken);
        }
    }
}
