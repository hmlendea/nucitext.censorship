[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/fund.html)
[![Build Status](https://github.com/hmlendea/nucitext.censorship/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nucitext.censorship/actions/workflows/dotnet.yml)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/nucitext.censorship)](https://github.com/hmlendea/nucitext.censorship/releases/latest)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://gnu.org/licenses/gpl-3.0)

# NuciText.Censorship

## About

NuciText.Censorship is a .NET abstraction package for text censorship.

It provides:
- A common `ITextCensor` interface for filtering offensive words and URLs
- A standardised contract for use with dependency injection
- A foundation for language-specific censorship implementations

## Installation

[![Get it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciText.Censorship)

### .NET CLI
```bash
dotnet add package NuciText.Censorship
```

### Package Manager
```powershell
Install-Package NuciText.Censorship
```

## Usage

Register a concrete `ITextCensor` implementation via dependency injection, then inject and use it where necessary:

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

The `Censor` method replaces offensive words and URLs with asterisks.

## Target Framework

The current package targets `.NET 10.0`.

## License

This project is licensed under the `GNU General Public License v3.0` or later. See [LICENSE](./LICENSE) for details.
