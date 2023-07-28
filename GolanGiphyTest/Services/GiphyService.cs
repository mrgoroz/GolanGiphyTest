using GolanGiphyTest.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace GolanGiphyTest.Services
{
    public class GiphyService : IGiphyService
    {
        static ConcurrentDictionary<DateTime, GiphyResponse> TrendingGifsCache = new ConcurrentDictionary<DateTime, GiphyResponse>();
        static ConcurrentDictionary<string, GiphyResponse> TrendingGifsBySearchTermCache = new ConcurrentDictionary<string, GiphyResponse>();

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GiphyService(HttpClient httpclient, IConfiguration configuration)
        {
            _httpClient = httpclient;
            _configuration = configuration;
        }

        public async Task<GiphyResponse> GetTrendingGifs()
        {
            DateTime today = DateTime.Today;
            if (TrendingGifsCache.ContainsKey(today))
            {
                return TrendingGifsCache[today];
            }

            string giphyUrl = _configuration["Giphy:GiphyUrl"];
            string apiKey = _configuration["Giphy:ApiKey"];

            string urlToQuery = giphyUrl.Replace("[apiKey]", apiKey);

            var response = await _httpClient.GetAsync(urlToQuery);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            GiphyResponse giphyResponse = JsonSerializer.Deserialize<GiphyResponse>(responseStream);
            TrendingGifsCache[today] = giphyResponse;
            return giphyResponse;
        }


        public async Task<GiphyResponse> GetTrendingGifsBySearchTerm(string searchTerm)
        {
            if (TrendingGifsBySearchTermCache.ContainsKey(searchTerm))
            {
                return TrendingGifsBySearchTermCache[searchTerm];
            }

            string giphyUrl = _configuration["Giphy:GiphyUrlWithSearchTerm"];
            string apiKey = _configuration["Giphy:ApiKey"];

            string urlToQuery = giphyUrl.Replace("[apiKey]", apiKey).Replace("[searchTerm]", searchTerm);

            var response = await _httpClient.GetAsync(urlToQuery);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStringAsync();
            GiphyResponse giphyResponse = JsonSerializer.Deserialize<GiphyResponse>(responseStream);
            TrendingGifsBySearchTermCache[searchTerm] = giphyResponse;
            return giphyResponse;
        }

    }
}
