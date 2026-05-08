/*
    EnhancedLinq - Tests for Deferred GroupBy
*/

namespace ExtendedLinq.Tests.MsExtensions.Deferred;

using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using EnhancedLinq.MsExtensions.Deferred;

public class DeferredSegmentGroupExtensionsTests
{
    [Test]
    public async Task GroupBy_ConsecutiveRuns_ReturnsGroupings()
    {
        StringSegment source = new("aa11bb2");

        var groups = source.GroupBy(char.IsDigit).ToList();

        await Assert.That(groups.Count).IsEqualTo(3);

        await Assert.That(groups[0].Key).IsFalse();
        await Assert.That(groups[0].ToList()).IsEquivalentTo(new List<char> { 'a', 'a' });

        await Assert.That(groups[1].Key).IsTrue();
        await Assert.That(groups[1].ToList()).IsEquivalentTo(new List<char> { '1', '1' });

        await Assert.That(groups[2].Key).IsFalse();
        await Assert.That(groups[2].ToList()).IsEquivalentTo(new List<char> { 'b', 'b' });

        await Assert.That(groups[3].Key).IsTrue();
        await Assert.That(groups[3].ToList()).IsEquivalentTo(new List<char> { '2' });
    }

    [Test]
    public async Task GroupBy_NullPredicate_Throws()
    {
        StringSegment source = new("abc");

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
        {
            source.GroupBy((Func<char, object>)null!);
            return Task.CompletedTask;
        });
    }
}
