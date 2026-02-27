using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Fl.Shared.Utils.Components.Web.Middlewares.TraceLogContext;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class TraceLogContextServiceCollectionExtensions
{
    public static IServiceCollection AddTraceLogContext(this IServiceCollection services) =>
        services.AddSingleton<TraceLogContextMiddleware>();
}