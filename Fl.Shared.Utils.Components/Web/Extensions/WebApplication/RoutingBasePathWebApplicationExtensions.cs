using Fl.Functional.Utils;
using Microsoft.Extensions.DependencyInjection;
using WebApp = Microsoft.AspNetCore.Builder.WebApplication;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;
using Fl.Shared.Utils.Components.Web.Models;

namespace Fl.Shared.Utils.Components.Web.Extensions.WebApplication;

[ExcludeFromCodeCoverage(Justification = "untestable sealed class WebApplication")]
public static class RoutingBasePathWebApplicationExtensions
{
    private const char Slash = '/';

    public static WebApp UseRoutingBasePath(this WebApp app) =>
        app
            .Tee(a =>
                (a.Services, App: a)
                .Map(_ => (Config: _.Services.GetRequiredService<RoutingConfiguration>(), _.App))
                .MakeOption(_ => !_.Config.PathBase.StartsWith(Slash))
                .Map(_ => (_.Config.PathBase, _.App))
                .IfSome(_ => _.App.UsePathBase(_.PathBase).UseRouting()));
}
