/*
    EnhancedLinq - Tests for MsExtensions Immediate Split
*/

using EnhancedLinq.MsExtensions.Immediate;

namespace ExtendedLinq.Tests.MsExtensions.Immediate;

using System.Linq;
using Microsoft.Extensions.Primitives;

public class ImmediateSegmentSplitExtensionsTests
{
    [Test]
    public async Task Split_OnCharSeparator_ReturnsSegments()
    {
        StringSegment source = new("a,b,c");

        StringSegment[] parts = source.Split(',');

        await Assert.That(parts.Select(p => p.ToString()))
            .IsEquivalentTo(["a", "b", "c"]);
    }

    [Test]
    public async Task Split_OnStringSegmentSeparator_ReturnsSegments()
    {
        StringSegment source = new("a<>b<>c");
        StringSegment sep = new("<>");

        StringSegment[] parts = source.Split(sep);

        await Assert.That(parts.Select(p => p.ToString())).IsEquivalentTo(["a", "b", "c"]);
    }

    [Test]
    public async Task Split_EmptySource_ReturnsEmptyArray()
    {
        StringSegment source = new("");

        StringSegment[] parts = source.Split(',');

        await Assert.That(parts).IsEquivalentTo(Array.Empty<StringSegment>());
    }
}
