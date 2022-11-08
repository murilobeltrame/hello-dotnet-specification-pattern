using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Interfaces
{
    public interface IWineUseCases
    {
        Task<Wine> CreateWineAsync(IWineCreationPayload request, CancellationToken cancellationToken = default);
        Task<Wine> GetWineAsync(Guid id, CancellationToken cancellationToken = default);
        Task<object?> GetWinesAsync(IWineFilter filter, CancellationToken cancellationToken = default);
    }

    public interface IWineFilter
    {
        uint? Page { get; }
        ushort? Size { get; }
        string? Sort { get; }
        string? WineryName { get; }
        string? Label { get; }
        string? GrapeName { get; }
        string? RegionName { get; }
        string? CountryName { get; }
    }

    public interface IWineCreationPayload
    {
        string WineryName { get; }
        string RegionName { get; }
        string Label { get; }
        string GrapeNames { get; }
    }
}
