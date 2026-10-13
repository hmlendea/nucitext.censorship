# API reference

## Overview

NuciText.Censorship exposes a single public interface: `ITextCensor`. There are no API controllers, no HTTP endpoints, and no REST routes. The entire API surface is a .NET interface consumed via dependency injection.

## Public API

| Symbol | Type | Namespace | File |
|--------|------|-----------|------|
| `ITextCensor` | Interface | `NuciText.Censorship.English` | [`ITextCensor.cs`](../../NuciText.Censorship/ITextCensor.cs) |

## Interface definition

```csharp
namespace NuciText.Censorship.English
{
    /// <summary>
    /// Filters text by censoring offensive words and URLs.
    /// </summary>
    public interface ITextCensor
    {
        /// <summary>
        /// Censors the given text by replacing bad words and URLs with asterisks.
        /// </summary>
        /// <param name="text">The text to censor.</param>
        /// <returns>The censored text.</returns>
        string Censor(string text);
    }
}
```

## Method reference

### `ITextCensor.Censor`

| Property | Value |
|----------|-------|
| **Signature** | `string Censor(string text)` |
| **Parameters** | `text` (`string`) — The text to censor. Must not be null. |
| **Returns** | `string` — The censored text, with offensive words and URLs replaced by asterisks. |
| **Exceptions** | Concrete implementations may throw `ArgumentNullException` for null input. |
| **Thread safety** | Depends on the concrete implementation. |
| **Idempotency** | Depends on the concrete implementation. |

## Authentication and authorisation

Not applicable. This is a library interface, not a network endpoint. Authentication and authorisation are handled by the host application.

## Versioning

The package follows semantic versioning. The `ITextCensor` interface signature is a compatibility contract: any change to the method signature requires a major version bump. See [Compatibility contracts](../ARCHITECTURE.md#-compatibility-contracts) in the root architecture document.

## Related documentation

- [Repository overview](../repository-overview.md)
- [Architecture](../architecture.md)
- [Host and composition](../components/host-and-composition.md)
- [Presentation layer](../components/presentation.md)
- [Integration models](../components/integration-models.md)
