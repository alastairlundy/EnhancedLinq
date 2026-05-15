---
name: update-method-docs
description: Synchronizes public method definitions in source code with documentation tables in docs/Methods/ for EnhancedLinq projects.
---

# Update Method Docs

Automates the synchronization between public method signatures in the source code and their corresponding documentation tables in the `docs/Methods/` directory.

## When to Use

- When public methods have been added, removed, or modified in any of the core project namespaces.
- Before releasing a new version to ensure documentation matches the API.
- During a large refactor that affects public method signatures.

## When Not to Use

- For documenting internal or private methods.
- For general documentation changes that do not involve method signatures.
- In repositories that do not follow the `docs/Methods/*.md` table convention.

## Inputs

| Input | Required | Description |
|-------|----------|-------------|
| Source paths | Yes | Paths to the projects to scan (e.g., `src/EnhancedLinq/`, `src/EnhancedLinq.Memory/`, `src/EnhancedLinq.Async/`). |
| Doc paths | Yes | Paths to the markdown files containing the tables (e.g., `docs/Methods/EnhancedLinq.md`). |

## Workflow

### Step 1: Scan and Map
1. **Identify Public API**: For each project (`src/EnhancedLinq/`, `src/EnhancedLinq.Memory/`, `src/EnhancedLinq.Async/`), identify all public methods.
    - **Tip**: Use `rg` (ripgrep) for faster scanning of public methods (both static and non-static).
   - **C# 14 Note**: Be aware of `extension` blocks (C# 14); ensure the scanner captures methods nested within these blocks.
2. **Extract Signatures**: Capture the method name, return type, and parameters for each public API member.

### Step 2: Synchronize Documentation
Repeat for each corresponding doc file (e.g., `docs/Methods/EnhancedLinq.md`):
1. **Read Table**: Extract the existing method list from the markdown table.
2. **Diff Analysis**:
    - **Additions**: Identify methods in code but missing from docs.
    - **Removals**: Identify methods in docs that no longer exist in code.
    - **Updates**: Compare signatures for existing entries.
3. **Update Tables**: Apply changes while preserving markdown formatting and category headers (e.g., "Concurrent", "Dictionary").
4. **Write Back**: Commit changes to the `.md` file.

### Step 3: Final Summary
- Provide a consolidated summary of all additions, removals, and updates made across the three documentation files.

## Validation

- [ ] Verify that all public methods in the source code are present in the corresponding `.md` tables.
- [ ] Confirm that no deprecated or removed methods remain in the documentation tables.
- [ ] Ensure that method signatures in the tables exactly match the current code.
- [ ] Check that the markdown table formatting remains intact.

## Common Pitfalls

| Pitfall | Solution |
|---------|----------|
| Missing methods in docs | Ensure the scan includes all files in the project directory. |
| Incorrect signature formatting | Use exact strings from the source code for the method signature column. |
| Breaking markdown table syntax | Use a robust markdown table editor or regex to replace rows rather than rewriting the whole table. |
