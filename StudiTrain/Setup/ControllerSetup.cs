using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace StudiTrain.Setup
{
    public class ControllerSetup
    {
        public string ConnectionString { get; set; }

        public ControllerSetup(IConfiguration conf)
        {
            ConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
                ? conf["ConnectionString"]
                : PostgresToConnectionString(Environment.GetEnvironmentVariable("DATABASE_URL"));
        }

        /// <summary>
        /// transform a connection string from libpq form to standard syntax
        /// </summary>
        /// <param name="libpqString"></param>
        /// <exception cref="ArgumentException">if the passed string is not of format "user:password@netloc:port/dbname"</exception>
        /// <returns></returns>
        private static string PostgresToConnectionString(string libpqString)
        {
            if (libpqString is null) throw new ArgumentException("libpq string not set");
            var leadIndex = libpqString.IndexOf("://", StringComparison.Ordinal);
            var paramDeclarations = new[] { "Username", "Password", "Host", "Port", "Database" };
            var paramValues = libpqString.Substring(leadIndex + 3).Split(new[] { ':', '@', '/' });

            if (paramValues.Length != 5) throw new ArgumentException("libpq string has not enough parameters");

            return string.Join(";", paramDeclarations.Zip(paramValues, (decl, val) => decl + "=" + val))
                 + ";SslMode=Require;Trust Server Certificate=True";

        }
    }
}
