# 01-toolchain-readiness: Verify .NET 10 SDK and repository tooling

Check the installed .NET SDK and repository-level configuration that controls SDK selection and builds, including `global.json` and any shared build properties or CI configuration that could prevent targeting `net10.0`. Update only configuration that must change to support the requested target; keep unrelated tooling settings intact. The assessment found three SDK-style projects and did not report legacy project conversion needs.

## Scope Inventory
- **Projects affected**: Repository-level preflight for `Application`, `DefaultAPI`, and `DefaultAPITests`; no project file changes are currently indicated.
- **Distinct concerns**: .NET SDK availability, SDK pin/shared MSBuild settings, CI/build scripts, and baseline build/test behavior.
- **Assessment signals**: All three projects are SDK-style and currently target `net6.0`; there are no old-style conversion needs.

## Research Findings
- Installed SDK is .NET `10.0.403`; `dotnet --version` resolves to `10.0.403`. `dotnet --info` reports no `global.json`.
- Repository inspection found no `Directory.Build.props`, `Directory.Build.targets`, `Directory.Packages.props`, NuGet config, GitHub workflow, or script file that pins the SDK or target framework. No required tooling/config edit was identified.
- Baseline full-solution build succeeded: 3 projects succeeded, 0 failed. Existing warnings include `NETSDK1138` for `net6.0`, `NU1902` and `NU1903` for `System.Data.SqlClient` 4.8.3 (GHSA-8g2p-5pqh-5jmc and GHSA-98g6-xh36-x2p7), and nullable warnings in Application and DefaultAPI. These are baseline findings for the project-upgrade task; no warning suppression is planned.
- Baseline `DefaultAPITests` test discovery was attempted, but no tests ran: the testhost requires `Microsoft.NETCore.App 6.0.0`, which is not installed (only .NET 8 and 10 runtimes are present). Re-run tests after retargeting the test project to .NET 10.

**Done when**: A .NET 10 SDK is available to the repository, any required SDK/build configuration is compatible with `net10.0`, and the existing solution still restores/builds before the project-target changes.
