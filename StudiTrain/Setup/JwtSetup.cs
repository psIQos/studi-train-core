using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace StudiTrain.Setup
{
    public class JwtSetup
    {
        public byte[] JwtSecret { get; }

        public JwtSetup(IConfiguration conf)
        {
            JwtSecret = Encoding.ASCII.GetBytes(
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
                ? conf["JwtSecret"]
                : Environment.GetEnvironmentVariable("JWT_SECRET")
            );
        }
    }   
}
    