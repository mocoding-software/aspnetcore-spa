// ReSharper disable InconsistentNaming
#pragma warning disable SA1629 // Documentation text should end with a period

namespace Mocoding.AspNetCore.Spa.Components
{
    /// <summary>
    /// Prerendering Function Properties
    /// </summary>
    internal class RenderFuncProps
    {
        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        /// <value>
        /// The request URL.
        /// </value>
        /// <example>
        /// /some/url-with?params=0
        /// </example>
        public string requestUrl { get; set; }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>
        /// The base URL.
        /// </value>
        /// <example>
        /// http://localhost:5000
        /// </example>
        public string baseUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of css and js urls to include.
        /// </summary>
        /// <value>
        /// Array of css and js urls.
        /// </value>
        public string[] assets { get; set; }

        /// <summary>
        /// Gets or sets the inline scripts.
        /// </summary>
        /// <value>
        /// The inline scripts.
        /// </value>
        public InlineScript[] inlineScripts { get; set; }
    }

    /// <summary>
    /// Include inline script
    /// </summary>
    internal class InlineScript
    {
        /// <summary>
        /// Gets or sets inline script position (top or bottom).
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string position { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>
        /// The script.
        /// </value>
        public string script { get; set; }
    }
}

#pragma warning restore SA1629 // Documentation text should end with a period
