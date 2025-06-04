using ClientStaver.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

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

        public IActionResult Signup()
        {
            ViewBag.Roles = Enum.GetValues(typeof(RoleEnum))
                .Cast<RoleEnum>()
                .Select(r => new SelectListItem
            {
                Value = r.ToString(),
                Text = r.ToString()
            }).ToList();
            return View();
        }

        public async Task<IActionResult> SignupRequest(Signup signup)
        {
            if (signup == null)
            {
                TempData["Signup"] = "Signup data is required.";
                return RedirectToAction("Signup");
            }

            var roles = new List<RoleEnum> { signup.Role };


            var signupRequest = new UserRequest
            {
                Username = signup.Username,
                Hashword = GetHash(signup.Password),
                Roles = roles
            };

            var response = await _apiService.CreateUserAsync(signupRequest);
            if (!response.IsSuccessStatusCode)
            {
                TempData["DeniedSignup"] = "Username already exists or invalid data.";
                return RedirectToAction("Signup");
            }
            TempData["Successlogin"] = "Signup successful. You can now log in.";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> LoginRequest(Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                TempData["DeniedLogin"] = "Username and password are required.";
                return RedirectToAction("Login");
            }
            
            var loginRequest = new LoginRequest
            {
                Username = login.Username,
                Hashword = GetHash(login.Password)
            };

            var response = await _apiService.GetLoginTokenAsync(loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                TempData["DeniedLogin"] = "Invalid username or password.";
                return RedirectToAction("Login");
            }
            TempData["Successlogin"] = "Login successful.";
            return RedirectToAction("Index", "Home" );
        }

        public async Task<IActionResult> Logout()
        {
            Token.Value = null; // Clear the token
            TempData["SuccessLogin"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }

        private byte[] GetHash(string password)
        {
            using (var genetator = SHA256.Create())
            {
                return genetator.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
