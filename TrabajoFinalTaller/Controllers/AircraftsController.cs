using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalTaller.Data;
using TrabajoFinalTaller.Models;

namespace TrabajoFinalTaller.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AircraftsController : ControllerBase
    {
        private readonly AirportsDbContext _context;

        public AircraftsController(AirportsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAircrafts()
        {
            var aircrafts = await _context.Aircrafts.ToListAsync();

            return Ok(aircrafts);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAircraft(Aircraft aircraft)
        {
            _context.Aircrafts.Add(aircraft);

            await _context.SaveChangesAsync();

            return StatusCode(201, aircraft);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAircraft(int id, Aircraft aircraft)
        {
            var aircraftExistente = await _context.Aircrafts
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aircraftExistente == null)
            {
                return NotFound("Avión no encontrado.");
            }

            aircraftExistente.Iatacode = aircraft.Iatacode;
            aircraftExistente.Name = aircraft.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAircraft(int id)
        {
            var aircraft = await _context.Aircrafts
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aircraft == null)
            {
                return NotFound("Avión no encontrado.");
            }

            _context.Aircrafts.Remove(aircraft);

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}