# Quick start

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

## Installation

```bash
dotnet add package NuciText.Censorship
```

## Usage

1. Install a concrete `ITextCensor` implementation (e.g., `NuciText.Censorship.English`).
2. Register it via dependency injection.
3. Inject and call `Censor`.

```csharp
using NuciText.Censorship.English;

// Register in DI container:
services.AddSingleton<ITextCensor, EnglishTextCensor>();

// Inject and use:
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

## Build from source

```bash
dotnet build NuciText.Censorship
```

## Next steps

- [Repository overview](./repository-overview.md)
- [Architecture](./architecture.md)
- [API reference](./api-reference/INDEX.md)
- [API usage examples](./api-usage-examples.md)
