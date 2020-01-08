using Microsoft.AspNetCore.Http;

namespace Mocoding.AspNetCore.Spa.Abstractions
{
    /// <summary>
    /// Responsible to resolve assets to be included in pre-rendering.
    /// </summary>
    public interface IAssetsResolver
    {
        /// <summary>
        /// Return list of css and js files to be included in pre-rendering.
        /// </summary>
        /// <param name="context">Current http context.</param>
        /// <returns>Css and js files.</returns>
        public string[] ResolveAssets(HttpContext context);
    }
}
