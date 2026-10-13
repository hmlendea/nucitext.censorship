# Inspect, edit, delete

## Overview

NuciText.Censorship is a library, not an application. It has no inspect, edit, or delete operations. The package does not manage any entities, records, or state that could be inspected, edited, or deleted.

## What the package does not provide

- No CRUD operations.
- No entity management.
- No state inspection or mutation.
- No audit trails or change history.

## The Censor operation

The only operation the package defines is `ITextCensor.Censor(string text)`, which transforms input text into censored text. This is a pure function (from the caller's perspective) — it does not inspect, edit, or delete any persistent state.

## Consumer responsibilities

Any inspect, edit, or delete functionality is the responsibility of the host application. The host application may use `ITextCensor` as part of its own inspect/edit/delete workflows, but the package itself provides no such workflows.
