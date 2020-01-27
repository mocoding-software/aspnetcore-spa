using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Mocoding.AspNetCore.Spa.Components;
using NSubstitute;
using Xunit;

namespace Mocoding.AspNetCore.Spa.Tests
{
    public class ResolveAssetsTests
    {

        [Fact]
        public void DefaultAssetsTest()
        {
            var (path, hostEnv) = ConfigureWebHostEnvironment();
            var (opts, options) = SetupOptions();

            var assetsResolver = new WebRootAssetsResolver(hostEnv, options);
            var context = new DefaultHttpContext();

            var assets = assetsResolver.ResolveAssets(context);
                
            Assert.Empty(assets);

            Directory.Delete(path, true);
        }

        [Fact]
        public void FullAssetsTest()
        {
            var (path, hostEnv) = ConfigureWebHostEnvironment();

            File.Create(Path.Join(path, "index.js")).Close();
            File.Create(Path.Join(path, "index.css")).Close();

            var (opts, options) = SetupOptions();

            var assetsResolver = new WebRootAssetsResolver(hostEnv, options);
            var context = new DefaultHttpContext();

            var assets = assetsResolver.ResolveAssets(context);

            Assert.Contains("/index.js", assets);
            Assert.Contains("/index.css", assets);

            Directory.Delete(path, true);
        }

        [Fact]
        public void BabelPolyfillTest()
        {
            var (path, hostEnv) = ConfigureWebHostEnvironment();

            var (opts, options) = SetupOptions();
            opts.BabelPolyfill = "/polyfill.js";

            var assetsResolver = new WebRootAssetsResolver(hostEnv, options);
            var context = new DefaultHttpContext();
            context.Request.Headers.Add("User-Agent", "MSIE");

            var assets = assetsResolver.ResolveAssets(context);

            Assert.Contains(opts.BabelPolyfill, assets);

            Directory.Delete(path, true);
        }

        private static (SpaOptions opts, IOptions<SpaOptions> options) SetupOptions()
        {
            var opts = new SpaOptions();
            var options = Substitute.For<IOptions<SpaOptions>>();
            options.Value.Returns(opts);
            return (opts, options);
        }

        private static (string path, IWebHostEnvironment hostEnv) ConfigureWebHostEnvironment()
        {
            var path = Path.Join(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            using var provider = new PhysicalFileProvider(path);
            var hostEnv = Substitute.For<IWebHostEnvironment>();
            hostEnv.WebRootPath = path;
            hostEnv.WebRootFileProvider.Returns(provider);
            return (path, hostEnv);
        }
    }
}
