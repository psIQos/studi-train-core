using System;
using System.Collections.Generic;
using System.Linq;
using StudiTrain.Entities;
using StudiTrain.Models;
using StudiTrain.Models.Database;
using StudiTrain.Setup;

namespace StudiTrain.Services
{
    public interface IUserService
    {
        public User Authenticate(AuthModel user);
        public User GetUser(string jwToken);
        public IEnumerable<string> GetPermissions(User user);
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
            return user == null ? new User() : new User(user.Username, user.Id, _setup.CreateJwToken(user.Username));
        }

        public User GetUser(string jwToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPermissions(User user)
        {
            throw new NotImplementedException();
        }
    }
}