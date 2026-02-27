using Fl.Functional.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WebApp = Microsoft.AspNetCore.Builder.WebApplication;
using Fl.Shared.Utils.Components.Web.Endpoints;

namespace Fl.Shared.Utils.Components.Web.Extensions.WebApplication;

[ExcludeFromCodeCoverage(Justification = "Static method involving extension method calls")]
public static class EndpointWebApplicationExtensions
{
    public static WebApp UseEndpointDefinitions(this WebApp app)
        => app.Tee(a => a
            .Services
            .GetRequiredService<IEnumerable<IEndpoint>>()
            .ForEach(_ => _.DefineEndpoints(app)));
}
