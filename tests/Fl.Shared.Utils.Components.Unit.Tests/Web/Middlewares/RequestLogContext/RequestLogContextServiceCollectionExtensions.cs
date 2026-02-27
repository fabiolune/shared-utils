using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Fl.Shared.Utils.Components.Web.Middlewares.RequestLogContext;

namespace Fl.Shared.Utils.Components.Unit.Tests.Web.Middlewares.RequestLogContext;

public class RequestLogContextServiceCollectionExtensions
{
    [Test]
    public void AddExceptionHandler_ShouldAddMiddleware()
    {
        var services = Substitute.For<IServiceCollection>();
        services
            .AddRequestLogContext();
        services
            .Received(1)
            .Add(Arg.Is<ServiceDescriptor>(d => d.ServiceType == typeof(RequestLogContextMiddleware)));
    }
}
