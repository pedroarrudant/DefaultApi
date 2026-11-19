# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade all projects in `DefaultAPI.sln` from .NET 6 to .NET 10 (`net10.0`).
**Scope**: Three SDK-style projects: `Application`, `DefaultAPI`, and `DefaultAPITests`; 31 code files and 904 LOC per assessment.

## Upgrade Options

| Option | Selected | Why |
|--------|----------|-----|
| Upgrade Strategy | All-at-Once | Three SDK-style projects target .NET 6, have low assessed difficulty, and form a clear dependency graph; an atomic upgrade avoids unnecessary multi-targeting and tier overhead. |
| Package Management | Per-Project (defer CPM to post-migration) | With three projects and no version conflicts reported, keeping package-management changes out of this upgrade limits unrelated churn. |
| Unsupported API Handling | Fix Inline | Resolve API changes during the upgrade rather than leave stubs and follow-up work. |

## Tasks

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: Three projects, all on .NET 6, SDK-style, and with a clear dependency structure.

### 01-toolchain-readiness: Verify .NET 10 SDK and repository tooling

Check the installed .NET SDK and repository-level configuration that controls SDK selection and builds, including `global.json` and any shared build properties or CI configuration that could prevent targeting `net10.0`. Update only configuration that must change to support the requested target; keep unrelated tooling settings intact. The assessment found three SDK-style projects and did not report legacy project conversion needs.

**Done when**: A .NET 10 SDK is available to the repository, any required SDK/build configuration is compatible with `net10.0`, and the existing solution still restores/builds before the project-target changes.

### 02-net10-project-upgrade: Upgrade all projects, packages, and affected APIs

Upgrade `Application\Application.csproj`, `DefaultAPI\DefaultAPI.csproj`, and `DefaultAPITests\DefaultAPITests.csproj` together from `net6.0` to `net10.0`. The assessment flags 9 binary-incompatible, 10 source-incompatible, and 2 behavioral API changes, along with package upgrade recommendations for `Microsoft.Extensions.Logging` and `Newtonsoft.Json`, a security-vulnerability finding in `Application` (the assessment does not identify its package or CVE), one incompatible `System.Data.SqlClient` reference with `Microsoft.Data.SqlClient` identified as its replacement, and deprecated xUnit. Research actual package usage and resolved vulnerabilities, update packages/tooling references as needed, and account for the flagged configuration binder, dependency-injection, SQL client, `TimeSpan`, and URI behavior changes.

**Done when**: All three projects target `net10.0`; identified package, security, and API migration issues are resolved without deferred stubs; and the full solution builds without errors or warnings.

### 03-solution-validation: Validate build, tests, and dependency health

Run a clean full-solution restore/build and execute the available test suite for `DefaultAPITests`. Review resulting package/security warnings and compatibility findings. Keep project-level package references during this upgrade and record Central Package Management as a post-migration recommendation, since the current solution has three projects and no reported cross-project version conflicts.

**Done when**: The full solution builds warning-free, all available tests pass, and remaining dependency or post-migration recommendations are documented.
