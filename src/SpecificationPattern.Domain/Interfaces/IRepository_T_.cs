using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<IEnumerable<T>> FetchAsync(ISpecification<T> specification, CancellationToken cancellationToken);

        Task<T> CreateAsync(T record, CancellationToken cancellationToken);

        Task UpdateAsync(T record, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    }
}
