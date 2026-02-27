using Serilog;
using Fl.Functional.Utils;
using Err = LanguageExt.Common.Error;

namespace Fl.Shared.Utils.Components.Logging.Extensions.Error;
public static class ErrorExtensions
{
    private const string DefaultTemplate = "{Component} raised an error with {Message}";

    public static Action ToLoggingAction(this Err err, ILogger logger, string template, params object[] parameters) =>
        err
           .Map(e => e.Exception.Match(ex => () => logger.Error(ex, template, parameters),
                () => new Action(() => logger.Error(template, parameters))));

    public static Action ToLoggingAction(this Err err, ILogger logger, string componentName) =>
        err.ToLoggingAction(logger, DefaultTemplate, componentName, err.Message);

}
