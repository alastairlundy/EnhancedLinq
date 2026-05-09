/*
    EnhancedLinq - Tests for Immediate CountAtLeast and CountAtMost
*/

using EnhancedLinq.MsExtensions.Immediate;

namespace ExtendedLinq.Tests.MsExtensions.Immediate;

using Microsoft.Extensions.Primitives;

public class ImmediateCountAtLeastAtMostExtensionsTests
{
    [Test]
    public async Task CountAtLeast_NoPredicate_Works()
    {
        StringSegment source = new("abcd");

        await Assert.That(source.CountAtLeast(3)).IsTrue();
        await Assert.That(source.CountAtLeast(5)).IsFalse();
    }

    [Test]
    public async Task CountAtLeast_WithPredicate_Works()
    {
        StringSegment source = new("a1b2c");

        await Assert.That(source.CountAtLeast(char.IsLetter, 3)).IsTrue();
        await Assert.That(source.CountAtLeast(char.IsLetter, 4)).IsFalse();
    }

    [Test]
    public async Task CountAtMost_NoPredicate_Works()
    {
        StringSegment source = new("ab");

        await Assert.That(source.CountAtMost(3)).IsTrue();
        await Assert.That(source.CountAtMost(1)).IsFalse();
    }

    [Test]
    public async Task CountAtMost_WithPredicate_Works()
    {
        StringSegment source = new("a1b2c");

        await Assert.That(source.CountAtMost(char.IsLetter, 3)).IsTrue();
        await Assert.That(source.CountAtMost(char.IsLetter, 2)).IsFalse();
    }
}
