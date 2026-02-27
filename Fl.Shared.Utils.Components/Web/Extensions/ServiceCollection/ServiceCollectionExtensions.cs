using CorrelationId.DependencyInjection;
using Fl.Configuration.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fl.Shared.Utils.Components.Web.Middlewares.ServiceDescription;
using Fl.Shared.Utils.Components.Web.Middlewares.RequestLogContext;
using Fl.Shared.Utils.Components.Web.Middlewares.ExceptionHandler;

namespace Fl.Shared.Utils.Components.Web.Extensions.ServiceCollection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultComponents(this IServiceCollection services, IConfiguration config) =>
        services
            .AddExceptionHandler()
            .AddRequestLogContext()
            .AddServiceDescription(config)
            .AddDefaultCorrelationId()
            .AddRoutingBasePath(config)
            .AddJsonSerializationConfiguration();

    public static IServiceCollection AddFromAppConfiguration<T>(this IServiceCollection services, IConfiguration config) where T : class, new() =>
        services.AddSingleton(config.GetRequiredConfiguration<T>());
}
