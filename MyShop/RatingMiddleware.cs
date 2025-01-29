using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Services;

namespace MyShop
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;

        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IRatingService ratingService)
        {
            Rating rating = new();
            rating.Host = httpContext.Request.Host.ToString();
            rating.Method = httpContext.Request.Method.ToString();
            rating.Path = httpContext.Request.Path.ToString();
            rating.Referer = httpContext.Request.Headers.Referer.ToString();
            rating.UserAgent = httpContext.Request.Headers.UserAgent.ToString();
            rating.RecordDate = DateTime.Now;
            await ratingService.AddRating(rating);
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }

}
