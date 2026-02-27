using Microsoft.AspNetCore.Builder;

namespace Fl.Shared.Utils.Components.Web.Endpoints;

public interface IEndpoint
{
    WebApplication DefineEndpoints(WebApplication app);
}