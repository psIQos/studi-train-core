using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using StudiTrain.Models.Database;

namespace StudiTrain.Entities
{
    public class User
    {
        public User()
        {
            Authenticated = false;
        }

        public User(Users user)
        {
            Name = user.Username;
            DisplayName = user.DisplayName;
            Id = user.Id;
        }

        public User(string name, int id, IEnumerable<string> roles = null)
        {
            Name = name;
            Id = id;
            Roles = roles;
        }

        public string Name { get; }

        public int Id { get; }

        public bool Authenticated { get; } = true;

        public string DisplayName { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public SecurityToken JwToken { get; set; }

        public string GetJwToken()
        {
            return new JwtSecurityTokenHandler().WriteToken(JwToken);
        }
    }
}