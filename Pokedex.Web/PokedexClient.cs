using System.Net.Http.Headers;

namespace Pokedex.Client
{
    public class PokedexClient
    {
        private readonly HttpClient _httpClient;

        public PokedexClient(string token)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<string> GetPokemonsAsync()
        {
            var response = await _httpClient.GetAsync("https://yourapi.com/api/v1.0/Pokemons");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

}
