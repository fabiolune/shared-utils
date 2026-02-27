using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using Fl.Shared.Utils.Components.Web.Middlewares.ServiceDescription;
using System.IO;
using System.Threading.Tasks;

namespace Fl.Shared.Utils.Components.Unit.Tests.Web.Middlewares.ServiceDescription;

public class ServiceDescriptionMiddlewareTests
{
    private ServiceDescriptionMiddleware _sut;
    private Components.Web.Models.ServiceDescription _config;
    private DefaultHttpContext _context;

    [SetUp]
    public void SetUp()
    {
        _config = new Components.Web.Models.ServiceDescription
        {
            Name = "some name",
            Version = "some version"
        };

        _sut = new ServiceDescriptionMiddleware(_config);
    }

    [Test]
    public void InvokeAsync_WhenPathIsCorrect_ShouldReturnServiceDescription()
    {
        var isRequestDelegateInvoked = false;
        _context = new DefaultHttpContext();
        _context.Request.Path = "/internal/description";
        _context.Request.Method = "GET";
        _context.Response.Body = new MemoryStream();

        _sut.InvokeAsync(_context, _ =>
        {
            isRequestDelegateInvoked = true;
            return Task.CompletedTask;
        }).Wait();

        _context.Response.StatusCode.ShouldBe(200);
        isRequestDelegateInvoked
            .ShouldBeFalse();
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = new StreamReader(_context.Response.Body)
            .ReadToEnd();
        var result = JsonConvert.DeserializeObject<Components.Web.Models.ServiceDescription>(body);
        result.ShouldBeEquivalentTo(_config);
    }

    [Test]
    public void InvokeAsync_WhenPathIsNotCorrect_ShouldInvokeNext()
    {
        var isRequestDelegateInvoked = false;
        _context = new DefaultHttpContext();
        _context.Request.Path = "/wrong/path";
        _context.Request.Method = "GET";

        _sut.InvokeAsync(_context, _ =>
        {
            isRequestDelegateInvoked = true;
            return Task.CompletedTask;
        }).Wait();

        _context.Response.StatusCode.ShouldBe(200);
        isRequestDelegateInvoked
            .ShouldBeTrue();
    }
}
