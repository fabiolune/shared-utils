using Fl.Functional.Utils;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.Diagnostics.CodeAnalysis;
using Fl.Shared.Utils.Components.Logging.Models;

namespace Fl.Shared.Utils.Components.Logging.Extensions.LoggerConfiguration;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class LoggerConfigurationExtensions
{
    private static readonly
        Dictionary<LogEventLevel, Func<LoggerMinimumLevelConfiguration, Serilog.LoggerConfiguration>> LogLevelMapping =
        new()
        {
            { LogEventLevel.Verbose, mlc => mlc.Verbose() },
            { LogEventLevel.Debug, mlc => mlc.Debug() },
            { LogEventLevel.Information, mlc => mlc.Information() },
            { LogEventLevel.Warning, mlc => mlc.Warning() },
            { LogEventLevel.Error, mlc => mlc.Error() },
            { LogEventLevel.Fatal, mlc => mlc.Fatal() }
        };

    public static Serilog.LoggerConfiguration ConfigureDefault(this Serilog.LoggerConfiguration config) =>
        config
            .ConfigureDefault(LogEventLevel.Information);

    public static Serilog.LoggerConfiguration ConfigureDefault(this Serilog.LoggerConfiguration config, SerilogConfiguration cfg) =>
        config
            .ConfigureDefault(cfg.MinimumLevel, cfg.Overrides)
            .Enrich
            .WithProperty(nameof(SerilogConfiguration.Environment), cfg.Environment)
            .Enrich
            .WithProperty(nameof(SerilogConfiguration.System), cfg.System)
            .Enrich
            .WithProperty(nameof(SerilogConfiguration.Customer), cfg.Customer);

    public static Serilog.LoggerConfiguration ConfigureDefault(this Serilog.LoggerConfiguration config, LogEventLevel level) =>
        level
            .MakeOption(l => !LogLevelMapping.ContainsKey(l))
            .IfNone(LogEventLevel.Information)
            .Map(l => LogLevelMapping[l])
            .Map(config.MinimumLevel.Map)
            .Enrich
            .FromLogContext()
            .WriteTo
            .Async(c => c.Console(new CompactJsonFormatter()));

    public static Serilog.LoggerConfiguration ConfigureDefault(this Serilog.LoggerConfiguration config, LogEventLevel level, IDictionary<string, LogEventLevel> overrides) =>
        config
            .ConfigureDefault(level)
            .Map(_ => overrides.Aggregate(_, (lc, kv) => lc.MinimumLevel.Override(kv.Key, kv.Value)));
}