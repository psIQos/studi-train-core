using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace StudiTrain.Entities
{
    public class User
    {
        public User()
        {
        }

        public User(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public User(string name, int id, SecurityToken jwToken, IEnumerable<string> roles = null,
            bool authenticated = true)
        {
            Name = name;
            Id = id;
            Roles = roles;
            JwToken = jwToken;
            Authenticated = authenticated;
        }

        public string Name { get; }

        public int Id { get; }

        public bool Authenticated { get; }

        public IEnumerable<string> Roles { get; set; }

        public SecurityToken JwToken { get; set; }

        public string GetJwToken()
        {
            return new JwtSecurityTokenHandler().WriteToken(JwToken);
        }
    }
}