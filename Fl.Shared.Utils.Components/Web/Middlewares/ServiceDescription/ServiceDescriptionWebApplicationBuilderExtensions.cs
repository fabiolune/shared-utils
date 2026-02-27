using Fl.Functional.Utils;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Fl.Shared.Utils.Components.Web.Middlewares.ServiceDescription;

[ExcludeFromCodeCoverage(Justification = "static extension methods")]
public static class ServiceDescriptionWebApplicationBuilderExtensions
{
    private const string DefaultServiceDescriptionFile = "servicedescription.json";

    public static WebApplicationBuilder AddServiceDescriptionConfiguration(this WebApplicationBuilder builder, string jsonFile) =>
        builder
            .Tee(b => b.Configuration.AddJsonFile(jsonFile));

    public static WebApplicationBuilder AddServiceDescriptionConfiguration(this WebApplicationBuilder builder) =>
        builder
            .Tee(b => b.Configuration.AddJsonFile(DefaultServiceDescriptionFile));
}
