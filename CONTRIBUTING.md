# Contributing to secdev

Thank you for contributing to `secdev`. To maintain high reliability and performance, we follow strict development standards.

---

## 1. Branching Strategy (GitFlow)

- **`main`**: Production code only. Direct commits are forbidden.
- **`developer`**: Integration branch for new features and bug fixes.
- **`feat/<feature-name>`**: Create branches off `developer` for all changes.
- **`fix/<bug-name>`**: For targeted bug fixes.
- **`sec/<cve-name>`**: For security patches.

```bash
# Workflow example
git checkout developer
git pull origin developer
git checkout -b feat/custom-shader-pipeline
# ... make changes ...
git push origin feat/custom-shader-pipeline
# Open PR against `developer`
```

---

## 2. Conventional Commits Standard

All commit messages and PR titles must adhere to [Conventional Commits](https://www.conventionalcommits.org/):

Format: `<type>(<optional scope>): <description>`

* `feat`: A new feature (e.g., `feat(engine): add SDF font atlas generator`)
* `fix`: A bug fix (e.g., `fix(ai): prevent token buffer truncation`)
* `sec`: Security improvement (e.g., `sec(sandbox): restrict subprocess env vars`)
* `perf`: Performance boost (e.g., `perf(renderer): batch quad draws into single VBO`)
* `refactor`: Code refactoring without logic changes
* `docs`: Documentation updates
* `ci`: GitHub Actions or automation scripts

---

## 3. Code Standards & Quality Checks

- **Format:** C++20 standard, 4 spaces indent, no tabs, LF line endings.
- **Engine Rules:**
  - Never allocate dynamically inside the per-frame render loop (`Update()` / `Render()`). Pre-allocate ring buffers.
  - Inactive UI panes must not trigger redundant GPU redraws.
- **Local Pre-commit Verification:**
  Run `powershell -File .\scripts\setup-dev.ps1` to link `.githooks` for automated pre-commit scanning.
