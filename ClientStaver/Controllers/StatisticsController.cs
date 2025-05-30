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

        // GET: URL:/Statistics/Statistics
        public async Task<IActionResult> Server()
        {
            //var token = HttpContext.Session.GetString("AccessToken");

            var computerStats = await _apiService.GetComputerStatsAsync();
            var bitcoinStats = await _apiService.GetBitcoinStatsAsync();

            var data = new ServerStatsView
            {
                ComputerStats = computerStats,
                BitcoinStats = bitcoinStats
            };

            return View(data);
        }

        // GET: URL:/Statistics/Home
        public async Task<IActionResult> Home()
        {
            var data = await _apiService.GetTempHumSensorsAsync();

            ViewData["sensorData"] = data;

            return View();
        }

        public async Task<IActionResult> DetailsHome(string room)
        {

            var data = await _apiService.GetTempHumSensorsAsync(room);
            if (data == null)
            {
                return NotFound();
            }

            ViewData["Title"] = room;

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _apiService.GetTempHumSensorAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        public async Task<IActionResult> CreateSensor(TempHumSensor sensor)
        {
            var response = await _apiService.PostTempHumSensorAsync(sensor);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Home");
            }
            else
            {
                return NotFound();
            }
        }


        public async Task<IActionResult> EditSensor(TempHumSensor sensor)
        {
            var response = await _apiService.PutTempHumSensorAsync(sensor);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Home");
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiService.DeleteTempHumSensorAsync(id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Home");
            }
            else
            {
                return NotFound();
            }
        }



    }
}
