using Microsoft.AspNetCore.Builder;
using Fl.Functional.Utils;
using System.Diagnostics.CodeAnalysis;

namespace Fl.Shared.Utils.Components.Web.Middlewares.TraceLogContext;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class TraceLogContextApplicationBuilderExtensions
{
    public static WebApplication UseTraceLogContext(this WebApplication app) => 
        app.Tee(_ => _.UseMiddleware<TraceLogContextMiddleware>(Array.Empty<object>()));
}
