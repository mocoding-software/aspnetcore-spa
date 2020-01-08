using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mocoding.AspNetCore.Spa.Abstractions;

namespace Mocoding.AspNetCore.Spa.Components
{
    internal class WebRootAssetsResolver : IAssetsResolver
    {
        private readonly string[] _assets;
        private readonly SpaOptions _options;

        public WebRootAssetsResolver(IWebHostEnvironment env, IOptions<SpaOptions> options)
        {
            var files = env.WebRootFileProvider.GetDirectoryContents("/");
            string[] EnumerateFiles(string extension) => files.Where(_ => _.Name.EndsWith(extension)).Select(file => $"/{file.Name}").ToArray();

            var assetsList = new List<string>();

            assetsList.AddRange(EnumerateFiles(".css"));
            if (options.Value.IncludeScripts)
                assetsList.AddRange(EnumerateFiles(".js"));

            _assets = assetsList.ToArray();
            _options = options.Value;
        }

        public string[] ResolveAssets(HttpContext context)
        {
            if (!_options.IncludeScripts || string.IsNullOrEmpty(_options.BabelPolyfill))
                return _assets;
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            var isInternetExplorer = userAgent.Contains("MSIE") || userAgent.Contains("Trident");

            return !isInternetExplorer ? _assets : new[] { _options.BabelPolyfill }.Union(_assets).ToArray();
        }
    }
}