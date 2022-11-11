using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Interfaces;
using SpecificationPattern.Domain.Specifications;

namespace SpecificationPattern.Application.UseCases
{
    public abstract class BaseCRUDUseCases<TEntity, TCreatingPayload, TFilter> : IBaseCRUDUseCases<TEntity, TCreatingPayload, TFilter>
        where TEntity : BaseEntity
        where TFilter : IBaseFilter
    {
        protected readonly IRepository<TEntity> Repository;

        public BaseCRUDUseCases(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        protected abstract Task<TEntity> ConfigureCreateEntityAsync(TCreatingPayload payload, CancellationToken cancellationToken = default);

        protected abstract ISpecification<TEntity> ConfigureSpecification(TFilter filter);

        public async Task<TEntity> CreateAsync(TCreatingPayload payload, CancellationToken cancellationToken = default)
        {
            var entity = await ConfigureCreateEntityAsync(payload, cancellationToken);
            var newRecord = await Repository.CreateAsync(entity, cancellationToken);
            await Repository.SaveChangesAsync(cancellationToken);
            return newRecord;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await Repository.DeleteAsync(id, cancellationToken);
            await Repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> FetchAsync(TFilter filter, CancellationToken cancellationToken = default)
        {
            var specification = ConfigureSpecification(filter);
            return await Repository.FetchAsync(specification, cancellationToken);
        }

        public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Repository.GetAsync(new GetEntityByIdSpecification<TEntity>(id), cancellationToken);
        }
    }

    public class GetEntityByIdSpecification<T> : BaseSpecification<T> where T : BaseEntity
    {
        public GetEntityByIdSpecification(Guid id)
        {
            WhereExpressions.Add(e => e.Id == id);
        }
    }
}
