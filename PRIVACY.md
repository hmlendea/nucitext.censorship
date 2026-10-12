# Privacy and Personal Data

This document describes how NuciText.Censorship handles personal data. NuciText.Censorship is a .NET abstraction package that provides a common `ITextCensor` interface for filtering offensive words and URLs from text. The package itself does not collect, transmit, or store personal data; it operates entirely on text supplied by the host application at runtime.

**Information reviewed:** 2026-10-12

## 📑 Table of Contents

- [What This Document Covers](#-what-this-document-covers)
- [Self-Hosted Deployments](#-self-hosted-deployments)
- [Data We Handle](#-data-we-handle)
- [Processing and Use](#-processing-and-use)
- [Storage, Retention, and Deletion](#-storage-retention-and-deletion)
- [External Processing and Integrations](#-external-processing-and-integrations)
- [Document Changes](#-document-changes)
- [Contact](#-contact)

## 🔎 What This Document Covers

This document describes how NuciText.Censorship at https://github.com/hmlendea/nucitext.censorship handles personal data. It covers the application behaviour and verified integrations described below. Where the software is self-hosted, the instance operator may have separate responsibilities described below.

## 🏠 Self-Hosted Deployments

This document covers the NuciText.Censorship package as published on NuGet. The package is a library consumed by host applications; it is not a standalone service operated by the project maintainers. Each host application and its operator control their own configuration, local storage, logs, backups, access controls, retention, and request handling.

NuciText.Censorship does not send data from a self-hosted instance to project maintainers or external services. The package performs text censoring locally within the host application process and does not include telemetry, update checks, crash reports, email, authentication, reverse-proxy, object-storage, or monitoring integrations.

## 📥 Data We Handle

### Data Provided to the Application

The host application supplies text to the `Censor` method. Any personal data contained in that text is provided by the host application's users, administrators, or connected systems. NuciText.Censorship does not request personal data directly.

### Data Generated or Collected by the Application

NuciText.Censorship does not generate or collect personal data automatically. The package does not produce server, application, access, audit, telemetry, crash, or update-check data.

### Data Received from Integrations

NuciText.Censorship does not receive personal data from integrations or third parties.

## 🧭 Processing and Use

The application processes the data described above for these verified functions:
- Censoring offensive words and URLs in supplied text — Data Provided to the Application

## 🗄️ Storage, Retention, and Deletion

NuciText.Censorship does not store, persist, or retain any data. The `Censor` method processes text in memory and returns the censored result without writing to disk, databases, logs, caches, or backups. The host application and its operator control all storage, retention, and deletion of data processed through this package.

## 🔗 External Processing and Integrations

NuciText.Censorship has no built-in external data transfer. The package does not communicate with external services, recipients, or integrations.

| Service or integration name | Purpose | Data category | Verified configuration or documentation URL |
|-----------------------------|---------|---------------|---------------------------------------------|

## 🔄 Document Changes

Update this document when application data flows, storage, integrations, or deployment responsibilities change. The current version is published at https://github.com/hmlendea/nucitext.censorship/blob/main/PRIVACY.md.

## 📬 Contact

For questions about application data handling, contact the project maintainers. For a self-hosted instance, contact the instance operator, unless the project explicitly handles the request. Include the package version and a description of the data flow in question; do not send passwords, access tokens, or other secrets.
