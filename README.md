[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/funding)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/nucitext.censorship)](https://github.com/hmlendea/nucitext.censorship/releases/latest)
[![Build Status](https://github.com/hmlendea/nucitext.censorship/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nucitext.censorship/actions/workflows/dotnet.yml)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://gnu.org/licenses/gpl-3.0)

# NuciText.Censorship

A .NET abstraction package for text censorship, providing a common interface and standardised contract for filtering offensive words and URLs.

## ✨ Features

- Common `ITextCensor` interface for filtering offensive words and URLs
- Standardised contract for use with dependency injection
- Foundation for language-specific censorship implementations

## 🚀 Usage

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

## 📦 Installation

[![Get it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciText.Censorship)

### .NET CLI

```bash
dotnet add package NuciText.Censorship
```

### Package Manager Console

```powershell
Install-Package NuciText.Censorship
```

## 🛠️ Development

### Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

All NuGet dependencies are restored automatically by `dotnet restore`.

### Build

```bash
dotnet build NuciText.Censorship
```

## 🤝 Contributing

Contributions are welcome. Please:
- Keep changes cross-platform
- Keep the existing public contract intact unless a breaking change is intentional
- Keep pull requests focused and consistent with the existing code style
- Update documentation when behaviour changes

## 💬 Support

Found a bug or have a suggestion? [Open an issue](https://github.com/hmlendea/nucitext.censorship/issues)!

If you find this project useful, consider [funding it](https://hmlendea.go.ro/funding) or giving a ⭐️ on GitHub!

## 📄 License

This project is licensed under the `GNU General Public License v3.0` or later. See [LICENSE](./LICENSE) for details.
