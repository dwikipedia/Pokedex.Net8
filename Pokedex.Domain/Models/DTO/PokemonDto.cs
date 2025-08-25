using Pokedex.Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pokedex.Domain.Models.Dto
{
    public class PokemonDto
    {
        [Required]
        public string Name { get; set; }
        public string? GivenName { get; set; }
        public decimal? Height { get; set; }
        public double? Weight { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender? Gender { get; set; }
        public List<Element>? Types { get; set; }
        public List<Element>? Weaknesses { get; set; }
        public int? SkillId { get; set; }
    }
}
