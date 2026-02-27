using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Shouldly;
using Fl.Shared.Utils.Components.Web.Extensions.ServiceCollection;
using System.Text.Json.Serialization;

namespace Fl.Shared.Utils.Components.Unit.Tests.Web.Extensions.ServiceCollection;

internal class JsonSerializationServiceCollectionExtensionsTests
{
    private Microsoft.Extensions.DependencyInjection.ServiceCollection _sut;

    [SetUp]
    public void SetUp() => _sut = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

    [Test]
    public void AddJsonSerializationConfiguration_ShouldApplyJsonSerializationConfiguration() =>
        _sut
            .AddJsonSerializationConfiguration()
            .BuildServiceProvider()
            .GetRequiredService<IOptions<JsonOptions>>()
            .Value
            .SerializerOptions
            .Converters
            .ShouldContain(c => c.GetType() == typeof(JsonStringEnumConverter));
}