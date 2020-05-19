using System;
using Jering.Javascript.NodeJS;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
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
    /// <summary>
    /// A set of useful predefined extensions to configure pipeline for SPA.
    /// </summary>
    public static class SpaExtensions
    {
        /// <summary>
        /// Uses the static files with cache.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>Application Builder.</returns>
        public static IApplicationBuilder UseStaticFilesWithCache(this IApplicationBuilder app)
        {
            return app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24 * 365; // one year
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
                },
            });
        }

        /// <summary>
        /// Use SPA prerendering middleware for React Application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="script">The script.</param>
        public static void UseReactSpa(this IApplicationBuilder app, IWebHostEnvironment env, string sourcePath = ".", string script = "start")
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

        /// <summary>
        /// Adds the application insights telemetry.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Service Collection.</returns>
        public static IServiceCollection AddAppInsightsTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddApplicationInsightsTelemetry(configuration)
                .AddApplicationInsightsTelemetryProcessor<ExcludeLocalhostPingProcessor>()
                .AddSingleton<ITelemetryProvider, ApplicationInsightsTelemetry>();
        }

        /// <summary>
        /// Adds the application insights telemetry.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="configureOptions">Configure Telemetry Options</param>
        /// <returns>Service Collection.</returns>
        public static IServiceCollection AddAppInsightsTelemetry(this IServiceCollection services, IConfiguration configuration, Action<ApplicationInsightsServiceOptions> configureOptions)
        {
            services.Configure(configureOptions);
            return services.AddAppInsightsTelemetry(configuration);
        }

        /// <summary>
        /// Adds the SPA renderer.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>Service Collection.</returns>
        public static IServiceCollection AddSpaRenderer(this IServiceCollection services) =>
            AddSpaRenderer(services, options => { });

        /// <summary>
        /// Adds the SPA renderer.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configure">Configuration.</param>
        /// <returns>Service Collection.</returns>
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
