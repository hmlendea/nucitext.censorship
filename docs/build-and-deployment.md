# Build and deployment

## Build

### Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

### Build command

```bash
dotnet build NuciText.Censorship
```

### Restore

```bash
dotnet restore
```

## CI/CD

The repository uses GitHub Actions for continuous integration. The workflow is defined in [`.github/workflows/dotnet.yml`](../../.github/workflows/dotnet.yml).

### Workflow triggers

- Push to `master` branch.
- Pull request to `master` branch.

### Workflow steps

1. Checkout source code.
2. Setup .NET 10.0.x.
3. Restore dependencies (`dotnet restore`).
4. Build (`dotnet build --no-restore`).
5. Test (`dotnet test --no-build --verbosity normal`).

### Build status

[![Build Status](https://github.com/hmlendea/nucitext.censorship/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nucitext.censorship/actions/workflows/dotnet.yml)

## Deployment

### Distribution

The package is distributed via NuGet as `NuciText.Censorship`.

### Versioning

- Semantic versioning.
- Version is declared in `NuciText.Censorship.csproj` as `<Version>1.0.0</Version>`.
- `GenerateDocumentationFile=true` ensures XML documentation is generated.

### Packaging

```bash
dotnet pack NuciText.Censorship
```

> **Note:** `GeneratePackageOnBuild` is set to `false`, so `dotnet pack` must be run explicitly to produce a NuGet package.

## Target framework

- `net10.0` — Requires .NET 10.0 or later runtime.

## Environments

There are no deployment environments. The package is a library consumed by host applications. There is no staging, production, or hosting infrastructure managed by this repository.
