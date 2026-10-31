using System.Linq;
using System.Threading;
using EnhancedLinq.Async.Deferred;

namespace ExtendedLinq.Tests.Deferred;

public class AsyncExcludeTests
{
    private sealed class CountingAsyncEnumerable<T>(IAsyncEnumerable<T> inner) : IAsyncEnumerable<T>
    {
        public int EnumerationCount { get; private set; }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            EnumerationCount++;
            return inner.GetAsyncEnumerator(cancellationToken);
        }
    }

    private static async IAsyncEnumerable<int> ToAsync(IEnumerable<int> items)
    {
        foreach (int item in items)
            yield return item;
    }

    [Test]
    public async Task Exclude_ItemsToExclude_ExcludesSpecifiedItems()
    {
        IAsyncEnumerable<int> source = ToAsync([1, 2, 3, 4, 5]);
        IAsyncEnumerable<int> exclude = ToAsync([2, 4]);

        IAsyncEnumerable<int> result = source.Exclude(exclude);

        List<int> actual = [];

        await foreach (int item in result)
            actual.Add(item);

        await Assert.That(actual).IsEquivalentTo([1, 3, 5]);
    }

    [Test]
    public async Task Exclude_ItemsToExclude_EnumeratesExcludeSequenceExactlyOnce()
    {
        IAsyncEnumerable<int> source = ToAsync([1, 2, 3, 4, 5]);
        CountingAsyncEnumerable<int> exclude = new(ToAsync([2, 4]));

        IAsyncEnumerable<int> result = source.Exclude(exclude);

        await foreach (int _ in result)
        {
        }

        await Assert.That(exclude.EnumerationCount).IsEqualTo(1);
    }

    [Test]
    public async Task GenerateNumberRange_WithSkips_ExcludesSkippedNumbers()
    {
        IAsyncEnumerable<int> numbersToSkip = ToAsync([2, 4, 6]);

        IAsyncEnumerable<int> result = 0.GenerateNumberRange(10, 1, numbersToSkip);

        List<int> actual = [];

        await foreach (int item in result)
            actual.Add(item);

        await Assert.That(actual).IsEquivalentTo([0, 1, 3, 5, 7, 8, 9]);
    }

    [Test]
    public async Task GenerateNumberRange_WithSkips_EnumeratesNumbersToSkipExactlyOnce()
    {
        CountingAsyncEnumerable<int> numbersToSkip = new(ToAsync([2, 4, 6]));

        IAsyncEnumerable<int> result = 0.GenerateNumberRange(10, 1, numbersToSkip);

        await foreach (int _ in result)
        {
        }

        await Assert.That(numbersToSkip.EnumerationCount).IsEqualTo(1);
    }
}
