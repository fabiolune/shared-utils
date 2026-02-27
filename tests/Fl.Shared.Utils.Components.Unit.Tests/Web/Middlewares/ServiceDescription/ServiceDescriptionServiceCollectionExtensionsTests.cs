using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using Fl.Shared.Utils.Components.Web.Middlewares.ServiceDescription;

namespace Fl.Shared.Utils.Components.Unit.Tests.Web.Middlewares.ServiceDescription;

public class ServiceDescriptionServiceCollectionExtensionsTests
{
    [Test]
    public void AddServiceDescription_ShouldAddMiddleware()
    {
        var services = Substitute.For<IServiceCollection>();

        services
            .AddServiceDescription(new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource
                {
                    InitialData = new Dictionary<string, string>
                        {
                            {"ServiceDescription:Name", "some name"},
                            {"ServiceDescription:Version", "some version"}
                        }
                }
                )
                .Build()
            );

        services
            .Received(1)
            .Add(Arg.Is<ServiceDescriptor>(d => d.ServiceType == typeof(ServiceDescriptionMiddleware)));

        services
            .Received(1)
            .Add(Arg.Is<ServiceDescriptor>(d =>
                d.ServiceType == typeof(Components.Web.Models.ServiceDescription)
                    && ((Components.Web.Models.ServiceDescription)d.ImplementationInstance).Name == "some name"
                    && ((Components.Web.Models.ServiceDescription)d.ImplementationInstance).Version == "some version"
                ));
    }
}
