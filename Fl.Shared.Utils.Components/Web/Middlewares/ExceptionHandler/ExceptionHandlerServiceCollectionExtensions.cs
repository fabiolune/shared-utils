using Microsoft.Extensions.DependencyInjection;

namespace Fl.Shared.Utils.Components.Web.Middlewares.ExceptionHandler;

public static class RequestLogContextServiceCollectionExtensions
{
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services) =>
        services.AddSingleton<JsonExceptionHandlerMiddleware>();
}