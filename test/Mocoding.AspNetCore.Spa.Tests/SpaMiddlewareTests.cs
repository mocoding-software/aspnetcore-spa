using System.Threading.Tasks;

using System;
using Microsoft.AspNetCore.Http;
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
            renderer.RenderHtmlPageAsync(context).Returns(expectedResponse);
            var middleware = new SpaMiddleware((ctx) => throw new InvalidOperationException());            
            
            // act
            await middleware.InvokeAsync(context, renderer);

            // assert            
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var actualResponse = reader.ReadToEnd();

            Assert.Equal(expectedResponse, actualResponse);
        }

        [Fact]
        public async void SpaMiddlewarePassTest()
        {            
            // arrange
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/no-accept";
            context.Response.Body = new MemoryStream();
            var renderer = Substitute.For<IServerRenderer>();                                   
            var pass = false; 
            var middleware = new SpaMiddleware((ctx) => { pass = true; return Task.FromResult(0); });            
            
            // act
            await middleware.InvokeAsync(context, renderer);            

            Assert.True(pass);
        }

        [Fact]
        public async void SpaMiddlewareCustomPathTest()
        {            
            // arrange
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/profile";
            context.Request.Headers.Add("Accept", "text/html");
            context.Response.Body = new MemoryStream();
            var renderer = Substitute.For<IServerRenderer>();                        
            const string expectedResponse = "<html></html>";
            renderer.RenderHtmlPageAsync(context).Returns(expectedResponse);
            var middleware = new SpaMiddleware((ctx) => throw new InvalidOperationException());            
            
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