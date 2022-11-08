using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.Api.Controllers.Requests;
using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Exceptions;
using System.Net;
using InvalidDataException = SpecificationPattern.Domain.Exceptions.InvalidDataException;

namespace SpecificationPattern.Api.Controllers
{
    [Route("wineries")]
    [ApiController]
    public class WineriesController : ControllerBase
    {
        private readonly ILogger<WineriesController> _logger;
        private readonly IWineryUseCases _wineryUseCases;

        public WineriesController(ILogger<WineriesController> logger, IWineryUseCases wineryUseCases)
        {
            _logger = logger;
            _wineryUseCases = wineryUseCases;
        }

        [HttpGet("{id}", Name = nameof(GetWinery))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Winery))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetWinery(Guid id)
        {
            try
            {
                return Ok(await _wineryUseCases.GetWineAsync(id));
            }
            catch (NotFoundException nfe)
            {
                _logger.LogWarning(nfe.Message);
                return NotFound(nfe.Message);
            }
        }

        [HttpGet(Name = nameof(FetchWineries))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Winery>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FetchWineries([FromQuery] FetchWineriesRequest request)
        {
            return Ok(await _wineryUseCases.GetWinesAsync(request));
        }

        [HttpPost(Name = nameof(CreateWinery))]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(IEnumerable<Winery>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateWinery([FromBody] CreateWineryRequest request)
        {
            try
            {
                var winery = await _wineryUseCases.CreateWineAsync(request);
                return CreatedAtAction(nameof(GetWinery), new { id = winery.Id }, winery);
            }
            catch (InvalidDataException ide)
            {
                _logger.LogWarning(ide.Message);
                throw;
            }

        }
    }
}
