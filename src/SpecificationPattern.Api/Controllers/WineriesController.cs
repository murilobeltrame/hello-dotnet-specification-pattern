using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Api.Controllers
{
    [Route("wineries")]
    [ApiController]
    public class WineriesController : BaseCRUDController<Winery, CreateWineryRequest, FetchWineriesRequest>
    {
        public WineriesController(
            ILogger<WineriesController> logger,
            IWineryUseCases wineryUseCases) : base(logger, wineryUseCases) { }
    }
}
