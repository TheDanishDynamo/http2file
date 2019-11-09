using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Http2File
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            // Configuration from appsettings.json has already been loaded by
            // CreateDefaultBuilder on Host in Program.cs. Use DI to load
            // the configuration into the Configuration property.
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConsoleLifetimeOptions>(opts 
                => opts.SuppressStatusMessages = Configuration["SuppressStatusMessages"] != null);
            services.AddHealthChecks();
            services.AddControllers();
            services.AddSingleton<IConfiguration>(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.Builder()
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Add endpoints to the request pipeline
            // https://aregcode.com/blog/2019/dotnetcore-understanding-aspnet-endpoint-routing/
            app.UseEndpoints(endpoints =>
            {
                // e.g. POST http://localhost:8000
                endpoints.MapControllerRoute(
                name: "root",
                pattern: "/",
                new { controller = "Root", action = "Index" });

                // e.g. POST http://localhost:8000/http2file
                endpoints.MapControllerRoute(
                name: "http2file",
                pattern: "/http2file",
                new { controller = "Root", action = "Http2File" });

                // e.g. GET http://localhost:8000/health
                endpoints.MapHealthChecks("/health");
            });
            
        }
    }
}
