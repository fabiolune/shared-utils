using LanguageExt;
using NSubstitute;
using NUnit.Framework;
using Serilog;
using Shouldly;
using Fl.Shared.Utils.Components.Logging.Extensions.Either;
using System;
using Err = LanguageExt.Common.Error;

namespace Fl.Shared.Utils.Components.Unit.Tests.Extensions.Either;

public class EitherExtensionsTests
{
    private ILogger _mockLogger;

    [SetUp]
    public void SetUp() => _mockLogger = Substitute.For<ILogger>();

    [Test]
    public void TeeLogWithTemplate_WhenEitherIsLeft_ShouldLog()
    {
        var input = Either<Err, string>.Left(Err.New("some error"));

        var result = input.TeeLog(_mockLogger, "some template", "something", "something else");

        result.ShouldBeEquivalentTo(input);

        _mockLogger
            .Received(1)
            .Error("some template", (object)"something", (object)"something else");
    }

    [Test]
    public void TeeLogWithTemplate_WhenEitherIsLeftWithException_ShouldLog()
    {
        var exception = new Exception("some exception message");
        var input = Either<Err, string>.Left(Err.New(exception));

        var result = input.TeeLog(_mockLogger, "some template", "something", "something else");

        result.ShouldBeEquivalentTo(input);

        _mockLogger
            .Received(1)
            .Error(exception, "some template", (object)"something", (object)"something else");
    }

    [Test]
    public void TeeLogWithComponentName_WhenEitherIsLeft_ShouldLog()
    {
        var input = Either<Err, string>.Left(Err.New("some error"));

        var result = input.TeeLog(_mockLogger, "component name");

        result.ShouldBeEquivalentTo(input);

        _mockLogger
            .Received(1)
            .Error("{Component} raised an error with {Message}", (object)"component name", (object)"some error");
    }

    [Test]
    public void TeeLogWithComponentName_WhenEitherIsLeftWithException_ShouldLog()
    {
        var exception = new Exception("some exception message");
        var input = Either<Err, string>.Left(Err.New(exception));

        var result = input.TeeLog(_mockLogger, "component name");

        result.ShouldBeEquivalentTo(input);

        _mockLogger
            .Received(1)
            .Error(exception, "{Component} raised an error with {Message}", (object)"component name", (object)"some exception message");
    }

    [Test]
    public void TeeLogWithTemplate_WhenEitherIsRight_ShouldNotLog()
    {
        var input = Either<Err, string>.Right("something");

        var result = input.TeeLog(_mockLogger, "some template", "something", "something else");

        result.ShouldBeEquivalentTo(input);

        _mockLogger
            .DidNotReceiveWithAnyArgs()
            .Error(default, default, default);
    }

    [Test]
    public void TeeLogWithComponentName_WhenEitherIsRight_ShouldNotLog()
    {
        var input = Either<Err, string>.Right("something");

        var result = input.TeeLog(_mockLogger, "component name");

        result.ShouldBeEquivalentTo(input);

        _mockLogger
            .DidNotReceiveWithAnyArgs()
            .Error(default, default, default);
    }

    [Test]
    public void TeeLogWithTemplate_WhenEitherAsyncIsLeft_ShouldLog()
    {
        var error = Err.New("some error");
        var input = EitherAsync<Err, string>.Left(error);

        _ = input.TeeLog(_mockLogger, "some template", "something", "something else");

        _mockLogger
            .Received(1)
            .Error("some template", (object)"something", (object)"something else");
    }

    [Test]
    public void TeeLogWithTemplate_WhenEitherAsyncIsLeftWithException_ShouldLog()
    {
        var exception = new Exception("some exception message");
        var input = EitherAsync<Err, string>.Left(Err.New(exception));

        _ = input.TeeLog(_mockLogger, "some template", "something", "something else");

        _mockLogger
            .Received(1)
            .Error(exception, "some template", (object)"something", (object)"something else");
    }

    [Test]
    public void TeeLogWithComponentName_WhenEitherAsyncIsLeft_ShouldLog()
    {
        var input = EitherAsync<Err, string>.Left(Err.New("some error"));

        _ = input.TeeLog(_mockLogger, "component name");

        _mockLogger
            .Received(1)
            .Error("{Component} raised an error with {Message}", (object)"component name", (object)"some error");
    }

    [Test]
    public void TeeLogWithComponentName_WhenEitherAsyncIsLeftWithException_ShouldLog()
    {
        var exception = new Exception("some exception message");
        var input = EitherAsync<Err, string>.Left(Err.New(exception));

        _ = input.TeeLog(_mockLogger, "component name");

        _mockLogger
            .Received(1)
            .Error(exception, "{Component} raised an error with {Message}", (object)"component name", (object)"some exception message");
    }

    [Test]
    public void TeeLogWithTemplate_WhenEitherAsyncIsRight_ShouldNotLog()
    {
        var input = EitherAsync<Err, string>.Right("something");

        _ = input.TeeLog(_mockLogger, "some template", "something", "something else");

        _mockLogger
            .DidNotReceiveWithAnyArgs()
            .Error(default, default, default);
    }

    [Test]
    public void TeeLogWithComponentName_WhenEitherAsyncIsRight_ShouldNotLog()
    {
        var input = EitherAsync<Err, string>.Right("something");

        _ = input.TeeLog(_mockLogger, "component name");

        _mockLogger
            .DidNotReceiveWithAnyArgs()
            .Error(default, default, default);
    }
}
