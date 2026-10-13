# FAQ

## Frequently asked questions

### What is NuciText.Censorship?

A .NET abstraction package that defines the `ITextCensor` interface for text censorship. It provides no implementation — concrete implementations are separate packages.

### How do I use it?

Register a concrete `ITextCensor` implementation via dependency injection, then inject and call `Censor`:

```csharp
services.AddSingleton<ITextCensor, EnglishTextCensor>();
```

### Is there a reference implementation?

No. This repository contains only the interface. Implementations are provided by separate packages (e.g., `NuciText.Censorship.English`).

### Does it have tests?

No. The package is interface-only. Verification is performed by consumers and implementers.

### What .NET version is required?

.NET 10.0 or later.

### Is it free?

Yes. The package is licensed under the GNU General Public License v3.0 or later. See [LICENSE](../../LICENSE).

### Can I contribute?

Yes! See [CONTRIBUTING.md](../../CONTRIBUTING.md).

### Where is the documentation?

See [docs/INDEX.md](./INDEX.md) for the full documentation catalogue.
