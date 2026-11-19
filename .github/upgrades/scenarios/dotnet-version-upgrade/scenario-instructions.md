# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0

## Source Control
- **Source Branch**: main
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)

## Upgrade Options

### Strategy
- Upgrade Strategy: All-at-Once

### Project Structure
- Package Management: Per-Project (defer CPM to post-migration)

### Compatibility
- Unsupported API Handling: Fix Inline

## Strategy
**Selected**: All-at-Once
**Rationale**: Three SDK-style projects target .NET 6, have low assessed difficulty, and form a clear dependency graph; an atomic upgrade avoids unnecessary multi-targeting and tier overhead.

### Execution Constraints
- Upgrade all projects together; do not introduce tier ordering or multi-targeting.
- Verify .NET 10 SDK and repository SDK/tooling configuration before changing project targets.
- Update project targets and dependencies, then resolve API and behavioral changes within the atomic upgrade task.
- Validate the full solution after each top-level task; the final validation must include the complete test suite.
- Record Central Package Management as a post-migration recommendation; do not introduce it during this upgrade.
