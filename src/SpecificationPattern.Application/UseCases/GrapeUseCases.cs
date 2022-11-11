using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Interfaces;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Application.UseCases
{
    public class GrapeUseCases:BaseCRUDUseCases<Grape, IGrapeCreationPayload, IGrapeFilter>, IGrapeUseCases
	{
        public GrapeUseCases(IRepository<Grape> repository) : base(repository) { }

        protected override Task<Grape> ConfigureCreateEntityAsync(IGrapeCreationPayload payload, CancellationToken cancellationToken = default) =>
            Task.FromResult(new Grape(payload.Name, GrapeColor.Red));

        protected override ISpecification<Grape> ConfigureSpecification(IGrapeFilter filter) =>
            new FetchGrapesByFilterSpecification(filter.Name, filter.Color, filter.Sort, filter.Page * filter.Size, filter.Size);
    }
}

