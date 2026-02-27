using Fl.Functional.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fl.Shared.Utils.Components.Web.Extensions.ServiceCollection;

namespace Fl.Shared.Utils.Components.Web.Middlewares.ServiceDescription;

public static class RequestLogContextServiceCollectionExtensions
{
    public static IServiceCollection AddServiceDescription(this IServiceCollection services, IConfiguration configuration) =>
        (services, configuration)
            .Map(_ => _.services.AddFromAppConfiguration<Models.ServiceDescription>(_.configuration))
            .Tee(_ => _.AddSingleton<ServiceDescriptionMiddleware>());
}
