using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pokedex.Domain.Models.Base
{
    public class BaseModel
    {
        [JsonPropertyOrder(-1)] // 👈 Forces 'id' to appear first
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
