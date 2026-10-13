# Application services

## Overview

NuciText.Censorship is an abstraction package. It contains no application services, no business logic, no orchestration, and no processing pipelines. The sole purpose of the package is to define the `ITextCensor` contract that concrete implementations fulfil.

## What the package does not provide

| Concern | Status |
|---------|--------|
| Service classes | None |
| Orchestration logic | None |
| Business rules | None |
| Processing pipelines | None |
| Validation logic | None (delegated to implementations) |
| Caching | None |
| Retry policies | None |
| Circuit breakers | None |

## What the package provides

| Concern | Detail |
|---------|--------|
| Contract definition | `ITextCensor` interface with `string Censor(string text)` |
| Package metadata | NuGet identity, versioning, target framework |
| Documentation | XML documentation file generated at build time |

## Consumer responsibilities

The host application is responsible for:

1. Selecting a concrete `ITextCensor` implementation.
2. Registering it in the DI container.
3. Calling `Censor` and handling any exceptions.
4. Managing the lifetime and configuration of the implementation.

## Implementation responsibilities

Concrete implementations (in separate packages) are responsible for:

1. Implementing the `Censor` method.
2. Defining word lists, URL detection, and replacement strategies.
3. Handling input validation (e.g., null checks).
4. Managing their own configuration and state.
