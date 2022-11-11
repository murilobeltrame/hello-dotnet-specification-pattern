using Microsoft.AspNetCore.Mvc;
using System.Net;
using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Exceptions;

namespace SpecificationPattern.Api.Controllers
{
    public abstract class BaseCRUDController<TEntity, TCreatingPayload, TFilter> : ControllerBase
        where TEntity: BaseEntity
        where TFilter: IBaseFilter
	{
        protected readonly ILogger<BaseCRUDController<TEntity, TCreatingPayload, TFilter>> Logger;
        protected readonly IBaseCRUDUseCases<TEntity, TCreatingPayload, TFilter> UseCases;

        public BaseCRUDController(
            ILogger<BaseCRUDController<TEntity, TCreatingPayload, TFilter>> logger,
            IBaseCRUDUseCases<TEntity, TCreatingPayload, TFilter> useCases)
		{
            Logger = logger;
            UseCases = useCases;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Fetch([FromQuery] TFilter request)
        {
            return Ok(await UseCases.FetchAsync(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await UseCases.GetAsync(id));
            }
            catch (NotFoundException nfe)
            {
                Logger.LogWarning(nfe.Message);
                return NotFound(nfe.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] TCreatingPayload request)
        {
            try
            {
                var record = await UseCases.CreateAsync(request);
                return CreatedAtAction(nameof(Get), new { id = record.Id }, record);
            }
            catch (Domain.Exceptions.InvalidDataException ide)
            {
                Logger.LogWarning(ide.Message);
                throw;
            }

        }
    }
}

