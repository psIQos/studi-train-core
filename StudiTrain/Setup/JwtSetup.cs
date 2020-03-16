using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using StudiTrain.Entities;

namespace StudiTrain.Setup
{
    public class JwtSetup
    {
        public JwtSetup(IConfiguration conf)
        {
            string secret;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                secret = conf["JwtSecret"];
            }
            else
            {
                var jwtString = Environment.GetEnvironmentVariable("JWT_SECRET");
                secret = jwtString ?? throw new ArgumentException("JWT_SECRET environment variable is not set");
            }
            JwtSecret = Encoding.ASCII.GetBytes(secret);
        }

        public byte[] JwtSecret { get; }

        public SecurityToken CreateJwToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(type: ClaimTypes.Actor, user.Id.ToString()), 
            };

            if (user.Roles != null)
                foreach (var role in user.Roles)
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