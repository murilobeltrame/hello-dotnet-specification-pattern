using SpecificationPattern.Application.Interfaces;

namespace SpecificationPattern.Api.Controllers.Requests
{
    public class CreateWineryRequest : IWineryCreationPayload
    {
        public string Name { get; set; }
    }
}
