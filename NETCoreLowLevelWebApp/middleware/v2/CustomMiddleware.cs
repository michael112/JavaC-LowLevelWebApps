using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NETCoreLowLevelWebApp
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string content = "{" + "\n" + "\"imie\": \"Michał\"," + "\n" + "\"nazwisko\": \"Choromański\"," + "\n" + "\"wydzial\": \"Elektryczny\"," + "\n" + "\"kierunek\": \"Informatyka stosowana\"," + "\n" + "\"poziom\": \"magisterskie\"" + "\n" + "}"; 
            byte[] contentAsByteArray = Encoding.UTF8.GetBytes(content);
            httpContext.Response.StatusCode = 200;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.Body.WriteAsync(contentAsByteArray, 0, contentAsByteArray.Length);
        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.MapWhen(context => ( context.Request.Path.Equals("/low/level") && context.Request.Method.Equals("GET") ), b => b.UseMiddleware<CustomMiddleware>());
        }
    }
}