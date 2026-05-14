/*
    EnhancedLinq - Tests for MsExtensions Deferred IndicesOf
*/

using EnhancedLinq.MsExtensions.Deferred;

namespace ExtendedLinq.Tests.MsExtensions.Deferred;

using System.Linq;
using Microsoft.Extensions.Primitives;

public class DeferredSegmentIndicesOfExtensionsTests
{
    [Test]
    public async Task IndicesOf_CharOccurrences_ReturnsIndices()
    {
        StringSegment source = new("ababa");

        List<int> indices = source.IndicesOf('a').ToList();

        await Assert.That(indices).IsEquivalentTo(new List<int> { 0, 2, 4 });
    }

    [Test]
    public async Task IndicesOf_EmptySource_Throws()
    {
        StringSegment source = new("");

        await Assert.ThrowsAsync<ArgumentException>(() =>
            Task.FromResult(source.IndicesOf('a')));
    }
}
