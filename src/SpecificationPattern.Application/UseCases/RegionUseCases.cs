using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Interfaces;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Application.UseCases
{
    public class RegionUseCases: BaseCRUDUseCases<Region, CreateRegionRequest, FetchRegionsRequest>, IRegionUseCases
	{
        private readonly IRepository<Country> _countryRepository;

        public RegionUseCases(
            IRepository<Region> repository,
            IRepository<Country> countryRepository) : base(repository)
		{
            _countryRepository = countryRepository;

        }

        protected override async Task<Region> ConfigureCreateEntityAsync(CreateRegionRequest payload, CancellationToken cancellationToken = default)
        {
            var country = await _countryRepository.GetAsync(new GetCountryByNameSpecification(payload.CountryName), cancellationToken);
            return new Region(payload.Name, country);
        }

        protected override ISpecification<Region> ConfigureSpecification(FetchRegionsRequest filter) =>
            new FetchRegionByFilterSpecification(filter.Name, filter.CountryName, filter.Sort, filter.Page * filter.Size, filter.Size);
    }
}

