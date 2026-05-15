---
name: write-changelog
description: Generates a markdown changelog listing commit history since a specified tag/commit, grouped by package and including author attribution.
---

# write-changelog

Automate the generation of markdown changelogs based on git history between a specific tag/commit and a target branch, grouped by affected package.

## When to Use

- Creating release notes for a new version.
- Generating a summary of changes since a specific milestone.
- Auditing changes per package for a specific release cycle.

## When Not to Use

- For extremely large commit ranges where a summarized manual changelog is preferred.
- When the required git tags or range information is unavailable.

## Inputs

| Input | Required | Description |
|-------|----------|-------------|
| Prior release Git Tag/Commit | Yes | The starting point of the changelog range. |
| Branch the Tag/Commit exists on | Yes | The source branch for the prior release. |
| Target Branch/Commit | No | The end point of the range. Defaults to latest on provided branch if omitted. |
| Destination file location | No | Path to save the output. If omitted, outputs as a message. |

## Workflow

### Step 1: Commit Retrieval
- Determine the target range in the format `<prior_tag>..<target>`.
- If information is insufficient to determine the range, inform the user clearly listing the missing pieces.
- Use the `bash` tool to run: `git log --pretty=format:"%H %s %an <%ae>" --name-only <range>`
- Capture the commit hashes, messages, author name, author email, and the list of affected files.

### Step 2: Package Mapping
- Map modified files to the following packages:
    - `EnhancedLinq.Memory`: Files starting with `src/EnhancedLinq.Memory/`.
    - `EnhancedLinq.Async`: Files starting with `src/EnhancedLinq.Async/`.
    - `EnhancedLinq`: Files starting with `src/EnhancedLinq/` (excluding the specific `.Memory` and `.Async` paths).
    - `All packages`: Root-level changes, tests, and other non-package specific files.
- A single commit may be mapped to multiple packages if it touches files in different locations.

### Step 3: Attribution Resolution
- Extract the Git display name.
- Attempt to correlate the email to a GitHub username.
- Format the entry as: `Commit message by Git Display Name/@[GitHubUserName]`.

### Step 4: Markdown Construction
- Begin with the header: `## Changes since [Prior release Git Tag version]:`
- Group commits under the following sections in this exact order:
    1. `All packages`
    2. `EnhancedLinq`
    3. `EnhancedLinq.Memory`
    4. `EnhancedLinq.Async`
- Use markdown bullet points for each commit entry.

### Step 5: Output Phase
- If `destination_path` is provided:
    - Use the `bash` tool to check if the file exists.
    - If it exists, use the `question` tool to ask: "File [path] already exists. Overwrite?".
    - Use the `write` tool to save the markdown content.
- If no destination is provided, output the final markdown string as a response to the user.

## Validation

- [ ] The header accurately reflects the starting Git tag.
- [ ] Commits are correctly grouped into the four specified package categories.
- [ ] Each entry contains the commit message and author attribution.
- [ ] If a destination file was specified, the file is created/updated as expected.

## Common Pitfalls

| Pitfall | Solution |
|---------|----------|
| Incorrect mapping of `EnhancedLinq` | Ensure `.Memory` and `.Async` paths are checked before the generic `src/EnhancedLinq/` path. |
| Ambiguous target range | Proactively ask the user to provide a specific target commit if the branch head is unclear. |
| Overwriting important files | Always use the `question` tool before invoking `write` on an existing file. |
