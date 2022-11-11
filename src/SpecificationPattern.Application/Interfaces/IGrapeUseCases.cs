using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Interfaces
{
    public interface IGrapeUseCases : IBaseCRUDUseCases<Grape, IGrapeCreationPayload, IGrapeFilter> { }

    public interface IGrapeFilter: IBaseFilter
    {
        string? Name { get; }
        string? Color { get; }
    }

    public interface IGrapeCreationPayload
    {
        string Name { get; }
        string Color { get; }
    }
}
