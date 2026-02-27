using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Fl.Shared.Utils.Components.Web.Extensions.ServiceCollection;
using Fl.Shared.Utils.Components.Web.Models;

namespace Fl.Shared.Utils.Components.Unit.Tests.Web.Middlewares.ServiceDescription;

public class RoutingBasePathServiceCollectionExtensionsTests
{
    private IServiceCollection _services;

    [SetUp]
    public void Setup() => _services = Substitute.For<IServiceCollection>();

    [TestCase("some_path")]
    [TestCase("")]
    public void AddRoutingBasePath_WhenConfigurationIsNotNull_ShouldAddConfiguration(string path)
    {
        _services
            .AddRoutingBasePath(new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource
                {
                    InitialData = new Dictionary<string, string>
                    {
                                        {"RoutingConfiguration:PathBase", path}
                                    }
                })
                .Build()
            );

        _services
            .Received(1)
            .Add(Arg.Is<ServiceDescriptor>(d =>
                d.ServiceType == typeof(RoutingConfiguration)
                    && ((RoutingConfiguration)d.ImplementationInstance).PathBase == path
                ));
    }

    [Test]
    public void AddRoutingBasePath_WhenConfigurationIsNull_ShouldAddDefaultConfiguration()
    {
        _services
            .AddRoutingBasePath(
                new ConfigurationBuilder()
                    .Add(new MemoryConfigurationSource())
                    .Build()
            );

        _services
            .Received(1)
            .Add(Arg.Is<ServiceDescriptor>(d =>
                d.ServiceType == typeof(RoutingConfiguration)
                    && ((RoutingConfiguration)d.ImplementationInstance).PathBase == string.Empty
                ));
    }
}