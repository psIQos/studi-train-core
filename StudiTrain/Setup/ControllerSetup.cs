using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace StudiTrain.Setup
{
    public interface IControllerSetup
    {
        public string ConnectionString { get; }
    }

    public class ControllerSetup : IControllerSetup
    {
        public ControllerSetup(IConfiguration conf)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                ConnectionString = conf["ConnectionString"];
            }
            else
            {
                var dbString = Environment.GetEnvironmentVariable("DATABASE_URL");
                if (dbString == null) throw new ArgumentException("DATABASE_URL environment variable is not set");
                ConnectionString = PostgresToConnectionString(dbString);
            }
        }

        public string ConnectionString { get; }

        /// <summary>
        ///     transform a connection string from libpq form to standard syntax
        /// </summary>
        /// <param name="libpqString"></param>
        /// <exception cref="ArgumentException">if the passed string is not of format "user:password@netloc:port/dbname"</exception>
        /// <returns></returns>
        private static string PostgresToConnectionString(string libpqString)
        {
            if (libpqString is null) throw new ArgumentException("libpq string cannot be null");
            var leadIndex = libpqString.IndexOf("://", StringComparison.Ordinal);
            var paramDeclarations = new[] {"Username", "Password", "Host", "Port", "Database"};
            var paramValues = libpqString.Substring(leadIndex + 3).Split(':', '@', '/');

            if (paramValues.Length != 5) throw new ArgumentException("libpq string has not enough parameters");

            return string.Join(";", paramDeclarations.Zip(paramValues, (decl, val) => decl + "=" + val))
                   + ";SslMode=Require;Trust Server Certificate=True";
        }
    }
}