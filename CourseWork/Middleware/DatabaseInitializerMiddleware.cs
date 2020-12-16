using System.Threading.Tasks;
using CourseWork.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CourseWork.Middleware
{
    public sealed class DatabaseInitializerMiddleware
    {
        private readonly RequestDelegate _next;

        public DatabaseInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, RestarauntDbContext db)
        {
            DatabaseInitializer.Initialize(db);
            return _next(httpContext);
        }
    }

    public static class DatabaseInitializerMiddlewareExtensions
    {
        public static IApplicationBuilder UseDatabaseInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DatabaseInitializerMiddleware>();
        }
    }
}