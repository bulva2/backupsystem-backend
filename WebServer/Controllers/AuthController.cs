using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebServer.Logic;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;

        public AuthController(IConfiguration configuration, DatabaseContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Credentials credentials)
        {
            string username = credentials.Username;
            string password = credentials.Password;
            string passwordHash = HashPassword(password);

            Account? account = _context.Accounts.SingleOrDefault(a => a.Username == username);
            if (account == null || account.Password != passwordHash)
            {
                return Unauthorized("Invalid login or password!");
            }

            string issuer = _configuration["JWT:Issuer"]!;
            string key = _configuration["JWT:Key"]!;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Iss, issuer),
                    new Claim(JwtRegisteredClaimNames.Aud, issuer),
                    new Claim(JwtRegisteredClaimNames.Sub, account.ID.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, account.Username),
                    new Claim(ClaimTypes.Role, account.Role) //přidána informace (Claim) o uživatelské roli
                }),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken tokenObject = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(tokenObject);

            return Ok(new Token(tokenString));
        }

        private static string HashPassword(string password)
        {
            byte[] data = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }
	}
}
