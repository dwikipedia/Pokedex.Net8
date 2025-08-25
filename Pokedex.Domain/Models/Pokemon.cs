using Pokedex.Domain.Models.Base;
using Pokedex.Domain.Models.Enum;
using System.Text.Json.Serialization;

namespace Pokedex.Domain.Models
{
    public class Pokemon : BaseModel
    {
        public string? GivenName { get; set; }
        public List<Element>? Types { get; set; }
        public List<Element>? Weaknesses { get; set; }
        public decimal? Height { get; set; }
        public double? Weight { get; set; }

        public Gender? Gender { get; set; }
        public int? SkillId { get; set; }

        [JsonIgnore]
        //virtual = lazy loading
        public virtual Skill? Skill { get; set; }
    }
}
