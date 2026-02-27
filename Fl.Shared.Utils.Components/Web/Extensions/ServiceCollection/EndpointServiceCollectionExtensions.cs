using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Fl.Shared.Utils.Components.Web.Endpoints;

namespace Fl.Shared.Utils.Components.Web.Extensions.ServiceCollection;

[ExcludeFromCodeCoverage]
public static class EndpointServiceCollectionExtensions
{
    public static IServiceCollection AddEndpointDefinitions(
        this IServiceCollection services, params Type[] scanMarkers)
        => services.Scan(_ => _
            .FromAssembliesOf(scanMarkers)
            .AddClasses(__ => __.AssignableTo<IEndpoint>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

    public static IServiceCollection AddEndpointDefinitions(
        this IServiceCollection services, params Assembly[] assemblies)
        => services.Scan(_ => _
            .FromAssemblies(assemblies)
            .AddClasses(__ => __.AssignableTo<IEndpoint>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
}