using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalTaller.Data;
using TrabajoFinalTaller.Models;

namespace TrabajoFinalTaller.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AirportsController : ControllerBase
    {
        private readonly AirportsDbContext _context;

        public AirportsController(AirportsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAirports()
        {
            var airports = await _context.Airports.ToListAsync();

            return Ok(airports);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAirportById(int id)
        {
            var airport = await _context.Airports
                .FirstOrDefaultAsync(a => a.Id == id);

            if (airport == null)
            {
                return NotFound("Aeropuerto no encontrado.");
            }

            return Ok(airport);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAirport(int id, Airport airport)
        {
            var airportExistente = await _context.Airports
                .FirstOrDefaultAsync(a => a.Id == id);

            if (airportExistente == null)
            {
                return NotFound("Aeropuerto no encontrado.");
            }

            airportExistente.AirportCode = airport.AirportCode;
            airportExistente.Name = airport.Name;
            airportExistente.City = airport.City;
            airportExistente.Municipality = airport.Municipality;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(int id)
        {
            var airport = await _context.Airports.FindAsync(id);

            if (airport == null)
            {
                return NotFound("Aeropuerto no encontrado.");
            }

            _context.Airports.Remove(airport);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}