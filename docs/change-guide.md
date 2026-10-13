# Change guide

## Overview

This guide explains how to safely modify common areas of the NuciText.Censorship repository.

## Modifying the `ITextCensor` interface

### When to modify

- Adding a new method to the interface.
- Changing the signature of `Censor`.
- Changing the namespace.

### Impact

Any change to the `ITextCensor` interface is a **breaking change** for all consumers and implementations. It requires a major version bump.

### Process

1. Update `NuciText.Censorship/ITextCensor.cs`.
2. Update XML documentation comments.
3. Update `README.md` if the usage example changes.
4. Update `ARCHITECTURE.md` if the contract description changes.
5. Bump the major version in `NuciText.Censorship.csproj`.
6. Update `docs/api-reference/INDEX.md` and `docs/api-usage-examples.md`.

### Verification

```bash
dotnet build NuciText.Censorship
```

## Modifying the project file

### When to modify

- Changing the target framework.
- Updating package metadata (version, description, tags).
- Changing build properties (`Nullable`, `ImplicitUsings`, etc.).

### Impact

- Changing the target framework may break consumers on older .NET versions.
- Changing `Nullable` or `ImplicitUsings` may affect compilation of the interface.

### Process

1. Update `NuciText.Censorship/NuciText.Censorship.csproj`.
2. Verify the build succeeds.
3. Update `docs/build-and-deployment.md` if the target framework or build process changes.

## Modifying documentation

### When to modify

- Any change to the interface, project file, or repository structure.
- Adding new documentation files.

### Process

1. Update the relevant `docs/` file.
2. Update `docs/INDEX.md` if adding or removing files.
3. Update cross-references in related files.

## Adding new documentation files

1. Create the file under `docs/` with a kebab-case name.
2. Add a one-line description to `docs/INDEX.md`.
3. Link to the new file from related documentation.

## CI/CD changes

The GitHub Actions workflow is in `.github/workflows/dotnet.yml`. Changes to the workflow should:

1. Maintain the existing structure (checkout, setup .NET, restore, build, test).
2. Update the .NET version if the target framework changes.
3. Update `docs/build-and-deployment.md` to reflect any changes.
