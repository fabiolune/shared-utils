using Microsoft.Extensions.DependencyInjection;

namespace Fl.Shared.Utils.Components.Web.Middlewares.RequestLogContext;

public static class RequestLogContextServiceCollectionExtensions
{
    public static IServiceCollection AddRequestLogContext(this IServiceCollection services) =>
        services.AddSingleton<RequestLogContextMiddleware>();
}
