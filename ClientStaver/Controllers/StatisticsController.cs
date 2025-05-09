using ClientStaver.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientStaver.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ApiService _apiService;
        
        public StatisticsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: URL:/Statistics/Index
        public async Task<IActionResult> Statistics()
        {
            var data = await _apiService.GetComputerStatsAsync();
            return View(data);
        }

        public IActionResult Welcome(string name, int numspace=1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numspace;
            return View();
        }

        
    }
}
