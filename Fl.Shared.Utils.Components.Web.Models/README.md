# Fl.Shared.Utils.Components.Web.Models

Shared model library for .NET web applications. Contains the data transfer objects and configuration records used across `Fl.Shared.Utils.Components` and `Fl.Shared.Utils.Mappers`.

---

## Project structure

```text
Fl.Shared.Utils.Components.Web.Models/
├── ErrorResponse.cs
├── RoutingConfiguration.cs
└── ServiceDescription.cs
```

---

## Models

### `ErrorResponse`

Immutable record representing a JSON error response body.

```csharp
public record ErrorResponse(string Message);
```

| Property | Type | Description |
| ---------- | ------ | ------------- |
| `Message` | `string` | Human-readable error description |

Serialises to:

```json
{
  "message": "Something went wrong"
}
```

Used by `JsonExceptionHandlerMiddleware` and `DataHttpContentMapper<T>` to produce consistent error payloads.

---

### `RoutingConfiguration`

Configuration record for setting an optional URL base path for the application.

```csharp
public record RoutingConfiguration
{
    public static readonly RoutingConfiguration Default = new() { PathBase = string.Empty };

    public string PathBase { get; init; } = string.Empty;
}
```

| Property | Type | Default | Description |
| ---------- | ------ | --------- | ------------- |
| `PathBase` | `string` | `""` | Base path prefix for all routes (e.g. `/api/v1`) |

`RoutingConfiguration.Default` is used as a fallback when no configuration entry is present. Consumed by `RoutingBasePathServiceCollectionExtensions` and `RoutingBasePathWebApplicationExtensions` in `Fl.Shared.Utils.Components`.

Example `appsettings.json` entry:

```json
{
  "RoutingConfiguration": {
    "PathBase": "/api/v1"
  }
}
```

---

### `ServiceDescription`

Configuration record that describes a service, exposed at the `GET /internal/description` endpoint.

```csharp
public record ServiceDescription
{
    public string Name    { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
}
```

| Property | Type | Default | Description |
| ---------- | ------ | --------- | ------------- |
| `Name` | `string` | `""` | Service name |
| `Version` | `string` | `""` | Service version |

Serialises to:

```json
{
  "name": "my-service",
  "version": "1.0.0"
}
```

Populated from `servicedescription.json` via `AddServiceDescriptionConfiguration` and consumed by `ServiceDescriptionMiddleware`.

Example `servicedescription.json`:

```json
{
  "ServiceDescription": {
    "Name": "my-service",
    "Version": "1.0.0"
  }
}
```

---

## Dependencies

This project has no external package dependencies. It targets `net8.0`, `net9.0` and `net10.0`.
