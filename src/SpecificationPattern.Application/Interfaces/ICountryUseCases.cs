using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Application.Interfaces
{
    public interface ICountryUseCases : IBaseCRUDUseCases<Country, CreateCountryRequest, FetchCountriesRequest> { }

    public class FetchCountriesRequest: IBaseFilter
    {
        public string? Name { get; set; }

        public uint? Page { get; set; }

        public ushort? Size { get; set; }

        public string? Sort { get; set; }
    }

    public class CreateCountryRequest
    {
        public string Name { get; set; }
    }
}
