using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Api.Controllers
{
    [Route("regions")]
    [ApiController]
    public class RegionsController : BaseCRUDController<Region, CreateRegionRequest, FetchRegionsRequest>
    {
        public RegionsController(
            ILogger<RegionsController> logger,
            IRegionUseCases regionUseCases) : base(logger, regionUseCases) { }
    }
}
