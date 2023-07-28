using GolanGiphyTest.Models;
using GolanGiphyTest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GolanGiphyTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGiphyService _giphyService;

        public HomeController(IGiphyService giphyService)
        {
            _giphyService = giphyService;
        }

        public async Task<IActionResult> Index()
        {
            GiphyResponse giphyRes = await _giphyService.GetTrendingGifsBySearchTerm("golan");
            return View(giphyRes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}