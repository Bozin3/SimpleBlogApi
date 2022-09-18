using BlogApi.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApi.Utils
{
    public static class TokenBuilder
    {
        public static string CreateToken(User user, string tokenKey)
        {
            Claim[] claims ={
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDesc = new SecurityTokenDescriptor();
            tokenDesc.Subject = new ClaimsIdentity(claims);
            tokenDesc.SigningCredentials = creds;
            tokenDesc.Expires = DateTime.Now.AddDays(1);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);

            return tokenHandler.WriteToken(token);
        }
    }
}
