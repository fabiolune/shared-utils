# Fl.Shared.Utils.Mappers

Shared mapper library for .NET web applications. Provides generic mapping abstractions and a ready-made implementation that converts functional types (`Option`, `Error`) into ASP.NET Core `IResult` HTTP responses.

---

## Project structure

```text
Fl.Shared.Utils.Mappers/
├── IMapper.cs
├── IApiResultMapper.cs
└── DataHttpContentMapper.cs
```

---

## Interfaces

### `IMapper<T1, T2>`

Generic mapping contract between two types.

```csharp
public interface IMapper<in T1, out T2>
{
    T2 Map(T1 item);
}
```

Use this interface to define any single-direction mapping between an input type `T1` and an output type `T2`.

---

### `IApiResultMapper<T>`

Specialisation of `IMapper` for mapping API results to ASP.NET Core `IResult`. Combines two mapping contracts:

- `IMapper<Option<T>, IResult>` — maps an optional value to an HTTP response
- `IMapper<Error, IResult>` — maps a functional error to an HTTP response

```csharp
public interface IApiResultMapper<T> :
    IMapper<Option<T>, IResult>,
    IMapper<Error, IResult>
{ }
```

---

## Implementations

### `DataHttpContentMapper<T>`

Default implementation of `IApiResultMapper<T>`. Maps `Option<T>` and `Error` values to JSON `IResult` responses with appropriate HTTP status codes.

| Input | HTTP status | Response body |
| ------- | ------------- | --------------- |
| `Option<T>.Some(value)` | `200 OK` | JSON-serialised `value` |
| `Option<T>.None` | `404 Not Found` | `{ "message": "Not Found" }` |
| `Error` | `500 Internal Server Error` | `{ "message": "Internal Server Error" }` |

Error responses use `ErrorResponse` (from `Fl.Shared.Utils.Components.Web.Models`) with the reason phrase derived from the HTTP status code.

#### Usage

```csharp
// Register in DI
services.AddSingleton<IApiResultMapper<MyDto>, DataHttpContentMapper<MyDto>>();

// Use in a minimal API endpoint
app.MapGet("/items/{id}", async (int id, IApiResultMapper<MyDto> mapper, IMyService service) =>
{
    Option<MyDto> result = await service.GetById(id);
    return mapper.Map(result);
});
```

---

## Tests

Unit tests are located in `tests/Fl.Shared.Utils.Mappers.Tests` and cover:

| Class under test | Scenarios |
| ----------------- | ----------- |
| `DataHttpContentMapper<T>` | `Error` input → 500 JSON with `"Internal Server Error"` |
| `DataHttpContentMapper<T>` | `Option.None` input → 404 JSON with `"Not Found"` |
| `DataHttpContentMapper<T>` | `Option.Some(value)` input → 200 JSON with the original value |

---

## Main dependencies

- [LanguageExt](https://github.com/louthy/language-ext) — functional types (`Option`, `Error`)
- `Microsoft.AspNetCore.App` — `IResult`, `Results`, `StatusCodes`
- `Fl.Functional.Utils` — functional utilities (Map, etc.)
- `Fl.Shared.Utils.Components.Web.Models` — `ErrorResponse`
