/*
    EnhancedLinq - Tests for MsExtensions Immediate IndexOf
*/

namespace ExtendedLinq.Tests.MsExtensions.Immediate;

using EnhancedLinq.MsExtensions.Immediate;
using Microsoft.Extensions.Primitives;

public class ImmediateSegmentIndexOfExtensionsTests
{
    [Test]
    public async Task IndexOf_WithEmptySearchSegment_ReturnsNegativeOne()
    {
        StringSegment segment = new("hello");
        StringSegment other = new("");

        int actual = segment.IndexOf(other);

        await Assert.That(actual).IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOf_FindsSubstringInString_ReturnsIndex()
    {
        string s = "hello world";
        var seg = new StringSegment("world");

        int actual = s.IndexOf(seg);

        await Assert.That(actual).IsEqualTo(6);
    }

    [Test]
    public async Task IndexOf_NotFound_ReturnsNegativeOne()
    {
        StringSegment segment = new("abcdef");
        StringSegment other = new("xyz");

        int actual = segment.IndexOf(other);

        await Assert.That(actual).IsEqualTo(-1);
    }
}
