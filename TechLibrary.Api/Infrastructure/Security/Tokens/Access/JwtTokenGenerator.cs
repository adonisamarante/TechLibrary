using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infrastructure.Security.Tokens.Access
{
    public class JwtTokenGenerator
    {
        public string Generate(User user)
        {
            var claims = new List<Claim>()
            {
                // the first parameter could just be "UserId", but JwtRegisteredClaimNames is a class that has some predefined claims
                // so it's safer to use it and not make explicit that it's a user id
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            // describing the token, setting the token's configs
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(60),
                Subject = new ClaimsIdentity(claims),
                // setting the cryptography, first parameter is the security key and second is the algorithm used to do the cryptography
                SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            // generating the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        private static SymmetricSecurityKey SecurityKey()
        {
            // hardcoding the key, not a good practice, just for the sake of the example
            // the key has to have a minimum of 32 characters
            var signInKey = "64aOpmMA7xxmNkGXT0x07ebqQdnPtzlG";
            
            // in order for the key to be used in the cryptography, it has to be converted to a byte array
            var symmetricKey =  Encoding.UTF8.GetBytes(signInKey);

            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}
