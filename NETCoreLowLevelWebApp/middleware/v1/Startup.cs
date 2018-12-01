using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NETCoreLowLevelWebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapWhen(context => ( context.Request.Path.Equals("/low/level") && context.Request.Method.Equals("GET") ), a =>
            {
                a.Run(async httpContext =>
                {
                    string content = "{" + "\n" + "\"imie\": \"Michał\"," + "\n" + "\"nazwisko\": \"Choromański\"," + "\n" + "\"wydzial\": \"Elektryczny\"," + "\n" + "\"kierunek\": \"Informatyka stosowana\"," + "\n" + "\"poziom\": \"magisterskie\"" + "\n" + "}"; 
                    byte[] contentAsByteArray = Encoding.UTF8.GetBytes(content);
                    httpContext.Response.StatusCode = 200;
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.Body.WriteAsync(contentAsByteArray, 0, contentAsByteArray.Length);
                });
            });
        }
    }
}
