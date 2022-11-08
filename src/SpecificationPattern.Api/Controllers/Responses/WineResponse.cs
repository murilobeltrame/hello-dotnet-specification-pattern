using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Api.Controllers.Responses
{
    public class WineResponse
    {
        public Guid Id { get; private set; }

        public string WineryName { get; private set; }

        public string Label { get; private set; }

        public string RegionName { get; private set; }

        public string CountryName { get; private set; }

        public IEnumerable<string> Grapes { get; set; }

        public WineResponse(Guid id, string wineryName, string label, string regionName, string countryName, IEnumerable<string> grapes)
        {
            Id = id;
            WineryName = wineryName;
            Label = label;
            RegionName = regionName;
            CountryName = countryName;
            Grapes = grapes;
        }

        public static WineResponse FromEntity(Wine wine) =>
            new WineResponse(wine.Id, wine.Winery.Name, wine.Label, wine.Region.Name, wine.Region.Country.Name, wine.Grapes.Select(g => g.Name));
    }
}
