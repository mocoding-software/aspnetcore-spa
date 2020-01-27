using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Mocoding.AspNetCore.Spa.Tests.Integration
{
    public class Factory : WebApplicationFactory<Startup>
    {
        public string ContentRootPath { get; set; }
        public string WebRootPath { get; set; }

        public Factory()
        {
            var path = System.IO.Path.Join(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString("N"));

            ContentRootPath = path;
            WebRootPath = Path.Join(path, "wwwroot");
            Directory.CreateDirectory(WebRootPath);
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(ContentRootPath);
            builder.UseStartup<Startup>();
        }

        protected override void Dispose(bool disposing)
        {
            if (Directory.Exists(ContentRootPath))
                Directory.Delete(ContentRootPath, true);
            base.Dispose(disposing);
        }
    }
}
