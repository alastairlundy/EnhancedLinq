/*
    EnhancedLinq - Tests for Immediate IndicesOf
*/

using EnhancedLinq.MsExtensions.Immediate;

namespace ExtendedLinq.Tests.MsExtensions.Immediate;

using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

public class ImmediateSegmentIndicesExtensionsTests
{
    [Test]
    public async Task IndicesOf_CharOccurrences_ReturnsIndices()
    {
        StringSegment source = new("ababa");

        var indices = source.IndicesOf('a');

        await Assert.That(indices).IsEquivalentTo(new List<int> { 0, 2, 4 });
    }

    [Test]
    public async Task IndicesOf_Subsegment_ReturnsAllOccurrences()
    {
        StringSegment source = new("ababa");
        StringSegment other = new("aba");

        var indices = source.IndicesOf(other);

        await Assert.That(indices).IsEquivalentTo(new List<int> { 0, 2 });
    }

    [Test]
    public async Task IndicesOf_EmptySource_ReturnsEmptyList()
    {
        StringSegment source = new("");

        var indices = source.IndicesOf('x');

        await Assert.That(indices).IsEquivalentTo(new List<int>());
    }
}
