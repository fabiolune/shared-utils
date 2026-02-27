using Fl.Functional.Utils;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace Fl.Shared.Utils.Components.Web.Middlewares.ExceptionHandler;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class ExceptionHandlerApplicationBuilderExtensions
{
    public static WebApplication UseJsonExceptionHandler(this WebApplication app) =>
        app.Tee(_ => _.UseMiddleware<JsonExceptionHandlerMiddleware>());
}