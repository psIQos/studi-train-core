﻿using StudiTrain.Models.Database;
using StudiTrain.Setup;

namespace StudiTrain.Services
{
    public class ControllerServices
    {
        public IUserService UserService { get; }

        public ControllerServices(PostgresContext dbContext, JwtSetup setup)
        {
            UserService = new UserService(dbContext, setup);
        }
    }
}
