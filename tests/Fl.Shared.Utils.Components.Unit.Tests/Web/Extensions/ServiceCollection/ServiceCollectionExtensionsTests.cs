using CorrelationId;
using CorrelationId.Abstractions;
using CorrelationId.Providers;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Shouldly;
using Fl.Shared.Utils.Components.Web.Extensions.ServiceCollection;
using Fl.Shared.Utils.Components.Web.Middlewares.ExceptionHandler;
using Fl.Shared.Utils.Components.Web.Middlewares.RequestLogContext;
using Fl.Shared.Utils.Components.Web.Middlewares.ServiceDescription;
using Fl.Shared.Utils.Components.Web.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fl.Shared.Utils.Components.Unit.Tests.Web.Extensions.ServiceCollection;

public class ServiceCollectionExtensionsTests
{

    private Microsoft.Extensions.DependencyInjection.ServiceCollection _sut;

    [SetUp]
    public void SetUp() => _sut = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

    [Test]
    public void AddDefaultComponents_WithoutRoutingConfiguration_ShouldAddExpectedComponentsWithDefaultRouting()
    {
        var provider = _sut
            .AddDefaultComponents(new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource
                {
                    InitialData = new Dictionary<string, string>
                    {
                        {"ServiceDescription:Name", "some name"},
                        {"ServiceDescription:Version", "some version"}
                    }
                }).Build())
            .BuildServiceProvider();

        provider
            .GetRequiredService<JsonExceptionHandlerMiddleware>()
            .ShouldNotBeNull();

        provider
            .GetRequiredService<RequestLogContextMiddleware>()
            .ShouldNotBeNull();

        provider
            .GetRequiredService<ServiceDescription>()
            .ShouldBeEquivalentTo(new ServiceDescription
            {
                Name = "some name",
                Version = "some version"
            });

        provider
            .GetRequiredService<ServiceDescriptionMiddleware>()
            .ShouldNotBeNull();

        provider
            .GetRequiredService<ICorrelationContextAccessor>()
            .ShouldBeAssignableTo<CorrelationContextAccessor>();

        provider
            .GetRequiredService<ICorrelationContextFactory>()
            .ShouldBeAssignableTo<CorrelationContextFactory>();

        provider
            .GetRequiredService<ICorrelationIdProvider>()
            .ShouldBeAssignableTo<GuidCorrelationIdProvider>();

        provider
            .GetRequiredService<RoutingConfiguration>()
            .ShouldBeEquivalentTo(new RoutingConfiguration
            {
                PathBase = ""
            });
    }

    [Test]
    public void AddDefaultComponents_WithRoutingConfiguratio_ShouldAddExpectedComponentsWithExpectedRouting()
    {
        var provider = _sut
            .AddDefaultComponents(new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource
                {
                    InitialData = new Dictionary<string, string>
                    {
                        {"RoutingConfiguration:PathBase", "some path"},
                        {"ServiceDescription:Name", "some name"},
                        {"ServiceDescription:Version", "some version"}
                    }
                }).Build())
            .BuildServiceProvider();

        provider
            .GetRequiredService<JsonExceptionHandlerMiddleware>()
            .ShouldNotBeNull();

        provider
            .GetRequiredService<RequestLogContextMiddleware>()
            .ShouldNotBeNull();

        provider
            .GetRequiredService<ServiceDescription>()
            .ShouldBeEquivalentTo(new ServiceDescription
            {
                Name = "some name",
                Version = "some version"
            });

        provider
            .GetRequiredService<ServiceDescriptionMiddleware>()
            .ShouldNotBeNull();

        provider
            .GetRequiredService<ICorrelationContextAccessor>()
            .ShouldBeOfType<CorrelationContextAccessor>();

        provider
            .GetRequiredService<ICorrelationContextFactory>()
            .ShouldBeOfType<CorrelationContextFactory>();

        provider
            .GetRequiredService<ICorrelationIdProvider>()
            .ShouldBeOfType<GuidCorrelationIdProvider>();

        provider
            .GetRequiredService<RoutingConfiguration>()
            .ShouldBeEquivalentTo(new RoutingConfiguration
            {
                PathBase = "some path"
            });
    }

    [Test]
    public void AddFromAppConfiguration_Class_ShouldAddSingletonInstanceFromAppConfiguration() =>
        _sut
            .AddFromAppConfiguration<ServiceDescription>(new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource
                {
                    InitialData = new Dictionary<string, string>
                    {
                        {"ServiceDescription:Name", "some name"},
                        {"ServiceDescription:Version", "some version"}
                    }
                }).Build())
            .BuildServiceProvider()
            .GetRequiredService<ServiceDescription>()
            .ShouldBeEquivalentTo(new ServiceDescription
            {
                Name = "some name",
                Version = "some version",
            });

    [Test]
    public void AddJsonSerializationConfiguration_ShouldApplyJsonSerializationConfiguration()
    {
        var provider = _sut
           .AddDefaultComponents(new ConfigurationBuilder()
               .Add(new MemoryConfigurationSource
               {
                   InitialData = new Dictionary<string, string>
                   {
                        {"ServiceDescription:Name", "some name"},
                        {"ServiceDescription:Version", "some version"}
                   }
               }).Build())
           .BuildServiceProvider();

        var jsonOptions = provider.GetRequiredService<IOptions<JsonOptions>>().Value;

        var converters = jsonOptions.SerializerOptions.Converters;

        converters.ShouldContain(c => c.GetType() == typeof(JsonStringEnumConverter));
    }
}
