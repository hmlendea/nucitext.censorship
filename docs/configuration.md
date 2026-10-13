# Configuration

## Overview

NuciText.Censorship has no configuration. The package does not read configuration files, environment variables, or any external configuration source. All configuration is the responsibility of the concrete implementation.

## Build-time properties

The project file (`NuciText.Censorship.csproj`) defines the following build-time properties:

| Property | Value | Description |
|----------|-------|-------------|
| `TargetFramework` | `net10.0` | Target .NET runtime. |
| `RootNamespace` | `NuciText.Censorship` | Root namespace for the assembly. |
| `AssemblyName` | `NuciText.Censorship` | Name of the output assembly. |
| `Version` | `1.0.0` | NuGet package version. |
| `Description` | `Abstraction for the NuciText censorship libraries. Replaces bad words and URLs with asterisks.` | NuGet package description. |
| `Authors` | `Horațiu Mlendea` | NuGet package author. |
| `Copyright` | `Copyright 2026 © Horațiu Mlendea` | Copyright notice. |
| `RepositoryUrl` | `https://github.com/hmlendea/nucitext.censorship` | Source repository URL. |
| `RepositoryType` | `git` | Repository type. |
| `PackageLicenseExpression` | `GPL-3.0-or-later` | License expression. |
| `PackageTags` | `text;censorship;filter;nlp;nucitext` | NuGet search tags. |
| `IsPackable` | `true` | Whether the project produces a NuGet package. |
| `GeneratePackageOnBuild` | `false` | Whether to generate a NuGet package on build. |
| `GenerateDocumentationFile` | `true` | Whether to generate an XML documentation file. |
| `ImplicitUsings` | `disable` | Whether to use implicit using directives. |
| `Nullable` | `enable` | Whether nullable reference types are enabled. |

## Runtime configuration

None. The package has no runtime configuration.

## Implementation configuration

Concrete implementations (in separate packages) may have their own configuration:

- Word lists (bad words to censor).
- URL detection patterns.
- Replacement characters (e.g., asterisks).
- Case sensitivity settings.
- Minimum word length thresholds.

This configuration is managed by the host application and the implementation package, not by NuciText.Censorship.

## Configuration precedence

Not applicable — there is no configuration to precedence.
