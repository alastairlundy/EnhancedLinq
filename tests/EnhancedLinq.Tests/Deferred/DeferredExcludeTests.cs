using System.Collections;
using System.Linq;
using EnhancedLinq.Deferred;

namespace ExtendedLinq.Tests.Deferred;

public class DeferredExcludeTests
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
    public async Task Exclude_ExcludesSpecifiedItems()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        IEnumerable<int> exclude = [2, 4];

        IEnumerable<int> result = source.Exclude(exclude);

        List<int> actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([1, 3, 5]);
    }

    [Test]
    public async Task Exclude_PreservesSourceOrder()
    {
        IEnumerable<int> source = [5, 4, 3, 2, 1];
        IEnumerable<int> exclude = [2, 4];

        IEnumerable<int> result = source.Exclude(exclude);

        List<int> actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([5, 3, 1]);
    }

    [Test]
    public async Task Exclude_PreservesDuplicatesInSource()
    {
        IEnumerable<int> source = [1, 2, 2, 3];
        IEnumerable<int> exclude = [2];

        IEnumerable<int> result = source.Exclude(exclude);

        List<int> actual = [.. result];

        await Assert.That(actual).IsEquivalentTo([1, 3]);
    }

    [Test]
    public async Task Exclude_EnumeratesExcludeSequenceExactlyOnce()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        CountingEnumerable<int> exclude = new([2, 4]);

        IEnumerable<int> result = source.Exclude(exclude);

        _ = result.ToList();

        await Assert.That(exclude.EnumerationCount).IsEqualTo(1);
    }

    [Test]
    public async Task Exclude_ResultCanBeEnumeratedMultipleTimes()
    {
        IEnumerable<int> source = [1, 2, 3, 4, 5];
        IEnumerable<int> exclude = [2, 4];

        IEnumerable<int> result = source.Exclude(exclude);

        List<int> first = [.. result];
        List<int> second = [.. result];

        await Assert.That(first).IsEquivalentTo(second);
        await Assert.That(first).IsEquivalentTo([1, 3, 5]);
    }
}
