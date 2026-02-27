using LanguageExt;
using Microsoft.AspNetCore.Http;
using Error = LanguageExt.Common.Error;

namespace Fl.Shared.Utils.Mappers;
public interface IApiResultMapper<T> :
    IMapper<Option<T>, IResult>,
    IMapper<Error, IResult>
{ }
