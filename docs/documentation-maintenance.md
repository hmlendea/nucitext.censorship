# Documentation maintenance

## Overview

This document describes how to keep the `docs/` documentation current and who is responsible for it.

## Ownership

The documentation is maintained by the repository maintainers. Contributions are welcome via pull requests. See [CONTRIBUTING.md](../../CONTRIBUTING.md) for guidelines.

## Update process

1. When modifying source code, update the relevant `docs/` files.
2. When adding new files to `docs/`, update `docs/INDEX.md`.
3. When changing the interface, update:
   - `docs/api-reference/INDEX.md`
   - `docs/api-usage-examples.md`
   - `docs/components/presentation.md`
   - `docs/repository-overview.md`
4. When changing the project file, update:
   - `docs/build-and-deployment.md`
   - `docs/configuration.md`
5. When changing the CI/CD workflow, update:
   - `docs/build-and-deployment.md`

## Review schedule

Documentation should be reviewed:

- On every pull request that modifies source code or configuration.
- At least quarterly for accuracy and completeness.
- When the root `ARCHITECTURE.md`, `SECURITY.md`, `PRIVACY.md`, or `README.md` is updated.

## Cross-reference integrity

When adding or removing documentation files:

1. Update `docs/INDEX.md` with the new file or remove the deleted file.
2. Check all cross-references in related files.
3. Use relative links from the `docs/` root (e.g., `[Architecture](./architecture.md)`).

## Last reviewed

- **Date:** 2026-10-13
- **Owner:** Repository maintainers
- **Coverage status:** Complete for the current repository state.
