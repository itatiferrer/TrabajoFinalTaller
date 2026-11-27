using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalTaller.Data;
using TrabajoFinalTaller.Models;

namespace TrabajoFinalTaller.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly AirportsDbContext _context;

        public CountriesController(AirportsDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/countries/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return NotFound("País no encontrado.");
            }

            return Ok(new
            {
                country.Id,
                country.Code,
                country.Name,
                country.Continent,
                country.Keywords,
                country.WikipediaLink
            });
        }

        // POST: api/v1/countries
        [HttpPost]
        public async Task<IActionResult> CreateCountry(Country country)
        {
            _context.Countries.Add(country);

            await _context.SaveChangesAsync();

            return StatusCode(201, country);
        }
    }
}