using System;
using System.Collections.Generic;
using System.Text;
using Jering.Javascript.NodeJS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Mocoding.AspNetCore.Spa.Abstractions;
using Mocoding.AspNetCore.Spa.Components;

namespace Mocoding.AspNetCore.Spa
{
    public static class SpaExtensions
    {
        public static IApplicationBuilder UseStaticFilesWithCache(this IApplicationBuilder app)
        {
            return app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24 * 365; //one year
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
                }
            });
        }

        public static void UsaReactSpa(this IApplicationBuilder app, IWebHostEnvironment env, string sourcePath = ".", string script = "start")
        {
            if (env.IsDevelopment())
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = sourcePath;
                    spa.UseReactDevelopmentServer(script);
                });
            }

            app.UseMiddleware<SpaMiddleware>();
        }

        public static IServiceCollection AddAppInsightsTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddApplicationInsightsTelemetry(configuration)
                .AddSingleton<ITelemetryProvider, ApplicationInsightsTelemetry>();
        }

        public static IServiceCollection AddSpaRenderer(this IServiceCollection services) =>
            AddSpaRenderer(services, options => { });

        public static IServiceCollection AddSpaRenderer(this IServiceCollection services, Action<SpaOptions> configure)
        {
            services.Configure(configure);
            return services
                .AddNodeJS()
                .AddSingleton<IServerRenderer, JeringServerRenderer>()
                .AddSingleton<IAssetsResolver, WebRootAssetsResolver>();
        }
    }
}
