using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

using System.Text.Json.Serialization;

namespace Fl.Shared.Utils.Components.Web.Extensions.ServiceCollection;

public static class JsonSerializationServiceCollectionExtensions
{

    public static IServiceCollection AddJsonSerializationConfiguration(this IServiceCollection services) =>
         services.Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

}
