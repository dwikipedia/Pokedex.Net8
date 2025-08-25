using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Models;
using Pokedex.Domain.Models.Dto;
using Pokedex.Domain.Models.DTO;
using Pokedex.Infrastructure;

namespace Pokedex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly PokedexContext _pokedexContext;

        public PokemonsController(PokedexContext pokedexContext)
        {
            _pokedexContext = pokedexContext;
            _pokedexContext.Database.EnsureCreated();
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllPokemon()
        {
            var pokemons = await _pokedexContext.Pokemons
                .Include(p => p.Types)
                .Include(p => p.Weaknesses)
                .ToArrayAsync();

            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPokemonById(int id)
        {
            var pokemon = await _pokedexContext.Pokemons
                .Include(p => p.Types)
                .Include(p => p.Weaknesses)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pokemon == null)
            {
                return NotFound();
            }

            return Ok(pokemon);
        }

        [HttpGet]
        public async Task<ActionResult> GetPokemonByCriteria([FromQuery] PokemonCriteriaDto dto)
        {
            var query = _pokedexContext.Pokemons
                .Include(p => p.Types)
                .Include(p => p.Weaknesses)
                .AsQueryable();

            if (!string.IsNullOrEmpty(dto.Name))
                query = query.Where(p => p.Name.ToLower().Contains(dto.Name.ToLower()));

            if (!string.IsNullOrEmpty(dto.GivenName))
                query = query.Where(p => p.GivenName.ToLower().Contains(dto.GivenName.ToLower()));

            if (dto.HeightMin.HasValue)
                query = query.Where(p => p.Height >= dto.HeightMin.Value);

            if (dto.HeightMax.HasValue)
                query = query.Where(p => p.Height <= dto.HeightMax.Value);

            if (dto.WeightMin.HasValue)
                query = query.Where(p => p.Weight >= dto.WeightMin.Value);

            if (dto.WeightMax.HasValue)
                query = query.Where(p => p.Weight <= dto.WeightMax.Value);

            if (dto.Gender.HasValue)
                query = query.Where(p => p.Gender == dto.Gender.Value);

            if (dto.Types?.Count > 0)
            {
                var loweredTypes = dto.Types.Select(t => t.ToLower()).ToList();
                query = query.Where(p => p.Types.Any(t => loweredTypes.Contains(t.Name.ToLower())));
            }

            if (dto.Weaknesses?.Count > 0)
            {
                var loweredWeaknesses = dto.Weaknesses.Select(w => w.ToLower()).ToList();
                query = query.Where(p => p.Weaknesses.Any(w => loweredWeaknesses.Contains(w.Name.ToLower())));
            }

            var results = await query.ToListAsync();
            if (results.Count == 0)
            {
                return NotFound(new
                {
                    Message = "No Pokémon matched the search criteria.",
                    Timestamp = DateTime.UtcNow,
                    Criteria = dto
                });
            }

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult> AddPokemon([FromBody] PokemonDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Invalid Pokémon data",
                    Detail = "The request body is missing or malformed.",
                    Status = 400,
                    Instance = HttpContext.Request.Path
                });
            }

            try
            {
                var newPokemon = new Pokemon
                {
                    Name = dto.Name,
                    GivenName = string.IsNullOrEmpty(dto.GivenName) ? dto.Name : dto.GivenName,
                    Gender = dto.Gender,
                    Height = dto.Height,
                    Weight = dto.Weight,
                    Types = dto.Types,
                    Weaknesses = dto.Weaknesses,
                    SkillId = dto.SkillId
                };

                _pokedexContext.Pokemons.Add(newPokemon);
                await _pokedexContext.SaveChangesAsync();

                return CreatedAtAction(nameof(AddPokemon), new { id = newPokemon.Id }, newPokemon);
            }
            catch (Exception ex)
            {
                // Optional: log the error
                //_logger.LogError(ex, "Error saving Pokémon");

                return StatusCode(500, new ProblemDetails
                {
                    Title = "Failed to save Pokémon data",
                    Detail = ex.Message,
                    Status = 500,
                    Instance = HttpContext.Request.Path
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPokemon(int id, [FromBody] PokemonDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pokemon = await _pokedexContext.Pokemons.FindAsync(id);

            if (pokemon == null)
            {
                return NotFound();
            }

            try
            {
                pokemon.Name = dto.Name;
                pokemon.GivenName = string.IsNullOrEmpty(dto.GivenName) ? dto.Name : dto.GivenName;
                pokemon.Gender = dto.Gender;
                pokemon.Height = dto.Height;
                pokemon.Weight = dto.Weight;
                pokemon.Types = dto.Types;
                pokemon.Weaknesses = dto.Weaknesses;
                pokemon.SkillId = dto.SkillId;

                await _pokedexContext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Q: What's AsNoTracking?
                //A: It signals clearly: “I’m just checking existence, not modifying or reusing this entity.”
                if (!await _pokedexContext.Pokemons.AsNoTracking().AnyAsync(p => p.Id == id))
                {
                    return NotFound();
                }

                return Conflict(new ProblemDetails
                {
                    Title = "Concurrency conflict",
                    Detail = "The Pokémon was modified by another process. Please reload and try again.",
                    Status = 409,
                    Instance = HttpContext.Request.Path
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePokemon(int id)
        {
            var pokemon = await _pokedexContext.Pokemons.FindAsync(id);

            if (pokemon == null) return NotFound();

            _pokedexContext.Pokemons.Remove(pokemon);
            await _pokedexContext.SaveChangesAsync();

            return Ok(pokemon);
        }
    }
}
