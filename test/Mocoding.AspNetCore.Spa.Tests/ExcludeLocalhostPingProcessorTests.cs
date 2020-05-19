using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Mocoding.AspNetCore.Spa.Components;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace Mocoding.AspNetCore.Spa.Tests
{
    public class ExcludeLocalhostPingProcessorTests
    {
        [Fact]
        public void SkipLocalhostRequestTest()
        {
            var next = Substitute.For<ITelemetryProcessor>();
            var processor = new ExcludeLocalhostPingProcessor(next);
            var request = new RequestTelemetry {Url = new Uri("http://localhost")};

            processor.Process(request);

            next.Received(0).Process(request);
        }

        [Fact]
        public void PassOtherRequestTest()
        {
            var next = Substitute.For<ITelemetryProcessor>();
            var processor = new ExcludeLocalhostPingProcessor(next);

            var request = new RequestTelemetry { Url = new Uri("http://localhost:5000") };
            processor.Process(request);
            next.Received().Process(request);

            request = new RequestTelemetry { Url = new Uri("https://example.com") };
            processor.Process(request);
            next.Received().Process(request);
        }
    }
}
