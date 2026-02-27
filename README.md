> This documentation is in line with the active development, hence should be considered work in progress. To check the documentation for the latest stable version please visit [https://fabiolune.github.io/shared-utils/](https://fabiolune.github.io/shared-utils/)

# shared-utils

A collection of .NET libraries that provide shared building blocks for web applications. The solution follows a functional programming style built on top of [LanguageExt](https://github.com/louthy/language-ext) and targets `net8.0`, `net9.0` and `net10.0`.

---

## Solution structure

```text
shared-utils/
├── Fl.Shared.Utils.Components.Web.Models/   # Shared model records (DTOs)
├── Fl.Shared.Utils.Components/              # Logging & web middleware components
├── Fl.Shared.Utils.Mappers/                 # Mapper abstractions and implementations
└── tests/
    ├── Fl.Shared.Utils.Components.Unit.Tests/
    └── Fl.Shared.Utils.Mappers.Tests/
```

---

## Projects

### [`Fl.Shared.Utils.Components.Web.Models`](Fl.Shared.Utils.Components.Web.Models/README.md)

Lightweight model library with no external dependencies. Defines the shared records used by the other projects:

| Type | Description |
| ------ | ------------- |
| `ErrorResponse` | JSON error payload (`{ "message": "..." }`) |
| `RoutingConfiguration` | Optional URL base-path configuration |
| `ServiceDescription` | Service name and version metadata |

---

### [`Fl.Shared.Utils.Components`](Fl.Shared.Utils.Components/README.md)

The main component library. Provides reusable infrastructure for logging and ASP.NET Core web pipelines.

### Logging

| Component | Description |
| ----------- | ------------- |
| `SerilogConfiguration` | Configuration record for Serilog (level, overrides, environment, system, customer) |
| `LoggerConfigurationExtensions` | Default Serilog setup with CompactJson console output |
| `HostBuilderExtensions` | `AddSerilogLogging` on `IHostBuilder` |
| `LoggingWebApplicationBuilderExtensions` | `AddSerilogLogging` on `WebApplicationBuilder` |
| `EitherLoggingExtensions` | `TeeLog` on `Either<Error, T>` / `EitherAsync<Error, T>` |
| `ErrorExtensions` | Convert a `LanguageExt.Common.Error` to a logging `Action` |

### Web middleware

| Middleware | Description |
| ------------ | ------------- |
| `JsonExceptionHandlerMiddleware` | Catches unhandled exceptions → HTTP 500 JSON |
| `RequestLogContextMiddleware` | Enriches Serilog context with `CorrelationId` |
| `TraceLogContextMiddleware` | Enriches Serilog context with `SpanId` / `TraceId` |
| `ServiceDescriptionMiddleware` | Serves service metadata at `GET /internal/description` |

### Key extension methods

| Method | Description |
| -------- | ------------- |
| `services.AddDefaultComponents(config)` | Registers all default middleware and services |
| `app.UseDefaultMiddlewares()` | Wires up the default middleware pipeline |
| `services.AddEndpointDefinitions(...)` | Scans assemblies for `IEndpoint` implementations |
| `app.UseEndpointDefinitions()` | Calls `DefineEndpoints` on all registered `IEndpoint`s |
| `app.UseRoutingBasePath()` | Applies the configured URL base path |

---

### [`Fl.Shared.Utils.Mappers`](Fl.Shared.Utils.Mappers/README.md)

Mapper abstractions and a default HTTP-result implementation.

| Type | Description |
| ------ | ------------- |
| `IMapper<T1, T2>` | Generic single-direction mapping contract |
| `IApiResultMapper<T>` | Maps `Option<T>` and `Error` to ASP.NET Core `IResult` |
| `DataHttpContentMapper<T>` | Default implementation: `Some` → 200, `None` → 404, `Error` → 500 |

---

## Quality

| Concern | Tooling | Target |
| --------- | --------- | -------- |
| Unit testing | [NUnit](https://nunit.org/) + [NSubstitute](https://nsubstitute.github.io/) + [Shouldly](https://docs.shouldly.org/) | — |
| Code coverage | [Coverlet](https://github.com/coverlet-coverage/coverlet) + [Codecov](https://codecov.io/) | 100 % |
| Mutation testing | [Stryker.NET](https://stryker-mutator.io/) (advanced level) | — |
| Versioning | [GitVersion](https://gitversion.net/) — ContinuousDelivery on `main` | — |

---

## Key dependencies

| Package | Purpose |
| --------- | --------- |
| [LanguageExt](https://github.com/louthy/language-ext) | Functional types (`Either`, `Option`, `Error`) |
| [Serilog](https://serilog.net/) | Structured logging |
| [CorrelationId](https://github.com/stevejgordon/CorrelationId) | HTTP correlation ID propagation |
| [Scrutor](https://github.com/khellang/Scrutor) | Assembly scanning for `IEndpoint` |
| `Fl.Functional.Utils` | Internal functional utilities (Tee, Map, MakeOption, …) |
| `Fl.Configuration.Extensions` | Strongly-typed configuration helpers |
