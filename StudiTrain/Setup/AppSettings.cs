using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace StudiTrain.Setup
{
    public interface IAppSettings
    {
        public ControllerSetup ControllerSetup { get; set; }
        public JwtSetup JwtSetup { get; set; }
    }
    public class AppSettings : IAppSettings
    {
        public ControllerSetup ControllerSetup { get; set; }

        public JwtSetup JwtSetup { get; set; }

        public AppSettings(IConfiguration configuration)
        {
            ControllerSetup = new ControllerSetup(configuration);
            JwtSetup = new JwtSetup(configuration);
        }

        public AppSettings(ControllerSetup controllerSetup, JwtSetup jwtSetup)
        {
            ControllerSetup = controllerSetup;
            JwtSetup = jwtSetup;
        }
    }
}
