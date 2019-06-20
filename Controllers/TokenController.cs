using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Time_Tracking.Controllers
{
    public class TokenController : Controller
    {

        private const string SECRET_KEY = "abcdefabcdefabcdef";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenController.SECRET_KEY));

        [HttpGet]
        [Route("api/Token/{username}/{pword}")]

        public IActionResult Get (string username, string pword)
        {
            if (username == pword)
            {
                return new ObjectResult(GenerateToken(username));
            }
            else
                return BadRequest();
        }

        private string GenerateToken(string username)
        {
            try
            {
                //Generate Token

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.NameId, username),
                new Claim(JwtRegisteredClaimNames.Email, "michael@gmail.com"),
                new Claim("my key", "my value"),
            };

                //Create Credentials
                var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySuperSecretKey")), SecurityAlgorithms.HmacSha256);

                //Generate Token
                var token = new JwtSecurityToken(
                    issuer: "fasetto.word",
                    audience: "fasetto.word",
                    claims: claims,
                    expires: DateTime.Now.AddMonths(3),
                    signingCredentials: credentials
                    );

                return  new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
        }

        
    }
}
