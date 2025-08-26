using Pokedex.Domain.Models.Enum;
using System.Text.Json.Serialization;

namespace Pokedex.Domain.Models.Dto
{
    public class PokemonCriteriaDto : QueryParameters
    {
        public string? Name { get; set; }
        public string? GivenName { get; set; }
        public decimal? HeightMin { get; set; }
        public decimal? HeightMax { get; set; }
        public double? WeightMin { get; set; }
        public double? WeightMax { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender? Gender { get; set; }
        public List<string>? Types { get; set; }
        public List<string>? Weaknesses { get; set; }

    }
}
