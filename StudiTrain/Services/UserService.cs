using StudiTrain.Entities;
using StudiTrain.Models;
using StudiTrain.Models.Database;
using StudiTrain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace StudiTrain.Services
{
    public interface IUserService
    {
        public User Authenticate(AuthModel user);
        public IEnumerable<string> GetRoles(User user);
        public User GetUserFromToken(HttpContext context);
    }

    public class UserService : IUserService
    {
        private readonly PostgresContext _dbConn;
        private readonly JwtSetup _setup;

        public UserService(PostgresContext dbConn, JwtSetup setup)
        {
            _dbConn = dbConn;
            _setup = setup;
        }

        public User Authenticate(AuthModel userInput)
        {
            var user = _dbConn.Users.FirstOrDefault(u =>
                u.Username == userInput.Username && u.Passhash == userInput.Passhash);
            // username & password combination not correct
            if (user == null) return new User();
            // TODO: check roles
            var authUser = new User(user);
            authUser.JwToken = _setup.CreateJwToken(authUser);
            return authUser;
        }

        public IEnumerable<string> GetRoles(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserFromToken(HttpContext context)
        {
            var user = _dbConn.Users.FirstOrDefault(u => u.Id == int.Parse(TokenServices.GetToken(context).Actor));
            return user == null ? new User() : new User(user);
        }
    }
}