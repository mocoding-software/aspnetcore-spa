using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Mocoding.AspNetCore.Spa.Components
{
  /// <summary>
  /// Exclude health check requests from telemetry.
  /// </summary>
  /// <seealso cref="Microsoft.ApplicationInsights.Extensibility.ITelemetryProcessor" />
  internal class ExcludeLocalhostPingProcessor : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor _next;

        // next will point to the next TelemetryProcessor in the chain.
        public ExcludeLocalhostPingProcessor(ITelemetryProcessor next)
        {
            _next = next;
        }

        public void Process(ITelemetry item)
        {
            // Filter localhost calls
            if (item is RequestTelemetry request && request.Url.Host == "localhost" && request.Url.Port == 80)
                return;

            _next.Process(item);
        }
    }
}
