# Invariants

## System-wide invariants

This document records the invariants that must remain true for the NuciText.Censorship package to function correctly.

## Interface invariants

| # | Invariant | Verification |
|---|-----------|--------------|
| 1 | The `ITextCensor` interface must declare exactly one method: `string Censor(string text)`. | Compile-time; XML doc generation. |
| 2 | The `ITextCensor` interface must be in the `NuciText.Censorship.English` namespace. | Compile-time. |
| 3 | The `Censor` method must accept a non-null `string` and return a non-null `string`. | Compile-time (nullable reference types). |
| 4 | The `ITextCensor` interface must not declare any exceptions in its signature. | Compile-time. |

## Package invariants

| # | Invariant | Verification |
|---|-----------|--------------|
| 5 | The package must have zero NuGet package dependencies. | `dotnet build` / `dotnet pack`. |
| 6 | The package must target `net10.0`. | `dotnet build`. |
| 7 | The package must not contain any implementation code (only interfaces). | Code review; `dotnet build`. |
| 8 | The package must not contain any tests. | Repository inspection. |
| 9 | The package must not contain any logging, persistence, or telemetry code. | Code review. |
| 10 | The package must not contain any configuration schema. | Code review. |
| 11 | The package must not contain any DI registration helpers. | Code review. |

## Dependency invariants

| # | Invariant | Verification |
|---|-----------|--------------|
| 12 | Consumers depend only on `ITextCensor`. | Architecture review. |
| 13 | Implementations depend only on `ITextCensor`. | Architecture review. |
| 14 | The package has zero dependencies on implementations or consumers. | Architecture review. |
| 15 | No circular dependencies are possible by design. | Architecture review. |

## Versioning invariants

| # | Invariant | Verification |
|---|-----------|--------------|
| 16 | Any change to the `ITextCensor` interface signature requires a major version bump. | Release process. |
| 17 | The package version must follow semantic versioning. | Release process. |
| 18 | `GenerateDocumentationFile` must remain `true`. | Build verification. |

## Documentation invariants

| # | Invariant | Verification |
|---|-----------|--------------|
| 19 | `docs/INDEX.md` must link to every file under `docs/`. | Documentation review. |
| 20 | Documentation must not duplicate root `ARCHITECTURE.md`, `SECURITY.md`, `PRIVACY.md`, or `LICENSE`. | Documentation review. |
