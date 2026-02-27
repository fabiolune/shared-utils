using Fl.Functional.Utils;
using Fl.Shared.Utils.Components.Logging.Extensions.Builder;
using Fl.Shared.Utils.Components.Logging.Extensions.HostBuilder;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace Fl.Shared.Utils.Components.Logging.Extensions.Builder;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class LoggingWebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder builder) =>
        builder
            .Tee(b => b.Host.AddSerilogLogging(b.Configuration));
}
