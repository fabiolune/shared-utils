using Fl.Configuration.Extensions;
using Fl.Functional.Utils;
using Fl.Shared.Utils.Components.Logging.Extensions.LoggerConfiguration;
using Fl.Shared.Utils.Components.Logging.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace Fl.Shared.Utils.Components.Logging.Extensions.HostBuilder;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class HostBuilderExtensions
{
    public static IHostBuilder AddSerilogLogging(this IHostBuilder builder, SerilogConfiguration cfg) =>
        builder
            .Tee(b => b.UseSerilog((_, lc) => lc.ConfigureDefault(cfg)));

    public static IHostBuilder AddSerilogLogging(this IHostBuilder builder, IConfiguration configuration) =>
        builder.AddSerilogLogging(configuration.GetRequiredConfiguration<SerilogConfiguration>());
}