# Browser state and localisation

## Overview

NuciText.Censorship is a server-side .NET library. It has no browser component, no client-side state, no localisation resources, and no internationalisation support within this package.

## No browser state

The package does not:

- Manage browser session state.
- Store or retrieve client-side data.
- Handle cookies, local storage, or session storage.
- Participate in any client-server state synchronisation.

## No localisation

The package does not:

- Define resource files (`.resx`, `.json`).
- Provide culture-specific behaviour.
- Localise error messages or documentation.
- Support multi-language word lists.

## Language-specific implementations

Language-specific censorship (e.g., English, Romanian) is handled by separate packages (e.g., `NuciText.Censorship.English`). Each implementation package owns its own word lists and language-specific logic. The abstraction package is language-agnostic.

## Namespace convention

The `ITextCensor` interface is declared in the `NuciText.Censorship.English` namespace. This is the canonical namespace for the contract. Implementations for other languages should reference this namespace when implementing the interface, or define their own namespace conventions in their respective packages.
