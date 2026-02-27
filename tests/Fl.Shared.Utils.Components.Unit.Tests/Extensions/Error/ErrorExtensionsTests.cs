using NSubstitute;
using NUnit.Framework;
using Serilog;
using Fl.Shared.Utils.Components.Logging.Extensions.Error;
using System;
using Err = LanguageExt.Common.Error;

namespace Fl.Shared.Utils.Components.Unit.Tests.Extensions.Error;

public class ErrorExtensionsTests
{
    private ILogger _mockLogger;

    [SetUp]
    public void SetUp() => _mockLogger = Substitute.For<ILogger>();

    [Test]
    public void ToLoggingAction_WhenErrorHasNoException_ShouldLogMessage()
    {
        var error = Err.New("some error");

        error.ToLoggingAction(_mockLogger, "some component")();

        _mockLogger
            .Received(1)
            .Error("{Component} raised an error with {Message}", (object)"some component", (object)"some error");
    }

    [Test]
    public void ToLoggingAction_WhenErrorHasException_ShouldLogMessageAndException()
    {
        var exception = new Exception("some message");
        var error = Err.New(exception);

        error.ToLoggingAction(_mockLogger, "some component")();

        _mockLogger
            .Received(1)
            .Error(exception, "{Component} raised an error with {Message}", (object)"some component", (object)"some message");
    }

    [Test]
    public void ToLoggingActionWithTemplate_WhenErrorHasNoException_ShouldLogMessage()
    {
        var error = Err.New("some error");

        error.ToLoggingAction(_mockLogger, "some template", "something", "something else")();

        _mockLogger
            .Received(1)
            .Error("some template", (object)"something", (object)"something else");
    }

    [Test]
    public void ToLoggingActionWithTemplate_WhenErrorHasException_ShouldLogMessageAndException()
    {
        var exception = new Exception("some message");
        var error = Err.New(exception);

        error.ToLoggingAction(_mockLogger, "some template", "something", "something else")();

        _mockLogger
            .Received(1)
            .Error(exception, "some template", (object)"something", (object)"something else");
    }
}
