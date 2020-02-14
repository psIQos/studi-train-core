using Microsoft.AspNetCore.Mvc;
using StudiTrain.Models.Database;
using StudiTrain.Services;

namespace StudiTrain.Setup
{
    public class StudiTrainController : ControllerBase
    {
        protected PostgresContext DbConn { get; }

        protected ControllerServices Services { get; }

        public StudiTrainController(IAppSettings settings)
        {
            DbConn = new PostgresContext(settings.ControllerSetup.ConnectionString);
            Services = new ControllerServices(DbConn, settings.JwtSetup);
        }
    }
}
