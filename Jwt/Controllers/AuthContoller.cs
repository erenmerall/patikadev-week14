using Microsoft.AspNetCore.Mvc;
using Jwt.Data;
using Jwt.Models;
using Jwt.Services;

namespace Jwt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return BadRequest("Email zaten kayıtlı!");

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Kullanıcı başarıyla eklendi.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = _context.Users.FirstOrDefault(u => 
                u.Email == login.Email && u.Password == login.Password);

            if (user == null) return Unauthorized("Geçersiz email veya şifre!");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
