using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyValue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using DotNetEnv;

namespace KeyValue
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            RedisConnect.config = new ConfigurationOptions
            {
                EndPoints =
                {
                    { Env.GetString("redis_ip"), Env.GetInt("redis_port") }
                },

                KeepAlive = 180,

                Password = Env.GetString("redis_pass"),
                ReconnectRetryPolicy = new ExponentialRetry(5000)
            };
            var redis = RedisConnect.Connect();
            services.AddScoped(x => RedisConnect.Redis);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();


            app.UseAuthorization();


            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}