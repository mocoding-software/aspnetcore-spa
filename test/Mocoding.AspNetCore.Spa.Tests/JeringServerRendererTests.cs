using System.Threading.Tasks;

using System;
using Microsoft.AspNetCore.Http;
using Mocoding.AspNetCore.Spa;
using Xunit;
using NSubstitute;
using Mocoding.AspNetCore.Spa.Abstractions;
using System.IO;
using Mocoding.AspNetCore.Spa.Components;
using Jering.Javascript.NodeJS;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.Features;
using System.Text.Json;

namespace Mocoding.AspNetCore.Spa.Tests
{
    public class JeringServerRendererTests
    {
        [Fact]
        public async void RendererDefaultTest()
        {
            // arrange            
            var nodeJsService = Substitute.For<INodeJSService>();
            var opts = new SpaOptions();
            var options = Substitute.For<IOptions<SpaOptions>>();
            var telemetryProvider = Substitute.For<ITelemetryProvider>();
            var assetsResolver = Substitute.For<IAssetsResolver>();
            var requestFeature = Substitute.For<IHttpRequestFeature>();
            var context = new DefaultHttpContext();

            // configure dependencies
            assetsResolver.ResolveAssets(context).Returns(new string[] { "asset" });
            telemetryProvider.GetCodeSnippet().Returns("snippet");
            options.Value.Returns(opts);

            // configure path
            requestFeature.RawTarget.Returns("/?params");

            context.Features.Set(requestFeature);
            context.Request.Scheme = "http";
            context.Request.Host = new HostString("localhost:3000");

            var renderer = new JeringServerRenderer(nodeJsService, options, telemetryProvider, assetsResolver);

            const string expected = "<html></html>";
            var json = JsonSerializer.Serialize(new { html = expected });
            var doc = JsonDocument.Parse(json);
            nodeJsService.InvokeFromFileAsync<JsonElement>(opts.SsrScriptPath, "default", Arg.Any<object[]>())
                .Returns(Task.FromResult(doc.RootElement));

            // act
            var actual = await renderer.RenderHtmlPage(context);

            // assert                    
            Assert.Equal(expected, actual);
        }
    }
}