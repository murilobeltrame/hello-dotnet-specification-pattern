using SpecificationPattern.Application.Interfaces;

namespace SpecificationPattern.Api.Controllers.Requests
{
    public class FetchWineriesRequest : IWineryFilter
    {
        public uint? Page { get; set; }

        public ushort? Size { get; set; }

        public string? Sort { get; set; }

        public string? Name { get; set; }
    }
}
