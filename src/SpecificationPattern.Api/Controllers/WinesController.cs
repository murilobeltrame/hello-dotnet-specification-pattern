using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.Application.Commands.WineCommands;
using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Application.Queries;
using SpecificationPattern.Domain.Exceptions;
using System.Net;
using InvalidDataException = SpecificationPattern.Domain.Exceptions.InvalidDataException;

namespace SpecificationPattern.Api.Controllers
{
    [ApiController]
    [Route("wines")]
    public class WinesController : ControllerBase
    {
        private readonly ILogger<WinesController> _logger;
        private readonly IWineUseCases _wineUseCases;

        public WinesController(ILogger<WinesController> logger, IWineUseCases wineUseCases)
        {
            _logger = logger;
            _wineUseCases = wineUseCases;
        }

        [HttpGet("{id}", Name = nameof(GetWine))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(WineOutput))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetWine(Guid id)
        {
            try
            {
                var wine = await _wineUseCases.GetWineAsync(id);
                return Ok(wine);
            }
            catch (NotFoundException nfe)
            {
                _logger.LogWarning(nfe.Message);
                return NotFound(nfe.Message);
            }
        }

        [HttpGet(Name = nameof(FetchWines))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<WineOutput>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FetchWines([FromQuery] WinesQuery query)
        {
            return Ok(await _wineUseCases.GetWinesAsync(query));
        }

        [HttpPost(Name = nameof(CreateWine))]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(WineOutput))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateWine([FromBody] CreateWineCommand request)
        {
            try
            {
                var wine = await _wineUseCases.CreateWineAsync(request);
                return CreatedAtAction(nameof(GetWine), new { id = wine.Id }, wine);
            }
            catch (InvalidDataException ide)
            {
                _logger.LogWarning(ide.Message);
                throw;
            }

        }
    }
}
