# Ambiguities and open questions

## Overview

This document tracks unresolved items, TODOs, and known gaps in the NuciText.Censorship repository documentation and implementation.

## Known gaps

### 1. No concrete implementation in this repository

The repository contains only the `ITextCensor` interface. There is no reference implementation (e.g., `NuciText.Censorship.English`) in this repository. The README references `NuciText.Censorship.English` as an example, but it is not present.

**Status:** By design — implementations are separate packages.

### 2. No tests

The repository contains no test project. The `CONTRIBUTING.md` references `dotnet test`, but there are no tests to run.

**Status:** By design — the package is interface-only; verification is delegated to consumers and implementers.

### 3. Namespace mismatch

The `ITextCensor` interface is declared in the `NuciText.Censorship.English` namespace, but the package is named `NuciText.Censorship` (not `NuciText.Censorship.English`). This may cause confusion for implementers.

**Status:** Documented in [Architecture](./architecture.md) under "Namespace note."

### 4. No DI registration helper

The package does not provide an `AddNuciTextCensorship()` extension method or any DI registration helper. Host applications must manually register concrete implementations.

**Status:** By design — see [Host and composition](./components/host-and-composition.md).

### 5. No configuration schema

The package has no configuration options. All configuration is implementation-specific.

**Status:** By design — see [Configuration](./configuration.md).

## Open questions

| Question | Status | Notes |
|----------|--------|-------|
| Should the namespace be `NuciText.Censorship` instead of `NuciText.Censorship.English`? | Unresolved | The current namespace may be intentional to signal that the canonical implementation is English-focused. |
| Should a reference implementation be included in this repository? | Unresolved | Currently separate packages are expected. |
| Should the package provide a `CensorOptions` type for common configuration? | Unresolved | Would require a major version bump if added to the interface. |
