using DiyorMarket.Domain.Entities;
using DiyorMarket.Infrastructure.Persistence;
using DiyorMarket.LoginModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiyorMarket.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DiyorMarketDbContext _context;
        public AuthenticationController(DiyorMarketDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginRequest request)
        {
            var user = Authenticate(request.Login, request.Password);

            if (user is null)
            {
                return Unauthorized();
            }

            if (!FindUser(request.Login, request.Password))
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("anvarSekretKalitSozMalades"));
            var signingCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Phone));
            claimsForToken.Add(new Claim("name", user.Name));

            var jwtSecurityToken = new JwtSecurityToken(
                "anvar-api",
                "anvar-mobile",
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(30),
                signingCredentials);

            var token = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(token);
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterRequest request)
        {
            var existingUser = FindUser(request.Login);
            if (existingUser != null)
            {
                return Conflict("User with this login already exists.");
            }

            var user = new User
            {
                Login = request.Login,
                Password = request.Password,
                Name = request.FullName,
                Phone = request.Phone
            };

            _context.Users.Add(user);

            _context.SaveChanges();

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("anvarSekretKalitSozMalades"));
            var signingCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Phone));
            claimsForToken.Add(new Claim("name", user.Name));

            var jwtSecurityToken = new JwtSecurityToken(
                "anvar-api",
                "anvar-mobile",
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(30),
                signingCredentials);

            var token = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(token);
        }

        private bool FindUser(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == login);

            if (user is null || user.Password != password)
            {
                return false;
            }

            return true;
        }

        private User FindUser(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }

        static User Authenticate(string login, string password)
        {
            return new User()
            {
                Login = login,
                Password = password,
                Name = "Anvar",
                Phone = "124123"
            };
        }
    }
}