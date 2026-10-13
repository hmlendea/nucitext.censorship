# Concurrency and scheduling

## Overview

NuciText.Censorship is a library with no concurrency or scheduling concerns of its own. The package contains no threads, no async methods, no tasks, no timers, no locks, and no scheduling logic.

## Thread safety

The `ITextCensor` interface does not specify thread-safety guarantees. Thread safety is determined by the concrete implementation:

- If the implementation is stateless, it is inherently thread-safe.
- If the implementation maintains mutable state, it must handle its own synchronisation.

The host application should consult the implementation's documentation for thread-safety guarantees.

## Async support

The `Censor` method is synchronous (`string Censor(string text)`). There is no `CensorAsync` method. If an implementation needs to perform asynchronous work (e.g., fetching a remote word list), it must do so synchronously within the `Censor` method or provide its own async wrapper.

## Scheduling

The package does not schedule any work. There are no background tasks, no timers, and no deferred execution.

## Locking

The package does not use any locks. Locking, if needed, is the responsibility of the concrete implementation.
