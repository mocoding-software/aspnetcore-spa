using System;
using System.Globalization;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.ApplicationInsights.Extensibility;
using Mocoding.AspNetCore.Spa.Abstractions;

namespace Mocoding.AspNetCore.Spa.Components
{
    class ApplicationInsightsTelemetry : ITelemetryProvider
    {
        private readonly string _snippet;

        public ApplicationInsightsTelemetry(TelemetryConfiguration config)
        {
            var snippet = string.Format((IFormatProvider)CultureInfo.InvariantCulture, Resources.JavaScriptSnippet, $"instrumentationKey: \"{config.InstrumentationKey}\"", string.Empty);
            var start = snippet.IndexOf("var", StringComparison.Ordinal);
            var end = snippet.LastIndexOf("</script>", StringComparison.Ordinal);
            _snippet = snippet.Substring(start, end - start);
        }
        public string GetCodeSnippet()
        {
            return _snippet;
        }
    }
}
