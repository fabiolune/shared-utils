using Fl.Functional.Utils;
using Microsoft.AspNetCore.Http;

namespace Fl.Shared.Utils.Components.Web.Middlewares.ServiceDescription;

public class ServiceDescriptionMiddleware : IMiddleware
{
    private readonly Models.ServiceDescription _serviceDescription;
    private const string ServiceDescriptionPathValue = "/internal/description";

    public ServiceDescriptionMiddleware(Models.ServiceDescription serviceDescription) =>
        _serviceDescription = serviceDescription;

    public Task InvokeAsync(HttpContext context, RequestDelegate next) =>
        context
            .Request
            .Path
            .Value
            .MakeOption(path => path != ServiceDescriptionPathValue)
            .Match
            (
                _ => context.Response.WriteAsJsonAsync(_serviceDescription),
                () => next.Invoke(context)
            );

}
