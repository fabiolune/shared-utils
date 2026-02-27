using Microsoft.AspNetCore.Http;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Fl.Shared.Utils.Components.Unit.Tests.Utils;
using Fl.Shared.Utils.Components.Web.Middlewares.ExceptionHandler;
using Fl.Shared.Utils.Components.Web.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fl.Shared.Utils.Components.Unit.Tests.Web.Middlewares.ExceptionHandler;

public class ExceptionHandlerMiddlewareTests
{
    private JsonExceptionHandlerMiddleware _sut;
    private RequestDelegate _mockDelegate;

    private static readonly JsonSerializerOptions SerializationOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [SetUp]
    public void Setup()
    {
        _sut = new JsonExceptionHandlerMiddleware();
        _mockDelegate = Substitute.For<RequestDelegate>();
    }

    [Test]
    public void InvokeAsync_WhenNextThrows_ShouldReturnErrorMessage()
    {
        var context = TestUtils.SetupHttpContext("/whatever");
        var exception = new Exception("message");
        _mockDelegate(context).Returns(_ => throw exception);

        _sut.InvokeAsync(context, _mockDelegate).Wait();

        context
            .Response
            .StatusCode
            .ShouldBe(StatusCodes.Status500InternalServerError);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = new StreamReader(context.Response.Body).ReadToEnd();
        var parsedResponse = JsonSerializer.Deserialize<ErrorResponse>(body, SerializationOptions);
        parsedResponse.Message.ShouldBe(exception.Message);
    }

    [Test]
    public void InvokeAsync_WhenNextDoesNotThrow_ShouldReturnNextReturn()
    {
        var context = TestUtils.SetupHttpContext("/whatever");

        var called = false;
        _mockDelegate(context).Returns(Task.Run(() => called = true));

        _sut.InvokeAsync(context, _mockDelegate).Wait();

        called.ShouldBeTrue();
    }
}
