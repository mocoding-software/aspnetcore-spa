using System.Text.Json;
using System.Threading.Tasks;
using Jering.Javascript.NodeJS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Mocoding.AspNetCore.Spa.Abstractions;

namespace Mocoding.AspNetCore.Spa.Components
{
    public class JeringServerRenderer : IServerRenderer
    {
        private readonly INodeJSService _jsService;
        private readonly IOptions<SpaOptions> _options;
        private readonly ITelemetryProvider _telemetryProvider;
        private readonly IAssetsResolver _assesResolver;

        public JeringServerRenderer(INodeJSService jsService, IOptions<SpaOptions> options, ITelemetryProvider telemetryProvider, IAssetsResolver assesResolver)
        {
            _jsService = jsService;
            _options = options;
            _telemetryProvider = telemetryProvider;
            _assesResolver = assesResolver;
        }
        public Task<string> RenderHtmlPage(HttpContext context)
        {
            var requestFeature = context.Features.Get<IHttpRequestFeature>();
            var unencodedPathAndQuery = requestFeature.RawTarget;

            var request = context.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";

            var props = new RenderFuncProps()
            {
                requestUrl = unencodedPathAndQuery,
                baseUrl = baseUrl,
                assets = _assesResolver.ResolveAssets(context),
                inlineScripts = new[]{new InlineScript()
                {
                    position = "top",
                    script = _telemetryProvider.GetCodeSnippet()
                }}
            };

            var objectArgs = new object[]
            {
                props
            };

            return _jsService.InvokeFromFileAsync<JsonElement>(_options.Value.SsrScriptPath, "default", objectArgs)
                .ContinueWith(task => task.Result.GetProperty("html").GetString());
        }
    }
}
