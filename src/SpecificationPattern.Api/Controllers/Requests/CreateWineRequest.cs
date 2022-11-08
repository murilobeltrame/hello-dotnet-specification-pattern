using SpecificationPattern.Application.Interfaces;

namespace SpecificationPattern.Api.Controllers.Requests
{
    public class CreateWineRequest : IWineCreationPayload
    {
        public string WineryName { get; set; }

        public string RegionName { get; set; }

        public string Label { get; set; }

        public string GrapeNames { get; set; }
    }
}
