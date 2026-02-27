using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using NUnit.Framework;
using Shouldly;
using Fl.Shared.Utils.Components.Web.Models;

namespace Fl.Shared.Utils.Mappers.Tests.Mappers;

public class DataHttpContentMapperTests
{
    private DataHttpContentMapper<object> _sut;

    [SetUp]
    public void Setup() => _sut = new DataHttpContentMapper<object>();

    [Test]
    public void Map_WhenInputIsLeft_ShouldReturn500()
    {
        var input = Error.New("whatever");

        var result = _sut.Map(input) as JsonHttpResult<ErrorResponse>;

        result
            .ShouldNotBeNull();
        result!
            .StatusCode
            .ShouldBe(500);
        result
            .Value
            .ShouldBeEquivalentTo(new ErrorResponse("Internal Server Error"));
    }

    [Test]
    public void Map_WhenInputIsNone_ShouldReturn404()
    {
        var input = Option<object>.None;

        var result = _sut.Map(input) as JsonHttpResult<ErrorResponse>;

        result
            .ShouldNotBeNull();
        result!
            .StatusCode
            .ShouldBe(404);
        result
            .Value
            .ShouldBeEquivalentTo(new ErrorResponse("Not Found"));
    }

    [Test]
    public void Map_WhenInputIsSomeShouldReturn200()
    {
        var content = new object();
        var input = Option<object>.Some(content);

        var result = _sut.Map(input) as JsonHttpResult<object>;

        result
            .ShouldNotBeNull();
        result!
            .StatusCode
            .ShouldBe(200);
        result
            .Value
            .ShouldBe(content);
    }
}