using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Models;
using Pokedex.Infrastructure.Helpers;

namespace Pokedex.Infrastructure
{
    public class PokedexContext : DbContext
    {
        public PokedexContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Skill)
                .WithMany(s => s.Pokemons)
                .HasForeignKey(p => p.SkillId);

            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.Types)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "PokemonTypes",
                    r => r.HasOne<Element>().WithMany().HasForeignKey("ElementId"),
                    l => l.HasOne<Pokemon>().WithMany().HasForeignKey("PokemonId"),
                    je =>
                    {
                        je.HasData(
                            new { PokemonId = 1, ElementId = 1 }, // Charmander → Fire
                            new { PokemonId = 2, ElementId = 3 }, // Pikachu → Electric
                            new { PokemonId = 3, ElementId = 2 }, // Squirtle → Water
                            new { PokemonId = 4, ElementId = 4 }, // Onix → Rock
                            new { PokemonId = 4, ElementId = 5 }, // Onix → Ground
                            new { PokemonId = 5, ElementId = 6 }, // Gengar → Ghost
                            new { PokemonId = 5, ElementId = 7 },  // Gengar → Poison
                            new { PokemonId = 6, ElementId = 1 }, // Charizard → Fire
                            new { PokemonId = 6, ElementId = 14 }, // Charizard → Flying
                            new { PokemonId = 7, ElementId = 2 }, // Lapras → Water
                            new { PokemonId = 7, ElementId = 9 }, // Lapras → Ice
                            new { PokemonId = 8, ElementId = 15 }, // Scizor → Bug
                            new { PokemonId = 8, ElementId = 16 }  // Scizor → Steel
                        );
                    });

            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.Weaknesses)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "PokemonWeaknesses",
                    r => r.HasOne<Element>().WithMany().HasForeignKey("ElementId"),
                    l => l.HasOne<Pokemon>().WithMany().HasForeignKey("PokemonId"),
                    je =>
                    {
                        je.HasData(
                            new { PokemonId = 1, ElementId = 2 },
                            new { PokemonId = 1, ElementId = 4 },
                            new { PokemonId = 1, ElementId = 5 },
                            new { PokemonId = 2, ElementId = 5 },
                            new { PokemonId = 3, ElementId = 3 },
                            new { PokemonId = 3, ElementId = 9 },
                            new { PokemonId = 4, ElementId = 2 },
                            new { PokemonId = 4, ElementId = 9 },
                            new { PokemonId = 4, ElementId = 10 },
                            new { PokemonId = 4, ElementId = 11 },
                            new { PokemonId = 4, ElementId = 5 },
                            new { PokemonId = 4, ElementId = 12 },
                            new { PokemonId = 5, ElementId = 8 },
                            new { PokemonId = 5, ElementId = 5 },
                            new { PokemonId = 5, ElementId = 6 },
                            new { PokemonId = 5, ElementId = 13 },
                            new { PokemonId = 6, ElementId = 2 },
                            new { PokemonId = 6, ElementId = 9 },
                            new { PokemonId = 6, ElementId = 5 },
                            new { PokemonId = 7, ElementId = 3 },
                            new { PokemonId = 7, ElementId = 6 },
                            new { PokemonId = 8, ElementId = 1 },
                            new { PokemonId = 8, ElementId = 17 }
                        );
                    });

            modelBuilder.Entity<Pokemon>()
                .Property(p => p.Height)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Pokemon>()
                .Property(p => p.Weight)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Pokemon>()
                .Property(p => p.Gender)
                .HasConversion<int>();


            modelBuilder.Seed();
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Element> Elements { get; set; }

    }
}
