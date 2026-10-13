# Logging

## Overview

NuciText.Censorship does not log anything. The package is an interface-only abstraction with no runtime behaviour of its own. It does not produce, write, or transmit any log entries.

## Logging framework

None. The package does not reference any logging framework (Serilog, NLog, log4net, etc.).

## Log levels

Not applicable. No log levels are used.

## Structured fields

Not applicable. No structured fields are attached to any log entry.

## Correlation

Not applicable. No correlation IDs, trace IDs, or span IDs are generated.

## Sinks/destinations

Not applicable. No logs are written to any destination (console, file, database, external service).

## Redaction/masking

Not applicable. No data is logged, so no redaction or masking is needed.

## Retention

Not applicable. No logs are produced.

## Sampling

Not applicable. No log sampling is used.

## Performance considerations

The package has no logging overhead. It is a pure interface with no runtime cost beyond the virtual method dispatch.

## Error logging

The package does not log exceptions. If the host application needs error logging, it must implement it around the `Censor` call. See [Error handling](./error-handling.md).

## Audit logging

Not applicable. No security-relevant events are logged by this package.

## Configuration

Not applicable. There is no logging configuration.

## Host application responsibilities

If the host application needs logging, it should:

1. Log the input text (if appropriate, with redaction).
2. Log the result of the `Censor` call.
3. Log any exceptions thrown by the implementation.
4. Use its own logging framework and configuration.
