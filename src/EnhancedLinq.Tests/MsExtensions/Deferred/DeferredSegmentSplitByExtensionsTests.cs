/*
    EnhancedLinq - Tests for Deferred SplitBy
*/

namespace ExtendedLinq.Tests.MsExtensions.Deferred;

using System.Linq;
using Microsoft.Extensions.Primitives;
using EnhancedLinq.MsExtensions.Deferred;

public class DeferredSegmentSplitByExtensionsTests
{
    [Test]
    public async Task SplitBy_Char_ReturnsAllSegments()
    {
        StringSegment source = new("a,b,c");

        var parts = source.SplitBy(',').Select(p => p.ToString()).ToList();

        await Assert.That(parts).IsEquivalentTo(new[] { "a", "b", "c" });
    }

    [Test]
    public async Task SplitBy_StringSegment_ReturnsAllSegments()
    {
        StringSegment source = new("a<>b<>c");
        StringSegment sep = new("<>");

        var parts = source.SplitBy(sep).Select(p => p.ToString()).ToList();

        await Assert.That(parts).IsEquivalentTo(new[] { "a", "b", "c" });
    }

    [Test]
    public async Task SplitBy_NoSeparator_ReturnsSourceAsSingleSegment()
    {
        StringSegment source = new("abc");

        var parts = source.SplitBy(',').Select(p => p.ToString()).ToList();

        await Assert.That(parts).IsEquivalentTo(new[] { "abc" });
    }

    [Test]
    public async Task SplitBy_Predicate_NullPredicate_Throws()
    {
        StringSegment source = new("a1b");

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            source.SplitBy((Func<char, bool>)null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return Task.CompletedTask;
        });
    }
}
