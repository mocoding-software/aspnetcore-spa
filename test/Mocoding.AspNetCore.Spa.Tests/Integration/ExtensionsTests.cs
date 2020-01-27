using System.IO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Net.Http.Headers;
using Xunit;

namespace Mocoding.AspNetCore.Spa.Tests.Integration
{
    public class ExtensionsTests : IClassFixture<Factory>
    {
        private readonly Factory _factory;

        public ExtensionsTests(Factory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void UseStaticFilesWithCache()
        {
            File.Create(Path.Join(_factory.WebRootPath, "index.txt")).Close();
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/index.txt");

            Assert.NotEmpty(response.Headers.CacheControl.ToString());
        }
    }
}
