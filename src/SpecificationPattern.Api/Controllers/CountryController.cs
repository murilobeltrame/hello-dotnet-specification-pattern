using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Api.Controllers
{
    [Route("countries")]
    [ApiController]
    public class CountryController: BaseCRUDController<Country, CreateCountryRequest, FetchCountriesRequest>
	{
		public CountryController(
			ILogger<CountryController> logger,
			ICountryUseCases countryUseCases) : base(logger, countryUseCases) { }
	}
}
