using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StudiTrain.Models.Database;
using StudiTrain.Services;

namespace StudiTrain.Setup
{
    public class StudiTrainController : ControllerBase
    {
        protected PostgresContext DbConn { get; }

        protected IControllerServices Services { get; }

        public StudiTrainController(IAppSetup setup)
        {
            DbConn = new PostgresContext(setup.ControllerSetup.ConnectionString);
            Services = new ControllerServices(DbConn, setup.JwtSetup);
        }
    }
}
