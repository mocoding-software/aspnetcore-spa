using System;
using System.Globalization;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.ApplicationInsights.Extensibility;
using Mocoding.AspNetCore.Spa.Abstractions;

namespace Mocoding.AspNetCore.Spa.Components
{
    internal class ApplicationInsightsTelemetry : ITelemetryProvider
    {
        private readonly string _snippet;

        public ApplicationInsightsTelemetry(TelemetryConfiguration config)
        {
            var settings = @$"enableAutoRouteTracking: true,
autoTrackPageVisitTime: true,
disableFetchTracking: false,
instrumentationKey: ""{config.InstrumentationKey}""";
            _snippet = string.Format((IFormatProvider)CultureInfo.InvariantCulture, Resources.JavaScriptSnippet, settings, string.Empty);
        }

        public string GetCodeSnippet()
        {
            return _snippet;
        }
    }
}
