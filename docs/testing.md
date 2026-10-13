# Testing

## Overview

NuciText.Censorship is an interface-only package. It contains no tests. Verification is performed by consumers and implementers of the `ITextCensor` interface.

## Test strategy

The package itself has no test project. The testing strategy is:

1. **Consumer integration tests:** Host applications test their integration with concrete `ITextCensor` implementations.
2. **Implementation tests:** Concrete implementation packages (e.g., `NuciText.Censorship.English`) test their own `Censor` logic.
3. **Build verification:** `dotnet build NuciText.Censorship` succeeds.
4. **Package validation:** `dotnet pack` produces a valid NuGet package.

## Test organisation

There are no tests in this repository. The repository structure contains only:

- `NuciText.Censorship/ITextCensor.cs` — The interface.
- `NuciText.Censorship/NuciText.Censorship.csproj` — The project file.

## Running tests

The `CONTRIBUTING.md` references `dotnet test`, but there are no tests to run:

```bash
dotnet test
```

This command will succeed but report "no test matches the given testcase filter" or similar, since no test project exists.

## Build verification

```bash
dotnet build NuciText.Censorship
```

## Package validation

```bash
dotnet pack NuciText.Censorship
```

## What should be tested by consumers

Consumers and implementers should test:

- Null input handling.
- Empty string handling.
- Normal text censoring.
- URL censoring.
- Edge cases (very long strings, special characters, Unicode).
- Thread safety (if applicable).
- Performance characteristics.

## Related documentation

- [Build and deployment](./build-and-deployment.md)
- [Error handling](./error-handling.md)
- [Ambiguities and open questions](./ambiguities-and-open-questions.md)
