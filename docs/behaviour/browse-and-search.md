# Browse and search

## Overview

NuciText.Censorship is a library, not an application. It has no browse, search, or query functionality. The package does not expose any searchable API surface beyond the `ITextCensor` interface.

## What the package does not provide

- No search endpoints.
- No query APIs.
- No filtering or sorting capabilities.
- No listing or browsing operations.

## Consumer responsibilities

The host application is responsible for any search or browse functionality that uses `ITextCensor`. For example, a host application might:

1. Search a database of user messages.
2. Pass each message through `ITextCensor.Censor()`.
3. Return the censored results.

This search logic lives in the host application, not in this package.
