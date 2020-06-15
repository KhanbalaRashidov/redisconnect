using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using KeyValue;
using DotNetEnv;

namespace KeyValue
{
    public class Program
    {
        private string Url;
        public static void Main(string[] args)
        {
            // Load Environment variables
            Env.Load();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls($"http://{Env.GetString("ip")}:{Env.GetInt("port")}");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
