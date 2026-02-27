using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using static Fl.Functional.Utils.Functional;

namespace Fl.Shared.Utils.Components.Web.Middlewares.TraceLogContext;

[ExcludeFromCodeCoverage(Justification = "it uses global static LogContext")]
public class TraceLogContextMiddleware : IMiddleware
{
    private const string SpanIdPropertyName = "SpanId";
    private const string TraceIdPropertyName = "TraceId";

    public Task InvokeAsync(HttpContext context, RequestDelegate next) =>
        Activity.Current
            .MakeOption(a => a!, a => a == default)
            .Match(
                a => Using(
                    new CompositeDisposable(
                        LogContext.PushProperty(SpanIdPropertyName, a.SpanId),
                        LogContext.PushProperty(TraceIdPropertyName, a.TraceId)),
                    _ => next.Invoke(context)),
                () => next.Invoke(context));
}