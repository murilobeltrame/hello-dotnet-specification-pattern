using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.Api.Controllers.Requests;
using SpecificationPattern.Api.Controllers.Responses;
using SpecificationPattern.Application.Interfaces;
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(WineResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetWine(Guid id)
        {
            try
            {
                var wine = await _wineUseCases.GetWineAsync(id);
                var response = WineResponse.FromEntity(wine);
                return Ok(response);
            }
            catch (NotFoundException nfe)
            {
                _logger.LogWarning(nfe.Message);
                return NotFound(nfe.Message);
            }
        }

        [HttpGet(Name = nameof(FetchWines))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<WineResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FetchWines([FromQuery] FetchWinesRequest request)
        {
            return Ok(await _wineUseCases.GetWinesAsync(request));
        }

        [HttpPost(Name = nameof(CreateWine))]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(IEnumerable<WineResponse>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateWine([FromBody] CreateWineRequest request)
        {
            try
            {
                var wine = await _wineUseCases.CreateWineAsync(request);
                return CreatedAtAction(nameof(GetWine), new { id = wine.Id }, WineResponse.FromEntity(wine));
            }
            catch (InvalidDataException ide)
            {
                _logger.LogWarning(ide.Message);
                throw;
            }

        }
    }
}
