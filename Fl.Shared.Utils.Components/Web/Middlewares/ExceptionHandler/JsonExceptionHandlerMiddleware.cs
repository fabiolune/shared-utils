using Fl.Functional.Utils;
using Microsoft.AspNetCore.Http;
using Fl.Shared.Utils.Components.Web.Models;
using static LanguageExt.Prelude;

namespace Fl.Shared.Utils.Components.Web.Middlewares.ExceptionHandler;

public class JsonExceptionHandlerMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next) =>
        Try(() => next(context))
            .Match(_ => _, ex =>
                context
                    .Response
                    .Tee(_ => _.StatusCode = StatusCodes.Status500InternalServerError)
                    .WriteAsJsonAsync(new ErrorResponse(ex.Message))
            );
}