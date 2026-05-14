# AGENTS.md

## Quick Summary
EnhancedLinq is a suite of .NET libraries providing deferred and immediate enumeration mode extensions for LINQ, targeting `IEnumerable<T>`, `IAsyncEnumerable<T>`, `Span<T>`, and `Memory<T>`.

## Key Facts
- **Language**: Requires C# 14 (`<LangVersion>14</LangVersion>`).
- **Target Frameworks**: Multi-targeted for `net10.0`, `net9.0`, `net8.0`, and `netstandard2.0`.
- **Trim/AOT**: `EnableTrimAnalyzer` is enabled; prioritize trim-compatible implementations.
- **Dependencies**: Heavily relies on `DotExtensions` and `Polyfill`.

## High-Signal Context

- **Solution Structure**:
  - `src/EnhancedLinq`: Core package, includes `MsExtensions` for `Microsoft.Extensions.Primitives`.
  - `src/EnhancedLinq.Async`: `IAsyncEnumerable` support.
  - `src/EnhancedLinq.Memory`: Immediate mode extensions for `Span<T>` and `ReadOnlySpan<T>`.
  - `src/EnhancedLinq.Tests`: Main test project.
  - `src/EnhancedLinq.Memory.Tests`: Memory-specific test project.

## Developer Commands

- **Build Solution**: `dotnet build src/EnhancedLinq.sln`
- **Run Regular Tests**: `dotnet test src/EnhancedLinq.Tests/EnhancedLinq.Tests.csproj`
- **Run Memory Tests**: `dotnet test src/EnhancedLinq.Memory.Tests/EnhancedLinq.Memory.Tests.csproj`

## Common Gotchas
- **Deferred vs Immediate**: Extensions in `EnhancedLinq.Memory` target `Memory<T>` and `ReadOnlyMemory<T>`, which can be used in deferred execution; only `Span<T>` and `ReadOnlySpan<T>` are immediate‑mode only.
- **C# 14 Features**: The project uses the latest C# 14 features (e.g., extension blocks).
- **Polyfills**: The `Polyfill` package is used for backporting newer .NET APIs to `netstandard2.0`.
- **Multiple Enumeration**: Enumerating an `IEnumerable<T>` or `IAsyncEnumerable<T>` multiple times causes full re‑enumeration, which can be significantly slower than enumerating a cached collection.
- **Test Execution**: If a test does not run, use `dotnet test --help` to view available command-line options.

## Tests and Validation
Before submitting a PR, ensure:
1. **Full Build**: Run `dotnet build src/EnhancedLinq.sln` to verify no breaking changes across any Target Frameworks.
2. **Regular Tests**: Run `dotnet test src/EnhancedLinq.Tests/EnhancedLinq.Tests.csproj`.
3. **Memory Tests**: Run `dotnet test src/EnhancedLinq.Memory.Tests/EnhancedLinq.Memory.Tests.csproj`.
4. **Analyzer Check**: Ensure `Meziantou.Analyzer` warnings are addressed.