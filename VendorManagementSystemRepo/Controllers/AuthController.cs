using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VendorManagementSystemRepo.Models;
using VendorManagementSystemRepo.Data;
using Microsoft.AspNetCore.Cors;

namespace VendorManagementSystemRepo.Controllers
{
    [EnableCors("AllowAngular")]
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly VendorManagementSystemDb _context;

        public AuthController(IConfiguration config, VendorManagementSystemDb context)
        {
            _config = config;
            _context = context;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] Users user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest("User Already Exists");
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(new
            {
                message="User Registerd Succesfully",
            });
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password && u.IsActive);

            if (user == null)
            {
                return Unauthorized("Invalid Credentials");
            }
            var token = GenerateToken(user);
            return Ok(new { token, role = user.Role });

        }
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok("Password reset link has been sent(mocked)");
        }
        [HttpPost("reset-password")]
        public IActionResult ResetPassword( string email,string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            user.Password = newPassword;
            _context.SaveChanges();
            return Ok("Password Reset Successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult getAdmin()
        {
            return Ok("welcome");
        }
        [HttpPost("admin-only")]
        public IActionResult AdminOnly(string email, string password)
        {
            var userisAdmin = _context.Users.FirstOrDefault(u => u.Email ==email && u.Password == password);
            if (userisAdmin != null)
            {
                if (userisAdmin.Role=="Admin")
                {
                    var token = GenerateToken(userisAdmin);
                    return Ok(new { token, Role = userisAdmin.Role });
                }
            }
            return NotFound("Invalid Credentials");
           
        }
        private string GenerateToken(Users user)
        {
            var claims = new List<Claim>
            {
                   new Claim(ClaimTypes.Email, user.Email),
                   new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                   new Claim(ClaimTypes.Role, "Admin"),
                   new Claim(ClaimTypes.Role, "Legal"),
                   new Claim(ClaimTypes.Role, "Finance"),
                   new Claim(ClaimTypes.Role, "Procurement"),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"]));
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
