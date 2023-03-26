using JobPortal.Models;
using JobPortal.WebModels.AuthenticationWebModels;
using JobPortal.WebModels.JobWebModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthenticateController : ControllerBase
    {
        private readonly SQLServerDBContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticateController(SQLServerDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<AuthResponseWebModel>> LoginUser(AuthLoginWebModel user)
        {
            try
            {
                if (user.Username == "joydip" && user.Password == "joydip123")
                {
                    var issuer = _configuration.GetValue<string>("JWT:Issuer");
                    var audience = _configuration.GetValue<string>("JWT:Audience");
                    var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT:Key"));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                            new Claim(JwtRegisteredClaimNames.Email, user.Username),
                            new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString())
                         }),
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    var stringToken = tokenHandler.WriteToken(token);

                    return Ok(new AuthResponseWebModel
                    {
                        JwtToken = stringToken,
                        Message = "Authentication Successful."
                    });
                }

                return StatusCode((int)HttpStatusCode.NotFound, new AuthResponseWebModel
                {
                    JwtToken = null,
                    Message = "Username or password is incorrect."
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
