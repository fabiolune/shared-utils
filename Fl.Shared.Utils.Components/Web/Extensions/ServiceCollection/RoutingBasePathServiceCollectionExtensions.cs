using Fl.Configuration.Extensions;
using Fl.Functional.Utils;
using Fl.Shared.Utils.Components.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fl.Shared.Utils.Components.Web.Extensions.ServiceCollection;

public static class RoutingBasePathServiceCollectionExtensions
{
    public static IServiceCollection AddRoutingBasePath(this IServiceCollection services, IConfiguration configuration) =>
        (Services: services, Configuration: configuration)
                .Map(_ => _.Services.AddSingleton(
                    _.Configuration.GetConfiguration<RoutingConfiguration>().OrElse(RoutingConfiguration.Default)));
}