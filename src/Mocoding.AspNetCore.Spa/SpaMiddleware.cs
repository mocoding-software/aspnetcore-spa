using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mocoding.AspNetCore.Spa.Abstractions;

namespace Mocoding.AspNetCore.Spa
{
    public class SpaMiddleware
    {
        private readonly RequestDelegate _next;

        public SpaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, IServerRenderer renderer)
        {
            Task Render() =>
                renderer.RenderHtmlPage(context)
                    .ContinueWith(task => Output(context, task.Result));

            if (context.Request.Path.Value == "/")
                return Render();

            var acceptHeaders = context.Request.Headers["Accept"].FirstOrDefault();
            var hasRequiredHeader = acceptHeaders != null && acceptHeaders.Contains("text/html");
            return hasRequiredHeader ? Render() : _next(context);

            // Call the next delegate/middleware in the pipeline
        }

        private Task Output(HttpContext context, string output)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "text/html; charset=utf-8";
            return context.Response.WriteAsync(output);
        }
    }
}
