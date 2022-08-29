using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using APISchool.Models;

//Se agregan para uso de JWT
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace APISchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretkey;
        public readonly DB_API_SCHOOLContext _dbcontext;

        public AuthenticationController(IConfiguration config,DB_API_SCHOOLContext dbcontext)
        {
            secretkey = config.GetSection("settings").GetSection("secretkey").ToString();
            _dbcontext = dbcontext;
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult Validate([FromBody] User request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Datos no encontrados" });
            }

            User ReqUser = new User();
            ReqUser = _dbcontext.Users.Where(a => a.Email == request.Email).FirstOrDefault();

            if (ReqUser is null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Datos no encontrados" });
            }



            if (request.Email == ReqUser.Email && request.Password == ReqUser.Password)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretkey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Email));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });

            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" , mensaje = "Datos no encontrados" });
            }
        }
    }
}
