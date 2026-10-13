# Troubleshooting

## Common issues

### Build fails with "unrecognized SDK"

**Cause:** .NET 10.0 SDK is not installed.

**Solution:** Install the [.NET 10.0 SDK](https://dotnet.microsoft.com/download).

### `dotnet test` reports no tests

**Cause:** The package is interface-only and contains no test project.

**Solution:** This is expected. Verification is performed by consumers and implementers. See [Testing](./testing.md).

### `ITextCensor` not found

**Cause:** The `NuciText.Censorship` NuGet package is not installed, or the `using` directive is missing.

**Solution:**

```bash
dotnet add package NuciText.Censorship
```

```csharp
using NuciText.Censorship.English;
```

### `Censor` throws `ArgumentNullException`

**Cause:** The `text` parameter is null.

**Solution:** Ensure the input text is not null before calling `Censor`.

### No concrete implementation available

**Cause:** The `NuciText.Censorship` package only defines the interface. A concrete implementation (e.g., `NuciText.Censorship.English`) must be installed separately.

**Solution:** Install a concrete implementation package and register it via DI.

## Getting help

- [GitHub Issues](https://github.com/hmlendea/nucitext.censorship/issues)
- [README.md](../../README.md)
- [FAQ](./faq.md)
