using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationInsights.Extensibility;
using Mocoding.AspNetCore.Spa.Components;
using Xunit;

namespace Mocoding.AspNetCore.Spa.Tests
{
    public class ApplicationInsightsTelemetryTests
    {
        [Fact]
        public void DefaultAppInsightsTest()
        {
            var config = new TelemetryConfiguration {InstrumentationKey = "test"};
            var telemetry = new ApplicationInsightsTelemetry(config);

            var snippet = telemetry.GetCodeSnippet();

            Assert.Contains(config.InstrumentationKey, snippet);
        }
    }
}
