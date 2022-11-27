using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TProjection> GetAsync<TProjection>(ISpecification<TEntity, TProjection> specification, CancellationToken cancellationToken);

        Task<IEnumerable<TProjection>> FetchAsync<TProjection>(ISpecification<TEntity, TProjection> specification, CancellationToken cancellationToken);

        Task<TEntity> CreateAsync(TEntity record, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity record, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
