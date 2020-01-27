namespace Mocoding.AspNetCore.Spa.Abstractions
{
    /// <summary>
    /// Provides snippet for telemetry.
    /// </summary>
    public interface ITelemetryProvider
    {
        /// <summary>
        /// Gets telemetry code snippet.
        /// </summary>
        /// <returns>Javascript snippet.</returns>
        string GetCodeSnippet();
    }
}
