using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mocoding.AspNetCore.Spa.Abstractions;

namespace Mocoding.AspNetCore.Spa.Components
{
    public class WebRootAssetsResolver : IAssetsResolver
    {
        private string[] _assets;
        private SpaOptions _options;

        public WebRootAssetsResolver(IWebHostEnvironment env, IOptions<SpaOptions> options)
        {
            string[] EnumerateFiles(string pattern) => Directory.EnumerateFiles(env.WebRootPath, pattern).Select(file => $"/{Path.GetFileName(file)}").ToArray();

            var assetsList = new List<string>();

            assetsList.AddRange(EnumerateFiles("*.css"));
            if (options.Value.IncludeScripts)
                assetsList.AddRange(EnumerateFiles("*.js"));

            _assets = assetsList.ToArray();
            _options = options.Value;
        }

        public string[] ResolveAssets(HttpContext context)
        {
            if (!_options.IncludeScripts || string.IsNullOrEmpty(_options.BabelPolyfill))
                return _assets;
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            var isInternetExplorer = userAgent.Contains("MSIE") || userAgent.Contains("Trident");

            if (!isInternetExplorer) return _assets;
            return new[] {_options.BabelPolyfill}.Union(_assets).ToArray();

            
        }
    }
}

