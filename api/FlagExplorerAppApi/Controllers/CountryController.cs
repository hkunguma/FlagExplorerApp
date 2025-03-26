using FlagExplorerApp.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlagExplorerAppApi.Controllers
{
    /// <summary>
    /// Country API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService) 
        {
            this._countryService = countryService;
        }

        /// <summary>
        /// Retrieve all countries
        /// </summary>
        /// <returns></returns>
        [HttpGet("/countries")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Country>))]
        public async Task<IActionResult> Get()
        {
            var countries = await _countryService.GetAllCountries();
            return Ok(countries);
        }

        /// <summary>
        /// Retrieve details about a specific country
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("/countries/{name}")]
        public async Task<IActionResult> GetCountryByName(string name)
        {
            var countryDetail = await _countryService.GetCountryByName(name);
            return Ok(countryDetail);
        }
    }
}
