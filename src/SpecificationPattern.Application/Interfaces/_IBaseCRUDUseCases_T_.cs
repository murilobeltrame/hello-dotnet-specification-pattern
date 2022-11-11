using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Interfaces
{
	public interface IBaseCRUDUseCases<TEntity, TCreatingPayload, TFilter>
        where TEntity : BaseEntity
        where TFilter : IBaseFilter
	{
        Task<TEntity> CreateAsync(TCreatingPayload payload, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FetchAsync(TFilter filter, CancellationToken cancellationToken = default);
    }

    public interface IBaseFilter
    {
        uint? Page { get; }
        ushort? Size { get; }
        string? Sort { get; }
    }
}

