using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Fl.Shared.Utils.Components.Web.Middlewares.ExceptionHandler;

namespace Fl.Shared.Utils.Components.Unit.Tests.Web.Middlewares.ExceptionHandler;

public class ExceptionHandlerServiceCollectionExtensions
{
    [Test]
    public void AddExceptionHandler_ShouldAddMiddleware()
    {
        var services = Substitute.For<IServiceCollection>();
        services
            .AddExceptionHandler();
        services
            .Received(1)
            .Add(Arg.Is<ServiceDescriptor>(d => d.ServiceType == typeof(JsonExceptionHandlerMiddleware)));
    }
}