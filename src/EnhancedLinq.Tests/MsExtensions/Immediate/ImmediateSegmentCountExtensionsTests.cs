/*
    EnhancedLinq - Tests for Immediate Count
*/

using EnhancedLinq.MsExtensions.Immediate;

namespace ExtendedLinq.Tests.MsExtensions.Immediate;

using Microsoft.Extensions.Primitives;

public class ImmediateSegmentCountExtensionsTests
{
    [Test]
    public async Task Count_WithPredicate_ReturnsMatches()
    {
        StringSegment source = new("a1b2c");

        int actual = source.Count(char.IsDigit);

        await Assert.That(actual).IsEqualTo(2);
    }

    [Test]
    public async Task Count_WithNullPredicate_Throws()
    {
        StringSegment source = new("abc");

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
        {
            source.Count(null!);
            return Task.CompletedTask;
        });
    }
}
