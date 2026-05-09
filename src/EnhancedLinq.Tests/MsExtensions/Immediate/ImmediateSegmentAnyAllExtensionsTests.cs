/*
    EnhancedLinq - Tests for Immediate Any and All
*/

using EnhancedLinq.MsExtensions.Immediate;

namespace ExtendedLinq.Tests.MsExtensions.Immediate;

using Microsoft.Extensions.Primitives;

public class ImmediateSegmentAnyAllExtensionsTests
{
    [Test]
    public async Task Any_NoPredicate_BehavesAsExpected()
    {
        StringSegment nonEmpty = new("a");
        StringSegment empty = new("");

        await Assert.That(nonEmpty.Any()).IsTrue();
        await Assert.That(empty.Any()).IsFalse();
    }

    [Test]
    public async Task Any_WithPredicate_ReturnsTrueWhenMatch()
    {
        StringSegment source = new("a1b");

        await Assert.That(source.Any(char.IsDigit)).IsTrue();
    }

    [Test]
    public async Task Any_WithNullPredicate_Throws()
    {
        StringSegment source = new("a");

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            source.Any(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            return Task.CompletedTask;
        });
    }

    [Test]
    public async Task All_WithAllMatching_ReturnsTrue()
    {
        StringSegment source = new("abc");

        await Assert.That(source.All(char.IsLetter)).IsTrue();
    }

    [Test]
    public async Task All_WithMixed_ReturnsFalse()
    {
        StringSegment source = new("a1b");
        
        await Assert.That(source.All(c => char.IsLetter(c))).IsFalse();
    }
}
