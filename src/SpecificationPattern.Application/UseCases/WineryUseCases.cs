using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Interfaces;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Application.UseCases
{
    public class WineryUseCases : BaseCRUDUseCases<Winery, CreateWineryRequest, FetchWineriesRequest>, IWineryUseCases
    {
        public WineryUseCases(IRepository<Winery> repository) : base(repository) { }

        protected override Task<Winery> ConfigureCreateEntityAsync(CreateWineryRequest payload, CancellationToken cancellationToken = default) =>
            Task.FromResult(new Winery(payload.Name));

        protected override ISpecification<Winery> ConfigureSpecification(FetchWineriesRequest filter) =>
            new FetchWineryByFilterSpecification(filter.Name, filter.Sort, filter.Page * filter.Size, filter.Size);
    }
}
