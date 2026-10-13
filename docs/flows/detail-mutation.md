# Detail mutation

## Overview

This flow document does not apply to NuciText.Censorship. The package is a text censorship abstraction library and has no entity detail, mutation, or CRUD operations.

## The censorship call flow

The only meaningful "mutation" in this package is the transformation of input text to censored text, performed by the `Censor` method. This is documented in:

- [Startup and rendering](./startup-and-rendering.md)
- [Host and composition](../components/host-and-composition.md)
- [Presentation layer](../components/presentation.md)

## Note on flow documentation

The `flows/` directory structure follows a standard documentation template. Not all flow documents are applicable to every repository. This repository's only meaningful flow is the censorship call flow.
