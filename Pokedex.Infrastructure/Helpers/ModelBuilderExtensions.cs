using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Models;
using Pokedex.Domain.Models.Enum;

namespace Pokedex.Infrastructure.Helpers
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    Id = 1,
                    Name = "Flamethrower"
                },
                new Skill
                {
                    Id = 2,
                    Name = "Thunderbolt"
                },
                new Skill
                {
                    Id = 3,
                    Name = "Watergun"
                },
                new Skill
                {
                    Id = 4,
                    Name = "Shadow Ball"
                },
                new Skill
                {
                    Id = 5,
                    Name = "Rock Throw"
                });
            modelBuilder.Entity<Element>().HasData(
                new Element { Id = 1, Name = "Fire" },
                new Element { Id = 2, Name = "Water" },
                new Element { Id = 3, Name = "Electric" },
                new Element { Id = 4, Name = "Rock" },
                new Element { Id = 5, Name = "Ground" },
                new Element { Id = 6, Name = "Ghost" },
                new Element { Id = 7, Name = "Poison" },
                new Element { Id = 8, Name = "Psychic" },
                new Element { Id = 9, Name = "Grass" },
                new Element { Id = 10, Name = "Ice" },
                new Element { Id = 11, Name = "Fighting" },
                new Element { Id = 12, Name = "Steel" },
                new Element { Id = 13, Name = "Dark" }
            );
            modelBuilder.Entity<Pokemon>().HasData(
                new Pokemon
                {
                    Id = 1,
                    GivenName = "Siganteng",
                    Name = "Charmander",
                    Height = 1.5m,
                    Weight = 20.5,
                    Gender = Gender.Male,
                    SkillId = 1,
                },
                new Pokemon
                {
                    Id = 2,
                    GivenName = "Thunderpaw",
                    Name = "Pikachu",
                    Height = 0.4m,
                    Weight = 6.0,
                    Gender = Gender.Male,
                    SkillId = 2
                },
                new Pokemon
                {
                    Id = 3,
                    GivenName = "Aquaflare",
                    Name = "Squirtle",
                    Height = 0.5m,
                    Weight = 9.0,
                    Gender = Gender.Female,
                    SkillId = 3
                },
                new Pokemon
                {
                    Id = 4,
                    GivenName = "Rockbuster",
                    Name = "Onix",
                    Height = 8.8m,
                    Weight = 210.0,
                    Gender = Gender.Male,
                    SkillId = 5
                },
                new Pokemon
                {
                    Id = 5,
                    GivenName = "Shadowfang",
                    Name = "Gengar",
                    Height = 1.5m,
                    Weight = 40.5,
                    Gender = Gender.Female,
                    SkillId = 4
                }
            );
        }
    }
}
