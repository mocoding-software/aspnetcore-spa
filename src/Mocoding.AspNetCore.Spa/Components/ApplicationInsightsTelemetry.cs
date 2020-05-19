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
            _snippet = string.Format((IFormatProvider)CultureInfo.InvariantCulture, Resources.JavaScriptSnippet, $"instrumentationKey: \"{config.InstrumentationKey}\"", string.Empty);
        }

        public string GetCodeSnippet()
        {
            return _snippet;
        }
    }
}
