[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/funding)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/nucitext.censorship)](https://github.com/hmlendea/nucitext.censorship/releases/latest)
[![Build Status](https://github.com/hmlendea/nucitext.censorship/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nucitext.censorship/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/NuciText.Censorship)](https://nuget.org/packages/NuciText.Censorship)
[![License](https://img.shields.io/github/license/hmlendea/nucitext.censorship)](https://github.com/hmlendea/nucitext.censorship/blob/master/LICENSE)

# NuciText.Censorship

A .NET abstraction package for text censorship, providing a common interface and standardised contract for filtering offensive words and URLs.

## 📑 Table of Contents

- [Capabilities](#-capabilities)
- [Usage](#-usage)
- [Installation](#-installation)
  - [Package Manager Installation](#package-manager-installation)
- [Development](#-development)
  - [Requirements](#requirements)
  - [Build](#build)
- [Project Structure](#-project-structure)
  - [Projects and Packages](#projects-and-packages)
  - [Directories](#directories)
- [Architecture](#-architecture)
- [Documentation](#-documentation)
- [GitHub Actions](#-github-actions)
- [Versioning](#-versioning)
- [Security](#-security)
- [Contributing](#-contributing)
- [Project Engagement](#-project-engagement)
- [License](#-license)

## ✨ Capabilities

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

[![Obtain it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciText.Censorship)

### Package Manager Installation

```bash
dotnet add package NuciText.Censorship
```

Or, via the `Package Manager Console`:
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

## 🗂️ Project Structure

This repository contains a single .NET library project.

### Projects and Packages

| Project | Type | Purpose |
|---------|------|---------|
| `NuciText.Censorship/` | Library | Defines the `ITextCensor` interface for text censorship |

### Directories

| Directory | Purpose |
|-----------|---------|
| `NuciText.Censorship/` | Source code and project file |
| `docs/` | Generated documentation |
| `.github/workflows/` | CI/CD workflows |

## 🏗️ Architecture

NuciText.Censorship is an interface-only abstraction package following the "abstraction package" pattern. It defines a stable `ITextCensor` contract that decouples consumers from concrete censorship implementations.

See the [architecture documentation](./ARCHITECTURE.md) for the system context, principal components, runtime flows, ownership boundaries, dependencies, constraints, and extension points.

## 📚 Documentation

Full documentation is available in the `docs/` directory.

| Resource | Description |
|----------|-------------|
| [Repository Overview](./docs/repository-overview.md) | Purpose, scope, entry points, and capabilities |
| [Architecture](./docs/architecture.md) | Architectural style, components, and design rationale |
| [API Reference](./docs/api-reference/INDEX.md) | Interface and method documentation |
| [API Usage Examples](./docs/api-usage-examples.md) | Registration and usage examples |
| [Build and Deployment](./docs/build-and-deployment.md) | CI/CD pipeline and packaging |
| [Configuration](./docs/configuration.md) | Build-time and runtime configuration |
| [Testing](./docs/testing.md) | Test strategy and verification |

## ⚙️ GitHub Actions

| Workflow | Purpose | What it does |
|----------|---------|--------------|
| [dotnet.yml](https://github.com/hmlendea/nucitext.censorship/blob/master/.github/workflows/dotnet.yml) | Continuous integration | Restores, builds, and tests on every push and PR to master |

## 🏷️ Versioning

This project follows [Semantic Versioning](https://semver.org/). The version is declared in `NuciText.Censorship.csproj` and updated on release. Breaking changes to the `ITextCensor` interface require a major version bump.

## 🔒 Security

NuciText.Censorship is an interface-only package with no runtime behaviour. It processes no data, logs nothing, and has zero external dependencies.

For information on reporting security vulnerabilities, see [SECURITY.md](./SECURITY.md).

## 🤝 Contributing

Contributions are welcome. Please:
- Keep changes cross-platform
- Keep the existing public contract intact unless a breaking change is intentional
- Keep pull requests focused and consistent with the existing code style
- Update documentation when behaviour changes

## 💝 Project Engagement

Found a bug or have a suggestion? [Open an issue](https://github.com/hmlendea/nucitext.censorship/issues)!

If you find this project useful, consider [funding it](https://hmlendea.go.ro/funding) or giving a ⭐️ on GitHub!

## 📄 License

This project is licensed under the `GNU General Public License v3.0` or later. See [LICENSE](./LICENSE) for details.
