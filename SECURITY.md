# Security Policy

FTAnalyzer is developed and maintained by a single hobbyist developer. Security reports are welcome and taken seriously, but please be patient - there is no dedicated security team behind this project.

## Supported Versions

Only the latest published release (Microsoft Store or GitHub release) is supported with security fixes. Please update to the latest version before reporting an issue, in case it's already fixed.

## Reporting a Vulnerability

**Please do not open a public GitHub issue for security vulnerabilities.**

Instead, use GitHub's private vulnerability reporting:

1. Go to the [Security tab](https://github.com/ShammyLevva/FTAnalyzer/security) of this repository.
2. Click **Report a vulnerability**.

This opens a private advisory visible only to the maintainer, so the issue isn't disclosed publicly before a fix is available.

Please include:
- A description of the vulnerability and its potential impact.
- Steps to reproduce, or a proof of concept if possible.
- The affected version of FTAnalyzer.

## Scope

FTAnalyzer is a Windows desktop application that reads GEDCOM genealogy files and stores data locally. Reports of general interest include:
- Ways a malicious or malformed GEDCOM file could cause more than a crash (e.g. arbitrary code execution, file system access outside the intended folders, injection into a downstream system).
- Handling of the Google Maps/Geocoding API key and other locally stored credentials.
- Issues in the [ftanalyzer.com](https://ftanalyzer.com) documentation site.

Reports about missing best-practice hardening in low-impact, non-exploitable areas are welcome as regular GitHub issues rather than private reports.
