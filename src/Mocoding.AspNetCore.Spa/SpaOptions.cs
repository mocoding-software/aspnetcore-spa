namespace Mocoding.AspNetCore.Spa
{
    /// <summary>
    /// SPA middleware options.
    /// </summary>
    public class SpaOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpaOptions"/> class.
        /// </summary>
        public SpaOptions()
        {
            IncludeScripts = true;
            BabelPolyfill = null;
            SsrScriptPath = "./wwwroot_node/server.js";
        }

        /// <summary>
        /// Gets or sets a value indicating whether include scripts.
        /// </summary>
        /// <value>
        ///   <c>true</c> if scripts are included ; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeScripts { get; set; }

        /// <summary>
        /// Gets or sets the babel polyfill.
        /// </summary>
        /// <value>
        /// The babel polyfill.
        /// </value>
        public string BabelPolyfill { get; set; }

        /// <summary>
        /// Gets or sets the SSR script path.
        /// </summary>
        /// <value>
        /// The SSR script path.
        /// </value>
        public string SsrScriptPath { get; set; }
    }
}
