using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mocoding.AspNetCore.Spa.Abstractions;

namespace Mocoding.AspNetCore.Spa
{
    /// <summary>
    /// Prerendering middleware for SPA.
    /// </summary>
    public class SpaMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public SpaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">Current http context.</param>
        /// <param name="renderer">Server side pre-renderer.</param>
        /// <returns>Task.</returns>
        public Task InvokeAsync(HttpContext context, IServerRenderer renderer)
        {
            Task Render() =>
                renderer.RenderHtmlPageAsync(context)
                    .ContinueWith(task => Output(context, task.Result));

            if (context.Request.Path.Value == "/")
                return Render();

            var acceptHeaders = context.Request.Headers["Accept"].FirstOrDefault();
            var hasRequiredHeader = acceptHeaders != null && acceptHeaders.Contains("text/html");
            return hasRequiredHeader ? Render() : _next(context);
        }

        private static Task Output(HttpContext context, string output)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "text/html; charset=utf-8";
            return context.Response.WriteAsync(output);
        }
    }
}
