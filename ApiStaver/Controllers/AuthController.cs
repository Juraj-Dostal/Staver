using ApiStaver.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiStaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly StatServerContext _context;

        public AuthController(StatServerContext context)
        {
            _context = context;
        }

        // Post: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> PostRegister(UserRequest user)
        {
            var roles = new List<Role>();

            foreach (var role in user.Roles)
            {
                roles.Add(await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == role));
            }

            if (roles.IsNullOrEmpty())
            {
                return BadRequest("Role does not exist.");
            }

            var newUser = new User
            {
                Username = user.Username,
                Hashword = user.Hashword,
                Roles = roles
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(user);
        }


        // POST: api/Auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (IsValidLogin(request))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("MySuperSecureSecretKey1234567890123456");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, "Admin") // Pridanie role
                }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = "ApiStaver",
                    Audience = "http://localhost:8070",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }

            return Unauthorized();
        }

        private bool IsValidHash(byte[] hashword) 
        {
            var genetator = SHA256.Create();
            var hash = genetator.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
            
            return hash.SequenceEqual(hashword);
        }

        private bool IsValidLogin(LoginRequest request)
        {

            var user = _context.User.FirstOrDefault(u => u.Username == request.Username);
            if (user == null)
            {
                return false;
            }

            return user.Hashword.SequenceEqual(request.Hashword);
        }
    }
}
