# Design decisions

## Overview

This document records key architectural and design decisions for NuciText.Censorship, along with their rationale.

## Decision log

| # | Decision | Status | Date |
|---|----------|--------|------|
| 1 | Interface-only package (no implementation) | Accepted | 2026 |
| 2 | No DI registration helpers | Accepted | 2026 |
| 3 | No configuration schema | Accepted | 2026 |
| 4 | No logging or telemetry | Accepted | 2026 |
| 5 | Target .NET 10.0 | Accepted | 2026 |
| 6 | `ImplicitUsings` disabled | Accepted | 2026 |
| 7 | `Nullable` enabled | Accepted | 2026 |
| 8 | `GenerateDocumentationFile` enabled | Accepted | 2026 |
| 9 | `GeneratePackageOnBuild` disabled | Accepted | 2026 |
| 10 | Namespace `NuciText.Censorship.English` | Accepted | 2026 |

## Detailed decisions

### 1. Interface-only package

**Context:** The package needs to provide a stable contract for text censorship that can be implemented by multiple providers.

**Decision:** The package contains only the `ITextCensor` interface. No concrete implementation is included.

**Rationale:** This follows the "abstraction package" pattern, allowing multiple implementations (language-specific, domain-specific) to coexist without consumers coupling to a particular one. It also keeps the package small and dependency-free.

**Consequences:** Consumers must find and register a concrete implementation. The package itself cannot be used standalone.

### 2. No DI registration helpers

**Context:** Many .NET libraries provide `AddXxx()` extension methods for DI registration.

**Decision:** The package does not provide any DI registration helpers.

**Rationale:** Avoids coupling to a specific DI framework or container. Keeps the package dependency-free. Makes composition explicit.

**Consequences:** Host applications must manually register concrete implementations.

### 3. No configuration schema

**Context:** Some libraries provide `IOptions<T>` patterns for configuration.

**Decision:** The package has no configuration options.

**Rationale:** Configuration is implementation-specific (word lists, replacement characters, etc.). The abstraction should not impose a schema that may not apply to all implementations.

**Consequences:** All configuration is managed by the host application and the concrete implementation.

### 4. No logging or telemetry

**Context:** Libraries often include logging for diagnostics.

**Decision:** The package does not log, persist, or transmit any data.

**Rationale:** Preserves privacy and minimises the trust boundary. The package processes no data of its own — it is a passive contract.

**Consequences:** No diagnostic information is available from the package itself. Debugging must be done in the concrete implementation.

### 5. Target .NET 10.0

**Context:** The package needs to target a .NET version.

**Decision:** Target `net10.0`.

**Rationale:** Aligns with the latest .NET runtime. Ensures access to the latest BCL features.

**Consequences:** Consumers must target .NET 10.0 or later.

### 6. `ImplicitUsings` disabled

**Context:** .NET 6+ supports implicit using directives.

**Decision:** `ImplicitUsings` is set to `disable`.

**Rationale:** Explicit `using` directives make the code self-documenting and avoid ambiguity.

**Consequences:** All `using` directives must be explicitly declared.

### 7. `Nullable` enabled

**Context:** Nullable reference types help prevent null-related bugs.

**Decision:** `Nullable` is set to `enable`.

**Rationale:** Enforces null-safety at the interface boundary. The `Censor` method accepts a non-null `string`, signalling that null input is not a supported contract.

**Consequences:** Consumers must handle null input before calling `Censor`.

### 8. `GenerateDocumentationFile` enabled

**Context:** XML documentation files provide IntelliSense support for consumers.

**Decision:** `GenerateDocumentationFile` is set to `true`.

**Rationale:** Ensures the public API is documented and discoverable.

**Consequences:** The XML documentation file is generated alongside the NuGet package.

### 9. `GeneratePackageOnBuild` disabled

**Context:** Some projects auto-generate NuGet packages on build.

**Decision:** `GeneratePackageOnBuild` is set to `false`.

**Rationale:** Prevents accidental package generation during development. Packages are generated explicitly via `dotnet pack`.

**Consequences:** `dotnet pack` must be run explicitly to produce a NuGet package.

### 10. Namespace `NuciText.Censorship.English`

**Context:** The interface is declared in the `NuciText.Censorship.English` namespace, but the package is named `NuciText.Censorship`.

**Decision:** Keep the namespace as `NuciText.Censorship.English`.

**Rationale:** The canonical implementation is English-focused. The namespace signals the intended primary use case.

**Consequences:** May cause confusion for implementers of other languages. Documented as an open question in [Ambiguities and open questions](./ambiguities-and-open-questions.md).
