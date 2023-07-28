using GolanGiphyTest.Models;

namespace GolanGiphyTest.Services
{
    public interface IGiphyService
    {
        Task<GiphyResponse> GetTrendingGifs();
        Task<GiphyResponse> GetTrendingGifsBySearchTerm(string searchTerm);
    }
}
