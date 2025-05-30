using ClientStaver.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientStaver.Controllers
{
    public class AuthController : Controller
    {
        public readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LoginRequest(LoginRequest login)
        {
            var response = await _apiService.GetLoginTokenAsync(login);

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Unauthorization"] = "Invalid username or password.";
                return View();
            }
            return RedirectToAction("Home");
        }

    }
}
