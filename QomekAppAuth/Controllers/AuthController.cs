using DotNetEnv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QomekAppBlog;
using QomekData;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QomekAppAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly QomekDbContext _db;

        public AuthController(QomekDbContext db)
        {
            _db = db;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hashStr = model.Password;

            // Create a SHA256 hash from string   
            using var sha256Hash = SHA256.Create();
            // Computing Hash - returns here byte array
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(hashStr));

            // now convert byte array to a string   
            var stringbuilder = new StringBuilder();
            foreach (var b in bytes)
            {
                stringbuilder.Append(b.ToString("x2"));
            }
            var password=stringbuilder.ToString();

            await _db.Users.AddAsync(new QomekData.Entities.User { UserName= model.Username, Password = password });

            await _db.SaveChangesAsync();

            return Ok(new { Message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hashStr = model.Password;

            // Create a SHA256 hash from string   
            using var sha256Hash = SHA256.Create();
            // Computing Hash - returns here byte array
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(hashStr));

            // now convert byte array to a string   
            var stringbuilder = new StringBuilder();
            foreach (var b in bytes)
            {
                stringbuilder.Append(b.ToString("x2"));
            }
            var password = stringbuilder.ToString();

            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == model.Username && u.Password == password);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            // Generate a JWT token or perform other authentication steps as needed
            // For example, you can use a library like IdentityServer4 or JWT tokens for authentication.
            //var secretKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            //    Env.GetString("SECRET_KEY")));

            //var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //List<Claim> claims = new();

            //claims.Add(new(JwtRegisteredClaimNames.UniqueName, model.Username));

            //var token = new JwtSecurityToken(
            //    Env.GetString("ISSUER_TOKEN"),
            //    Env.GetString("AUDIENCE_TOKEN"),
            //    claims,
            //    DateTime.UtcNow,
            //    DateTime.UtcNow.AddDays(3),
            //    signingCredentials);

            List<Claim> claims = new();

            claims.Add(new(JwtRegisteredClaimNames.UniqueName, model.Username));
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: Env.GetString("ISSUER_TOKEN"),
                    audience: Env.GetString("AUDIENCE_TOKEN"),
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(Env.GetInt("LIFE_TIME_TOKEN"))),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("SECRET_KEY"))), SecurityAlgorithms.HmacSha256));
            try
            {
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                return encodedJwt;
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"GenerateToken -> {ex.Message}");
                return null;
            }

            //return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
