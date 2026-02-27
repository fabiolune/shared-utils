using Serilog.Events;
using System.Diagnostics.CodeAnalysis;
using static System.String;

namespace Fl.Shared.Utils.Components.Logging.Models;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public record SerilogConfiguration
{
    public string Environment { get; init; } = Empty;
    public string System { get; init; } = Empty;
    public string Customer { get; init; } = Empty;
    public LogEventLevel MinimumLevel { get; init; } = LogEventLevel.Information;
    public IDictionary<string, LogEventLevel> Overrides { get; init; } = new Dictionary<string, LogEventLevel>();
}