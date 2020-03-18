using StudiTrain.Models.Database;
using StudiTrain.Setup;

namespace StudiTrain.Services
{
    public interface IControllerServices
    {
        public IUserService UserService { get; }
    }

    public class ControllerServices : IControllerServices
    {
        public IUserService UserService { get; }

        public ControllerServices(PostgresContext dbContext, IJwtSetup setup)
        {
            UserService = new UserService(dbContext, setup);
        }
    }
}
