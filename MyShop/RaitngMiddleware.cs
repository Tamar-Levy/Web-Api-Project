using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Services;

namespace MyShop
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RaitngMiddleware
    {
        private readonly RequestDelegate _next;

        public RaitngMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IRatingService ratingService)
        {
            Rating rating = new ();
            rating.Host = httpContext.Request.Host.ToString();
            rating.Method = httpContext.Request.Method.ToString();
            rating.Path = httpContext.Request.Path.ToString();
            rating.Referer = httpContext.Request.Headers.Referer.ToString();
            rating.UserAgent = httpContext.Request.Headers.UserAgent.ToString();
            rating.RecordDate = DateTime.Now;
            ratingService.AddRating(rating);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RaitngMiddlewareExtensions
    {
        public static IApplicationBuilder UseRaitngMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RaitngMiddleware>();
        }
    }
}
