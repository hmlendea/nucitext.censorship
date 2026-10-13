# API usage examples

## Overview

NuciText.Censorship is consumed via dependency injection. The host application registers a concrete `ITextCensor` implementation and injects it where needed.

## Registration

```csharp
using NuciText.Censorship.English;

// In the host application's DI configuration:
// (Assuming a concrete implementation exists in a separate package)
services.AddSingleton<ITextCensor, EnglishTextCensor>();
```

## Usage

```csharp
using NuciText.Censorship.English;

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

## Example call

```csharp
// Input
string message = "This is a bad word and a URL: https://example.com";

// Output (censored)
string censored = textCensor.Censor(message);
// Result: "This is a **** word and a URL: ***************"
```

> **Note:** The exact censoring behaviour (which words are flagged, how URLs are detected, replacement strategy) is determined by the concrete implementation, not by this abstraction package.

## Null handling

```csharp
// The Censor method accepts a non-null string.
// Concrete implementations may throw ArgumentNullException for null input:
try
{
    string result = textCensor.Censor(null!);
}
catch (ArgumentNullException ex)
{
    // Handle null input
}
```

## Related documentation

- [API reference](./api-reference/INDEX.md)
- [Host and composition](./components/host-and-composition.md)
- [Presentation layer](./components/presentation.md)
