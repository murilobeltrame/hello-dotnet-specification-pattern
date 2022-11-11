using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Interfaces;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Application.UseCases
{
    public class CountryUseCases : BaseCRUDUseCases<Country, CreateCountryRequest, FetchCountriesRequest>, ICountryUseCases
    {
        public CountryUseCases(IRepository<Country> repository) : base(repository) { }

        protected override Task<Country> ConfigureCreateEntityAsync(CreateCountryRequest payload, CancellationToken cancellationToken = default) =>
            Task.FromResult(new Country(payload.Name));

        protected override ISpecification<Country> ConfigureSpecification(FetchCountriesRequest filter) =>
            new FetchCountryByFilterSpecification(filter.Name, filter.Sort, filter.Page * filter.Size, filter.Size);
    }
}

