# Integrations

## Overview

NuciText.Censorship has no built-in external integrations. The package does not communicate with external services, recipients, or third-party systems. It is a pure .NET interface abstraction.

## Integration boundaries

| Integration | Direction | Protocol | Owner |
|-------------|-----------|----------|-------|
| Host application | Consumer | .NET interface call | Host application |
| Concrete implementation | Provider | .NET interface implementation | Separate package |
| .NET 10.0 runtime | Platform | BCL | Microsoft |
| NuGet | Distribution | Package reference | NuGet.org |
| GitHub Actions | CI/CD | YAML workflow | Repository maintainers |

## No external service integrations

The package does not integrate with:

- Web APIs or HTTP services.
- Databases or persistence stores.
- Message queues or event streams.
- Logging frameworks or telemetry systems (e.g., Application Insights, Sentry).
- Authentication or authorisation services (e.g., OAuth, JWT).
- Configuration providers (e.g., Azure App Configuration, AWS Systems Manager).
- Cloud services (e.g., Azure, AWS, GCP).
- Email or notification services.
- Update checks or crash reporting.

## Data transfer

The package does not send data from a self-hosted instance to project maintainers or external services. The package performs text censoring locally within the host application process.

## Related documentation

- [Privacy policy](../../PRIVACY.md)
- [Integration models](./components/integration-models.md)
- [Data model](./data-model.md)
