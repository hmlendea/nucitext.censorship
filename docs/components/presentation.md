# Presentation layer

## Overview

NuciText.Censorship is a library, not an application. It has no presentation layer, no user interface, no HTTP endpoints, no controllers, and no API surface beyond the `ITextCensor` interface.

## Public API surface

The entire public API consists of a single interface:

```csharp
namespace NuciText.Censorship.English
{
    public interface ITextCensor
    {
        string Censor(string text);
    }
}
```

| Member | Signature | Description |
|--------|-----------|-------------|
| `Censor` | `string Censor(string text)` | Censors the given text by replacing bad words and URLs with asterisks. |

## No API controllers

There are no API controllers, no HTTP routes, and no REST endpoints. The `api-reference/` directory is therefore empty of controller files. The sole API reference is the interface itself, documented here.

## Consumer usage

Consumers inject `ITextCensor` and call `Censor`:

```csharp
public sealed class MessageService
{
    readonly ITextCensor textCensor;

    public MessageService(ITextCensor textCensor)
    {
        this.textCensor = textCensor;
    }

    public string SanitiseMessage(string message)
        => textCensor.Censor(message);
}
```

## Nullability

The project has `<Nullable>enable</Nullable>`. The `Censor` method accepts a `string` parameter (not `string?`), indicating that null input is not a supported contract. Concrete implementations may throw `ArgumentNullException` for null input.
