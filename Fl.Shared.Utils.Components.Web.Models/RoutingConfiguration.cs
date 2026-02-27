using System.Diagnostics.CodeAnalysis;

namespace Fl.Shared.Utils.Components.Web.Models;

[ExcludeFromCodeCoverage]
public record RoutingConfiguration
{
    public static readonly RoutingConfiguration Default = new()
    {
        PathBase = string.Empty
    };

    public string PathBase { get; init; } = string.Empty;
}