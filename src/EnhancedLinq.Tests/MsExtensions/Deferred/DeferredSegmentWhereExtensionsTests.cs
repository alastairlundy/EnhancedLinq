/*
    EnhancedLinq - Tests for MsExtensions Deferred Where
*/

using EnhancedLinq.MsExtensions.Deferred;

namespace ExtendedLinq.Tests.MsExtensions.Deferred;

using System.Linq;
using Microsoft.Extensions.Primitives;

public class DeferredSegmentWhereExtensionsTests
{
    [Test]
    public async Task Where_ReturnsFilteredChars()
    {
        StringSegment source = new("a1b2c");

        List<char> result = source.Where(char.IsLetter).ToList();

        await Assert.That(result).IsEquivalentTo(new List<char> { 'a', 'b', 'c' });
    }

    [Test]
    public async Task Where_WithNullPredicate_Throws()
    {
        StringSegment source = new("x");

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            source.Where(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            return Task.CompletedTask;
        });
    }
}
