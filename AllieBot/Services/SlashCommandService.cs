using System.Net;

namespace AllieBot.Services
{
    public class SlashCommandService
    {
        private readonly HttpClient _httpClient;
        private readonly string dotaApiUrl;

        public SlashCommandService()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All
            };

            _httpClient = new HttpClient();
            dotaApiUrl = "https://api.opendota.com/api/";
        }

        public async Task<string> GetPlayerAsync(string playerId)
        {
            //get request to dota open api
            using HttpResponseMessage response = await _httpClient.GetAsync($"{dotaApiUrl}players/{playerId}");

            //return results
            return await response.Content.ReadAsStringAsync();
        }
    }
}
