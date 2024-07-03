using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess.DTOs;
using Shop.Service.IService;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var countries = _countryService.GetAllCountries();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var country = _countryService.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CountryDto countryDto)
        {
            _countryService.CreateCountry(countryDto);
            return CreatedAtAction(nameof(Get), new { id = countryDto.CountryId }, countryDto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CountryDto countryDto)
        {
            if (id != countryDto.CountryId)
            {
                return BadRequest();
            }
            _countryService.UpdateCountry(countryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _countryService.DeleteCountry(id);
            return NoContent();
        }
    }
}
