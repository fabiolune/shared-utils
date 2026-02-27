using System.Diagnostics.CodeAnalysis;
using static System.String;

namespace Fl.Shared.Utils.Components.Web.Models;

[ExcludeFromCodeCoverage]
public record ServiceDescription
{
    public string Name { get; init; } = Empty;
    public string Version { get; init; } = Empty;
}
