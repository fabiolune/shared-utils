using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Diagnostics.CodeAnalysis;
using static Fl.Functional.Utils.Functional;

namespace Fl.Shared.Utils.Components.Web.Middlewares.RequestLogContext;

[ExcludeFromCodeCoverage(Justification = "it uses global static LogContext")]
public class RequestLogContextMiddleware : IMiddleware
{
    private const string CorrelationIdPropertyName = "CorrelationId";

    private readonly ICorrelationContextAccessor _accessor;

    public RequestLogContextMiddleware(ICorrelationContextAccessor accessor) => _accessor = accessor;

    public Task InvokeAsync(HttpContext context, RequestDelegate next) =>
        _accessor
            .CorrelationContext
            .CorrelationId
            .Map<string, Task>(_ => 
                Using(LogContext.PushProperty(CorrelationIdPropertyName, _), _ => next.Invoke(context))
            );
}