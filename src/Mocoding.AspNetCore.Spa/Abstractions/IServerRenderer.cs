using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Mocoding.AspNetCore.Spa.Abstractions
{
    /// <summary>
    /// Responsible to render html page on server.
    /// </summary>
    public interface IServerRenderer
    {
        /// <summary>
        /// Renders the HTML page.
        /// </summary>
        /// <param name="context">Current http context.</param>
        /// <returns>HTML.</returns>
        public Task<string> RenderHtmlPageAsync(HttpContext context);
    }
}
