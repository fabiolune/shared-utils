using Fl.Functional.Utils;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace Fl.Shared.Utils.Components.Web.Middlewares.ServiceDescription;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class ServiceDescriptionApplicationBuilderExtensions
{
    public static WebApplication UseServiceDescription(this WebApplication app) =>
        app.Tee(_ => _.UseMiddleware<ServiceDescriptionMiddleware>());
}
