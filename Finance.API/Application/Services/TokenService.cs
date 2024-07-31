using Finance.API.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Finance.API.Application.Services
{
    public class TokenService : ITokenService
    {
        public string SecurityKey { get; private set; } = "#@#@!323@$#@$^%$&^&5DSfds!!$#$AC!!XZ!!C#$#@FDSADE#";
        public string GenerateToken(User user)
        {

            var encodingKey = Encoding.ASCII.GetBytes(SecurityKey);

            var claims = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            ]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodingKey), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandle = new JwtSecurityTokenHandler();
            var token = tokenHandle.CreateToken(tokenDescriptor);

            return tokenHandle.WriteToken(token);
        }
    }
}
