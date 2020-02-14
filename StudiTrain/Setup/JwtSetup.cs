using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace StudiTrain.Setup
{
    public class JwtSetup
    {
        public JwtSetup(IConfiguration conf)
        {
            JwtSecret = Encoding.ASCII.GetBytes(
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
                    ? conf["JwtSecret"]
                    : Environment.GetEnvironmentVariable("JWT_SECRET")
            );
        }

        public byte[] JwtSecret { get; }

        public SecurityToken CreateJwToken(string name, IEnumerable<string> roles = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name)
            };

            if (roles != null)
                foreach (var role in roles)
                    claims.Append(new Claim(ClaimTypes.Role, role));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(JwtSecret), SecurityAlgorithms.HmacSha256Signature
                )
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}