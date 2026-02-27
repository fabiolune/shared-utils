using CorrelationId;
using Fl.Functional.Utils;
using System.Diagnostics.CodeAnalysis;
using WebApp = Microsoft.AspNetCore.Builder.WebApplication;
using Fl.Shared.Utils.Components.Web.Middlewares.RequestLogContext;
using Fl.Shared.Utils.Components.Web.Middlewares.ServiceDescription;
using Fl.Shared.Utils.Components.Web.Middlewares.ExceptionHandler;

namespace Fl.Shared.Utils.Components.Web.Extensions.WebApplication;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class ApplicationBuilderExtensions
{
    public static WebApp UseDefaultMiddlewares(this WebApp app) =>
        app
            .UseServiceDescription()
            .Tee(_ => _.UseCorrelationId())
            .UseRequestLogContext()
            .UseJsonExceptionHandler();
}
