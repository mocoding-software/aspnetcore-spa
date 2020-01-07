using System.Threading.Tasks;

using System;
using Microsoft.AspNetCore.Http;
using Mocoding.AspNetCore.Spa;
using Xunit;
using NSubstitute;
using Mocoding.AspNetCore.Spa.Abstractions;
using System.IO;

namespace Mocoding.AspNetCore.Spa.Tests
{
    public class SpaMiddlewareTests
    {
        [Fact]
        public async void SpaMiddlewareDefaultTest()
        {            
            // arrange
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/";
            context.Response.Body = new MemoryStream();
            var renderer = Substitute.For<IServerRenderer>();                        
            const string expectedResponse = "<html></html>";
            renderer.RenderHtmlPage(context).Returns(expectedResponse);
            var middleware = new SpaMiddleware((context) => throw new InvalidOperationException());            
            
            // act
            await middleware.InvokeAsync(context, renderer);

            // assert            
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var actualResponse = reader.ReadToEnd();

            Assert.Equal(expectedResponse, actualResponse);
        }

    }
}