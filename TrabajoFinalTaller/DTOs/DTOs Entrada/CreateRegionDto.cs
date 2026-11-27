
using System.ComponentModel.DataAnnotations;

namespace TrabajoFinalTaller.DTOs.DTOsEntrada
{
    public class CreateRegionDto
    {
        [Required]
        public int CountryId { get; set; }

        [Required]
        public string Code { get; set; } = null!;

        [Required]
        public string LocalCode { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        public string? Keywords { get; set; }

        public string? WikipediaLink { get; set; }
    }
}

