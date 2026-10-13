# Security

This document complements the root [SECURITY.md](../SECURITY.md). It elaborates on the security model, threats, and mitigations for NuciText.Censorship.

## Security model

NuciText.Censorship is a .NET abstraction package. It defines a single interface (`ITextCensor`) and contains no implementation, no runtime behaviour, and no data processing of its own. The security model is therefore minimal:

- The package does not process, store, transmit, or log any data.
- The package does not execute any code beyond the interface definition.
- The package does not introduce any trust boundaries.

## Threat model

| Threat | Likelihood | Impact | Mitigation |
|--------|-----------|--------|------------|
| Malicious input text | N/A (package does not process input) | N/A | Input validation is the responsibility of the concrete implementation. |
| Dependency vulnerabilities | Low | Low | The package has zero NuGet dependencies. |
| Supply chain attack | Low | Low | The package is distributed via NuGet with a signed license expression. |
| Information disclosure | N/A (package does not log or persist) | N/A | No data is logged, persisted, or transmitted. |

## Data handling

The package does not handle any personal data. It does not collect, transmit, store, or log any data. All data processing is performed by the concrete implementation, which is the responsibility of the implementation's maintainers.

## Input validation

The package does not perform input validation. The `Censor` method accepts a `string` parameter (not `string?`), indicating that null input is not a supported contract. Concrete implementations may throw `ArgumentNullException` for null input.

## Secure coding practices

The package follows these secure coding practices:

- `Nullable` is enabled, enforcing null-safety at the interface boundary.
- `ImplicitUsings` is disabled, making all dependencies explicit.
- No external dependencies are introduced.
- No logging, persistence, or telemetry code is included.

## Related documentation

- [Root SECURITY.md](../SECURITY.md)
- [Privacy policy](../PRIVACY.md)
- [Error handling](./error-handling.md)
- [Logging](./logging.md)
