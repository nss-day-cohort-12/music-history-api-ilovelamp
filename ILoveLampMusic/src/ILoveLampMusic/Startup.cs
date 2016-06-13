using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILoveLampMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace ILoveLampMusic
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=MusicHistory.AspNetCore.NewDb;Trusted_Connection=True;";
            services.AddDbContext<MusicHistoryContext>(options => options.UseSqlServer(connection));
            // Add framework services.
            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowNewDevelopmentEnvironment",
            //Tells VS to accept other browsers and local hosts.
            builder => builder
                      .AllowAnyOrigin() //allows from anything(including virtual box)
                      .AllowAnyMethod()
                      .AllowAnyHeader().WithMethods("DELETE, PUT, POST, GET, OPTIONS"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
           app.UseCors(builder =>
           builder.WithOrigins("http://example.com"));

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
