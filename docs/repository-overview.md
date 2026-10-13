# Repository overview

## Purpose

NuciText.Censorship is a .NET abstraction package that defines a single, stable public contract — the `ITextCensor` interface — for text censorship. The package provides no implementation; it exists solely to decouple host applications and concrete censorship providers from one another. Language-specific or domain-specific implementations (for example `NuciText.Censorship.English`) are delivered as separate NuGet packages that reference this abstraction.

The intended consumers are:

- Host applications that need to censor text and want to depend on a stable abstraction rather than a concrete implementation.
- Implementers of censorship providers who need a well-defined contract to fulfil.

## Scope

The package owns:

- The `ITextCensor` interface definition and its XML documentation.
- The NuGet package metadata (version, description, tags, license expression).
- The target framework declaration (`net10.0`).

The package deliberately does **not** own:

- Any censorship logic, word lists, or replacement strategies.
- Any dependency-injection registration helpers or service-discovery mechanisms.
- Any configuration schema, logging, persistence, or telemetry.
- Any tests (the package is interface-only; verification is performed by consumers and implementers).

## Entry points

There is a single entry point: the `ITextCensor` interface, located at `NuciText.Censorship/ITextCensor.cs`.

```csharp
namespace NuciText.Censorship.English
{
    public interface ITextCensor
    {
        string Censor(string text);
    }
}
```

## Principal capabilities

| Capability | Description | Owner |
|------------|-------------|-------|
| Contract definition | Declares the `Censor(string) → string` method signature | `ITextCensor` |
| Package metadata | NuGet identity, versioning, target framework | `NuciText.Censorship.csproj` |
| Documentation generation | XML documentation file emitted at build time | `GenerateDocumentationFile=true` |

## Major state

The package is stateless. It contains no runtime state, no caches, no configuration, and no persisted data. All state is owned by the concrete implementation chosen by the host application.

## Major integrations

| Integration | Direction | Boundary |
|-------------|-----------|----------|
| Host application | Consumer | Depends on `ITextCensor` via DI |
| Concrete implementation | Provider | Implements `ITextCensor` |
| .NET 10.0 runtime | Platform | Base-class-library dependency |
| NuGet | Distribution | Package consumption and publishing |
| GitHub Actions | CI/CD | Build, restore, and test pipeline |

## System-wide invariants

1. The `ITextCensor` interface signature (`string Censor(string text)`) must not change without a major version bump.
2. The package must have zero runtime dependencies on any concrete implementation.
3. The package must not introduce logging, persistence, or telemetry.
4. The namespace `NuciText.Censorship.English` is the declared home of the interface; implementations must reference this namespace.
