using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StudiTrain.Setup
{
    public interface IAppSetup
    {
        public IControllerSetup ControllerSetup { get; }
        public IJwtSetup JwtSetup { get; }
    }
    public class AppSetup : IAppSetup
    {
        public IControllerSetup ControllerSetup { get; }

        public IJwtSetup JwtSetup { get; }

        public AppSetup(IConfiguration configuration)
        {
            ControllerSetup = new ControllerSetup(configuration);
            JwtSetup = new JwtSetup(configuration);
        }
    }
}
