using Fl.Functional.Utils;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace Fl.Shared.Utils.Components.Web.Middlewares.RequestLogContext;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class RequestLogContextApplicationBuilderExtensions
{
    public static WebApplication UseRequestLogContext(this WebApplication app) =>
        app.Tee(_ => _.UseMiddleware<RequestLogContextMiddleware>());
}
