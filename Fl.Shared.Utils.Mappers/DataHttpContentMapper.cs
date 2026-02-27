using Fl.Functional.Utils;
using LanguageExt;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http;
using Fl.Shared.Utils.Components.Web.Models;
using static Microsoft.AspNetCore.Http.Results;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Error = LanguageExt.Common.Error;

namespace Fl.Shared.Utils.Mappers;
public class DataHttpContentMapper<T> : IApiResultMapper<T>
{
    private readonly IResult _notFound = CreateByCode(Status404NotFound);
    private readonly IResult _serverError = CreateByCode(Status500InternalServerError);

    private static IResult CreateByCode(int statusCode) =>
        statusCode
            .Map(
                c => Json(new ErrorResponse(ReasonPhrases.GetReasonPhrase(c)),
                    statusCode: c));

    public IResult Map(Option<T> item) =>
        item
            .Match(t => Json(t, statusCode: Status200OK), _notFound);

    public IResult Map(Error item) => _serverError;
}
