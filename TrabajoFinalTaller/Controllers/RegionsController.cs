using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalTaller.Data;
using TrabajoFinalTaller.Models;
using TrabajoFinalTaller.DTOs.DTOsEntrada;
using TrabajoFinalTaller.DTOs.DTOsSalida;

namespace TrabajoFinalTaller.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly AirportsDbContext _context;

        public RegionsController(AirportsDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion(CreateRegionDto dto)
        {
            // Validar que exista el país
            var countryExists = await _context.Countries
                .AnyAsync(c => c.Id == dto.CountryId);

            if (!countryExists)
            {
                return NotFound("El país indicado no existe.");
            }

            // Crear la región
            var region = new Region
            {
                CountryId = dto.CountryId,
                Code = dto.Code,
                LocalCode = dto.LocalCode,
                Name = dto.Name,
                Keywords = dto.Keywords,
                WikipediaLink = dto.WikipediaLink
            };

            // Guardar en la base de datos
            _context.Regions.Add(region);

            await _context.SaveChangesAsync();

            // DTO de salida
            var resultado = new RegionDto
            {
                Id = region.Id,
                CountryId = region.CountryId,
                Code = region.Code,
                LocalCode = region.LocalCode,
                Name = region.Name,
                Keywords = region.Keywords,
                WikipediaLink = region.WikipediaLink
            };

            return StatusCode(201, resultado);
        }
        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            var regions = await _context.Regions
                .Select(r => new RegionDto
                {
                    Id = r.Id,
                    CountryId = r.CountryId,
                    Code = r.Code,
                    LocalCode = r.LocalCode,
                    Name = r.Name,
                    Keywords = r.Keywords,
                    WikipediaLink = r.WikipediaLink
                })
                .ToListAsync();

            return Ok(regions);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegionById(int id)
        {
            var region = await _context.Regions
                .Where(r => r.Id == id)
                .Select(r => new RegionDto
                {
                    Id = r.Id,
                    CountryId = r.CountryId,
                    Code = r.Code,
                    LocalCode = r.LocalCode,
                    Name = r.Name,
                    Keywords = r.Keywords,
                    WikipediaLink = r.WikipediaLink
                })
                .FirstOrDefaultAsync();

            if (region == null)
            {
                return NotFound("Región no encontrada.");
            }

            return Ok(region);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegion(int id, CreateRegionDto dto)
        {
            var region = await _context.Regions
                .FirstOrDefaultAsync(r => r.Id == id);

            if (region == null)
            {
                return NotFound("Región no encontrada.");
            }

            region.CountryId = dto.CountryId;
            region.Code = dto.Code;
            region.LocalCode = dto.LocalCode;
            region.Name = dto.Name;
            region.Keywords = dto.Keywords;
            region.WikipediaLink = dto.WikipediaLink;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            var region = await _context.Regions
                .FirstOrDefaultAsync(r => r.Id == id);

            if (region == null)
            {
                return NotFound("Región no encontrada.");
            }

            _context.Regions.Remove(region);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}