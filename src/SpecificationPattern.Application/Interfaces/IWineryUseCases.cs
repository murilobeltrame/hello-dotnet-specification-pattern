using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Interfaces
{
    public interface IWineryUseCases
    {
        Task<Winery> CreateWineAsync(IWineryCreationPayload payload, CancellationToken cancellationToken = default);
        Task<Winery> GetWineAsync(Guid id, CancellationToken cancellationToken = default);
        Task<object?> GetWinesAsync(IWineryFilter filter, CancellationToken cancellationToken = default);
    }

    public interface IWineryFilter
    {
        uint? Page { get; }
        ushort? Size { get; }
        string? Sort { get; }
        string? Name { get; }
    }

    public interface IWineryCreationPayload
    {
        string Name { get; }
    }
}
