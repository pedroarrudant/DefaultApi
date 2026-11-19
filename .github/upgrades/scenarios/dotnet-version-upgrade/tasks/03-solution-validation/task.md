# 03-solution-validation: Validate build, tests, and dependency health

Run a clean full-solution restore/build and execute the available test suite for `DefaultAPITests`. Review resulting package/security warnings and compatibility findings. Keep project-level package references during this upgrade and record Central Package Management as a post-migration recommendation, since the current solution has three projects and no reported cross-project version conflicts.

**Done when**: The full solution builds warning-free, all available tests pass, and remaining dependency or post-migration recommendations are documented.
