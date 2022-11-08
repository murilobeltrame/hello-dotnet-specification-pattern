using SpecificationPattern.Application.Interfaces;

namespace SpecificationPattern.Api.Controllers.Requests
{
    public class FetchWinesRequest : IWineFilter
    {
        public uint? Page { get; set; }
        public ushort? Size { get; set; }
        public string? Sort { get; set; }
        public string? WineryName { get; set; }
        public string? Label { get; set; }
        public string? GrapeName { get; set; }
        public string? RegionName { get; set; }
        public string? CountryName { get; set; }
    }
}
