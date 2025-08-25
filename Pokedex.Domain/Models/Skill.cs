using Pokedex.Domain.Models.Base;

namespace Pokedex.Domain.Models
{
    public class Skill : BaseModel
    {
        public virtual ICollection<Pokemon> Pokemons { get; set; }

    }
}
