using System.Collections;
using System.Linq;
using EnhancedLinq.Deferred;

namespace ExtendedLinq.Tests.Deferred;

public class DeferredNumberRangeSkipTests
{
    private sealed class CountingEnumerable<T>(IEnumerable<T> inner) : IEnumerable<T>
    {
        public int EnumerationCount { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            EnumerationCount++;
            return inner.GetEnumerator();
        }

        System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Test]
    public async Task GenerateNumberRange_WithSkips_ExcludesSkippedNumbers()
    {
        IEnumerable<int> numbersToSkip = [2, 4, 6];

        IEnumerable<int> result = 0.GenerateNumberRange(10, 1, numbersToSkip);

        List<int> actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([0, 1, 3, 5, 7, 8, 9]);
    }

    [Test]
    public async Task GenerateNumberRange_WithSkips_EnumeratesNumbersToSkipExactlyOnce()
    {
        CountingEnumerable<int> numbersToSkip = new([2, 4, 6]);

        IEnumerable<int> result = 0.GenerateNumberRange(10, 1, numbersToSkip);

        _ = result.ToList();

        await Assert.That(numbersToSkip.EnumerationCount).IsEqualTo(1);
    }

    [Test]
    public async Task GenerateNumberRange_WithNoSkips_YieldsFullRange()
    {
        IEnumerable<int> result = 0.GenerateNumberRange(5, 1, Enumerable.Empty<int>());

        List<int> actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([0, 1, 2, 3, 4]);
    }
}
