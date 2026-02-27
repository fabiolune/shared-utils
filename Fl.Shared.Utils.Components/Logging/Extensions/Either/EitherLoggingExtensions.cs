using LanguageExt;
using Fl.Functional.Utils;
using Err = LanguageExt.Common.Error;
using Fl.Shared.Utils.Components.Logging.Extensions.Error;
using Serilog;

namespace Fl.Shared.Utils.Components.Logging.Extensions.Either;
public static class EitherLoggingExtensions
{
    public static Either<Err, T> TeeLog<T>(this Either<Err, T> value, ILogger logger, string template, params object[] parameters) =>
        value.MapLeft(err => err.Tee(e => e.ToLoggingAction(logger, template, parameters)()));

    public static Either<Err, T> TeeLog<T>(this Either<Err, T> value, ILogger logger, string componentName) =>
        value.MapLeft(err => err.Tee(e => e.ToLoggingAction(logger, componentName)()));

    public static EitherAsync<Err, T> TeeLog<T>(this EitherAsync<Err, T> value, ILogger logger, string template, params object[] parameters) =>
        value.MapLeft(err => err.Tee(e => e.ToLoggingAction(logger, template, parameters)()));

    public static EitherAsync<Err, T> TeeLog<T>(this EitherAsync<Err, T> value, ILogger logger, string componentName) =>
        value.MapLeft(err => err.Tee(e => e.ToLoggingAction(logger, componentName)()));
}
