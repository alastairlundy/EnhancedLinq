/*
    EnhancedLinq - Tests for Deferred AsEnumerable
*/

namespace ExtendedLinq.Tests.MsExtensions.Deferred;

using System.Linq;
using Microsoft.Extensions.Primitives;
using EnhancedLinq.MsExtensions.Deferred;

public class DeferredSegmentAsEnumerableExtensionsTests
{
    [Test]
    public async Task AsEnumerable_ReturnsChars()
    {
        StringSegment source = new("abc");

        var result = source.AsEnumerable().ToList();

        await Assert.That(result).IsEquivalentTo(new List<char> { 'a', 'b', 'c' });
    }

    [Test]
    public async Task AsEnumerable_Empty_Throws()
    {
        StringSegment source = new("");

        await Assert.ThrowsAsync<ArgumentException>(() =>
        {
            source.AsEnumerable();
            return Task.CompletedTask;
        });
    }
}
