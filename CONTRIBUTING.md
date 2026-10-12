# Contributing to NuciText.Censorship

This document covers the guidelines and processes for contributing to this project, including how to report issues, suggest enhancements, submit code changes, and follow project standards.

## 📑 Table of Contents

- [How to Contribute](#-how-to-contribute)
  - [Reporting Issues](#-reporting-issues)
  - [Suggesting Enhancements](#-suggesting-enhancements)
  - [Code Contributions](#-code-contributions)
    - [Prerequisites](#-prerequisites)
    - [Development Setup](#-development-setup)
    - [Making Changes](#-making-changes)
    - [Pull Request Guidelines](#-pull-request-guidelines)
  - [Code Style](#-code-style)
  - [Testing](#-testing)
  - [Documentation](#-documentation)
- [Security](#-security)
- [License](#-license)
- [Getting Help](#-getting-help)

## 🤝 How to Contribute

### Reporting Issues

- Search existing issues first.
- Use the issue templates if available.
- Provide clear reproduction steps.
- Include environment details.

### Suggesting Enhancements

- Check the roadmap and existing discussions.
- Explain the use case and expected behavior.
- Consider implementation complexity.

### Code Contributions

#### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

#### Development Setup

```bash
# Clone the repository
git clone https://github.com/hmlendea/nucitext.censorship.git
cd nucitext.censorship

# Install dependencies
dotnet restore
```

#### Making Changes

1. Fork the repository.
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Make your changes.
4. Commit with clear and descriptive messages.
5. Push to your fork.
6. Open a Pull Request.

#### Pull Request Guidelines

- Target the `main` branch.
- Keep PRs focused and atomic.
- Update documentation if applicable.
- Add tests for any new functionality.
- Ensure the CI checks pass.

### Code Style

Follow the project's coding standards:
- Keep changes cross-platform
- Keep the existing public contract intact unless a breaking change is intentional
- Keep pull requests focused and consistent with the existing code style
- Update documentation when behaviour changes

### Testing

```bash
# Run all tests
dotnet test

# Run specific test suite
dotnet test --filter "FullyQualifiedName~NuciText.Censorship"
```

### Documentation

- Update relevant docs for changes.
- Follow the documentation style guide.
- Preview changes locally if possible.

## 🔒 Security

Report security vulnerabilities per the [Security Policy](./SECURITY.md).

## 📄 License

By contributing, you agree that your contributions will be licensed under the `GNU General Public License v3.0` or later. See [LICENSE](./LICENSE) for details.

## ❓ Getting Help

- [Open an issue](https://github.com/hmlendea/nucitext.censorship/issues)
- Check existing discussions and documentation first.
