﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace StudiTrain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("PORT");
            if (port == null)
            {
                return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
            }
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:" + port);
        }
    }
}