# AGENTS.md

## Quick Summary
EnhancedLinq is a suite of .NET libraries providing deferred and immediate enumeration mode extensions for LINQ, targeting `IEnumerable<T>`, `IAsyncEnumerable<T>`, `Span<T>`, and `Memory<T>`.

## Key Facts
- **Language**: Requires C# 14 (`<LangVersion>14</LangVersion>`).
- **Target Frameworks**: Multi-targeted for `net10.0`, `net9.0`, `net8.0`, and `netstandard2.0`.
- **Trim/AOT**: `EnableTrimAnalyzer` is enabled; prioritize trim-compatible implementations.
- **Dependencies**: Heavily relies on `DotExtensions`, `DotPrimitives.Collections`, and `Polyfill`.

## High-Signal Context

- **Solution Structure**:
  - `src/EnhancedLinq`: Core package, includes `MsExtensions` for `Microsoft.Extensions.Primitives`.
  - `src/EnhancedLinq.Async`: `IAsyncEnumerable` support.
  - `src/EnhancedLinq.Memory`: Immediate mode extensions for `Span<T>` and `Memory<T>`.
  - `src/EnhancedLinq.Tests`: Test suite.

## Developer Commands

- **Build Solution**: `dotnet build src/EnhancedLinq.sln`
- **Run Tests**: `dotnet test src/EnhancedLinq.Tests/EnhancedLinq.Tests.csproj`

## Common Gotchas
- **Deferred vs Immediate**: Extensions in `EnhancedLinq.Memory` must be immediate mode as they target `Span<T>`, which cannot be used in deferred execution (non-generic iterators).
- **C# 14 Features**: The project uses the latest C# 14 features (e.g., extension blocks); ensure your IDE/tooling is updated to avoid false positives.
- **Polyfills**: The `Polyfill` package is used for backporting newer .NET APIs to `netstandard2.0`.

## Tests and Validation
Before submitting a PR, ensure:
1. **Full Build**: Run `dotnet build src/EnhancedLinq.sln` to verify no breaking changes across any Target Frameworks.
2. **Test Suite**: Run all tests in `src/EnhancedLinq.Tests/EnhancedLinq.Tests.csproj`.
3. **Analyzer Check**: Ensure `Meziantou.Analyzer` warnings are addressed.
