using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace StudiTrain.Services
{
    public static class TokenServices
    {
        public static JwtSecurityToken GetToken(HttpContext context)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(context.GetTokenAsync("access_token").Result);
        }
    }
}
