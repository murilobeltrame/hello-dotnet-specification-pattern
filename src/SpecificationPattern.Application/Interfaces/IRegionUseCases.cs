using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Interfaces
{
    public interface IRegionUseCases : IBaseCRUDUseCases<Region, CreateRegionRequest, FetchRegionsRequest> { }

    public class FetchRegionsRequest: IBaseFilter
    {
        public uint? Page { get; set; }
        public ushort? Size { get; set; }
        public string? Sort { get; set; }
        public string? Name { get; set; }
        public string? CountryName { get; set; }
    }

    public class CreateRegionRequest
    {
        public string Name { get; set; }
        public string CountryName { get; set; }
    }
}
