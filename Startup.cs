using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HttpFile
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
